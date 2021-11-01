﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cookbook.Data.Entities
{
    public class RecipeIngredient : BaseEntity
    {
        public Recipe Recipe { get; set; }
        public Ingredient Ingredient { get; set; }
        [Column(TypeName = "decimal(5,3)")]
        public decimal Quantity { get; set; }
        [MaxLength(50)]
        public string Unit { get; set; }
    }
}
