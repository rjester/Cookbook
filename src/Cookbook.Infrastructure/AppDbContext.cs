using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Cookbook.Infrastructure
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Ingredient> Ingredients { get; set; } = null!;
        public virtual DbSet<Recipe> Recipes { get; set; } = null!;
        public virtual DbSet<RecipeIngredient> RecipeIngredients { get; set; } = null!;
        public virtual DbSet<Step> Steps { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.;Database=Cookbook1;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ingredient>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Recipe>(entity =>
            {
                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Title).HasMaxLength(50);
            });

            modelBuilder.Entity<RecipeIngredient>(entity =>
            {
                entity.HasIndex(e => e.IngredientId, "IX_RecipeIngredients_IngredientId");

                entity.HasIndex(e => e.RecipeId, "IX_RecipeIngredients_RecipeId");

                entity.Property(e => e.Quantity).HasColumnType("decimal(5, 3)");

                entity.Property(e => e.Unit).HasMaxLength(50);

                entity.HasOne(d => d.Ingredient)
                    .WithMany(p => p.RecipeIngredients)
                    .HasForeignKey(d => d.IngredientId);

                entity.HasOne(d => d.Recipe)
                    .WithMany(p => p.RecipeIngredients)
                    .HasForeignKey(d => d.RecipeId);
            });

            modelBuilder.Entity<Step>(entity =>
            {
                entity.HasIndex(e => e.RecipeId, "IX_Steps_RecipeId");

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.HasOne(d => d.Recipe)
                    .WithMany(p => p.Steps)
                    .HasForeignKey(d => d.RecipeId);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
