using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using Minimal.Blazor.Data;
using Minimal.Infrastructure.Data;
using Serilog;
using Serilog.Formatting.Compact;

Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console(new RenderedCompactJsonFormatter())
                .CreateBootstrapLogger();

Log.Information("Starting up");
try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog((ctx, lc) => lc
                    .WriteTo.Console()
                    .ReadFrom.Configuration(ctx.Configuration));

    //builder.Logging.ClearProviders();
    //builder.Logging.AddConsole();

    builder.Configuration.AddEnvironmentVariables(prefix: "ConnectionStrings__");
    string connectionString = builder.Configuration.GetConnectionString("CookbookConnection");
    builder.Services.AddDbContext<AppDbContext>(options =>
                //options.UseInMemoryDatabase("Cookbooks"));
                options.UseSqlServer(connectionString));
    // Add services to the container.
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddRazorPages();
    builder.Services.AddControllersWithViews();
    builder.Services.AddServerSideBlazor();
    builder.Services.AddSingleton<WeatherForecastService>();
    builder.Services.AddTransient<RecipeService1>();
    builder.Services.AddHttpClient();


    var app = builder.Build();

    app.UseSerilogRequestLogging();

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;

        try
        {
            Log.Information(connectionString);
            var context = services.GetRequiredService<AppDbContext>();
            //                        context.Database.Migrate();
            context.Database.EnsureCreated();
        }
        catch (Exception ex)
        {
            Log.Error(ex, "An error occurred seeding the DB.");
        }
    }

    app.UseHttpsRedirection();

    app.UseStaticFiles();

    app.UseRouting();

    

    if (app.Environment.IsDevelopment())
    {
        app.MapSwagger();
        app.UseSwaggerUI();
    }

    app.MapGet("/recipes", async (AppDbContext context) =>
            await context.Recipes.ToListAsync())
        .WithName("GetAllRecipes");
    //app.MapGet("/recipes/recent", async (AppDbContext context) =>
    //{
    //    var tt = await context.Recipes.OrderByDescending(x => x.Id).Take(10).ToListAsync();
    //    return tt;
    //})
    //    .WithName("GetRecentRecipes");
    app.MapGet("/recipes/search", async (AppDbContext context, string q) =>
            await context.Recipes.Where(x => x.Title.Contains(q)).ToListAsync())
        .WithName("GetRecipeSearch");
    app.MapGet("/recipes/{slug}", async (string slug, AppDbContext context) =>
        //await context.Recipes.Include(x => x.Steps).SingleOrDefaultAsync(x => x.Id == id)
        await context.Recipes.SingleOrDefaultAsync(x => x.Slug == slug)
            is Recipe recipe
                ? Results.Ok(recipe)
                : Results.NotFound())
        .WithName("GetRecipeBySlug")
        .Produces<Recipe>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

    app.MapPost("/recipes", async (Recipe recipe, AppDbContext context) =>
        {
            var collisions = await context.Recipes.Where(x => x.Slug.Contains(recipe.Slug) && x.Id != recipe.Id).Select(x => x.Slug).ToListAsync();
            if (collisions?.Count() > 0)
            {
                recipe.Slug += "-" + collisions.Count().ToString();
            }
            context.Recipes.Add(recipe);
            await context.SaveChangesAsync();

            return Results.Created($"/recipes/{recipe.Slug}", recipe);
        })
        .WithName("PostRecipe")
        .ProducesValidationProblem()
        .Produces<Recipe>(StatusCodes.Status201Created);
    app.MapPut("/recipes/{id}", async (int id, Recipe recipeUpdate, AppDbContext context) =>
        {
            var recipe = await context.Recipes.FindAsync(id);

            if (recipe is null) return Results.NotFound();

            recipe.UpdateTitle(recipeUpdate.Title);
            recipe.UpdateDescription(recipeUpdate.Description);
            await context.SaveChangesAsync();
            return Results.NoContent();
        })
        .WithName("UpdateRecipe")
        .ProducesValidationProblem()
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status404NotFound);
    app.MapDelete("/recipes/{id}", async (int id, AppDbContext context) =>
        {
            if (await context.Recipes.FindAsync(id) is Recipe recipe)
            {
                context.Recipes.Remove(recipe);
                await context.SaveChangesAsync();
                return Results.Ok(recipe);
            }

            return Results.NotFound();
        })
        .WithName("DeleteRecipe")
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status404NotFound);


    app.MapControllers();
    app.MapBlazorHub();
    app.MapFallbackToPage("/_Host");

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Unhandled exception");
}
finally
{
    Log.Information("Shutdown complete");
    Log.CloseAndFlush();
}

