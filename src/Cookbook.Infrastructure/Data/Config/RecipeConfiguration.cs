using Cookbook.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cookbook.Infrastructure.Data.Config
{
    public class RecipeConfiguration : IEntityTypeConfiguration<Recipe>
    {
        public void Configure(EntityTypeBuilder<Recipe> builder)
        {
            builder.Property(p => p.Title)
                .HasMaxLength(100)
                .IsRequired();
            builder.Property(p => p.Description)
                .HasMaxLength(1000)
                .IsRequired();
            builder.Property(p => p.PhotoUrl)
                .HasMaxLength(500);
            builder.Property(p => p.PrepTime)
                .HasMaxLength(50);
            builder.Property(p => p.CookTime)
                .HasMaxLength(50);
            builder.Property(p => p.ReadyIn)
                .HasMaxLength(50);
            builder.OwnsMany(p =>
                p.Steps, a =>
                {
                    a.WithOwner().HasForeignKey("RecipeId");
                    a.Property<int>("Id");
                    a.HasKey("Id");
                });
            builder.OwnsMany(p =>
                p.Ingredients, a =>
                {
                    a.WithOwner().HasForeignKey("RecipeId");
                    a.Property<int>("Id");
                    a.HasKey("Id");
                });
            //builder.Property(p => p.PrepTime)
            //    .HasMaxLength(50)
            //    .IsRequired();
            //builder.Property(p => p.CookTime)
            //    .HasMaxLength(50);
            //builder.Property(p => p.ReadyIn)
            //    .HasMaxLength(50)
            //    .IsRequired();
            //builder.Property(p => p.Servings)
            //    .HasMaxLength(50);
            //builder.Property(p => p.Yield)
            //    .HasMaxLength(50);
        }
    }
}
