using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FOODMATE.Model
{
    public partial class Meals
    {
        [Key]
        [Column("meal_id")]
        public int MealId { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }

        [Column("m_date", TypeName = "date")]
        public DateTime? MDate { get; set; }

        [Column("product_id")]
        public int? ProductId { get; set; }

        [Column("ammount")]
        public int Ammount { get; set; }

        [Column("meal")]
        public string Meal { get; set; }

        [ForeignKey(nameof(ProductId))]
        [InverseProperty(nameof(Products.Meals))]
        public virtual Products Product { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty("Meals")]
        public virtual User User { get; set; }
    }
}
