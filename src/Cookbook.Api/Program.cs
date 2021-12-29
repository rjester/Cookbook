using Cookbook.SharedKernel.Interfaces;
using Autofac.Extensions.DependencyInjection;
using Cookbook.Core;
using Cookbook.Infrastructure;
using Cookbook.Infrastructure.Data;
using Cookbook.Api;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;
using Autofac;
using Microsoft.AspNetCore.Mvc;
using Cookbook.Core.Services;
using Cookbook.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
    
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "MyPolicy",
        builder =>
        {
            builder.WithOrigins("https://cookbook.russjester.com", 
                                "http://localhost:3000")
                    .WithMethods("PUT", "DELETE", "GET");
        });
});
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.None;
});

builder.Configuration.AddEnvironmentVariables(prefix: "ConnectionStrings__");

//string connectionString = Environment.GetEnvironmentVariable("CookbookConnection");  //Configuration.GetConnectionString("DefaultConnection");
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");  //Configuration.GetConnectionString("DefaultConnection");

//builder.Services.AddDbContext(connectionString);
builder.Services.AddDbContext<AppDbContext>(options => {
    options.UseSqlServer(connectionString);
});


//builder.Services.AddScoped(typeof(EfRepository<>));

// Add services to the container.
//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Cookbook API", Version = "v1" });
                c.EnableAnnotations();
            });

builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterModule(new DefaultCoreModule());
    containerBuilder.RegisterModule(new DefaultInfrastructureModule(builder.Environment.EnvironmentName == "Development"));
});


builder.Logging.ClearProviders();
builder.Logging.AddConsole()
    .AddFilter(DbLoggerCategory.Database.Command.Name, LogLevel.Information);
builder.Logging.AddDebug();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("MyPolicy");
//app.UseAuthentication();
//app.UseAuthorization();

app.MapControllers();

//app.MapGet("/api/recipes",([FromServices] IRecipeService svc) =>
//    {
//    return svc.GetAll();
//});

//app.MapGet("/api/recipes/{id:int}", async ([FromServices] IRecipeService svc, int id) =>
//{
//    var result = await svc.GetById(id);
//    if (result is null)
//    {
//        return Results.NotFound();
//    }
//    return Results.Ok(result);
//});

//app.MapGet("/api/recipes/search/{title}", async ([FromServices]IRecipeService svc, string title) =>
//{
//    var result = await svc.GetByTitle(title);
//    return Results.Ok(result);
//});

app.Run();
