Build started...
Build succeeded.
info: Microsoft.EntityFrameworkCore.Infrastructure[10403]
      Entity Framework Core 6.0.0 initialized 'AppDbContext' using provider 'Microsoft.EntityFrameworkCore.SqlServer:6.0.0' with options: None
IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Ingredients] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(50) NOT NULL,
    CONSTRAINT [PK_Ingredients] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Recipes] (
    [Id] int NOT NULL IDENTITY,
    [Title] nvarchar(50) NOT NULL,
    [Description] nvarchar(500) NOT NULL,
    CONSTRAINT [PK_Recipes] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [RecipeIngredients] (
    [Id] int NOT NULL IDENTITY,
    [RecipeId] int NOT NULL,
    [IngredientId] int NOT NULL,
    [Quantity] decimal(5,3) NOT NULL,
    [Unit] nvarchar(50) NOT NULL,
    CONSTRAINT [PK_RecipeIngredients] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_RecipeIngredients_Ingredients_IngredientId] FOREIGN KEY ([IngredientId]) REFERENCES [Ingredients] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_RecipeIngredients_Recipes_RecipeId] FOREIGN KEY ([RecipeId]) REFERENCES [Recipes] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Steps] (
    [Id] int NOT NULL IDENTITY,
    [Description] nvarchar(500) NOT NULL,
    [RecipeId] int NULL,
    CONSTRAINT [PK_Steps] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Steps_Recipes_RecipeId] FOREIGN KEY ([RecipeId]) REFERENCES [Recipes] ([Id])
);
GO

CREATE INDEX [IX_RecipeIngredients_IngredientId] ON [RecipeIngredients] ([IngredientId]);
GO

CREATE INDEX [IX_RecipeIngredients_RecipeId] ON [RecipeIngredients] ([RecipeId]);
GO

CREATE INDEX [IX_Steps_RecipeId] ON [Steps] ([RecipeId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20211204143741_initial', N'6.0.0');
GO

COMMIT;
GO


