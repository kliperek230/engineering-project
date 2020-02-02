using System;
using System.Collections.Generic;

namespace FoodMateApp.Models
{
    public partial class Meals
    {
        public int MealId { get; set; }
        public int UserId { get; set; }
        public DateTime? MDate { get; set; }
        public int? ProductId { get; set; }

        public virtual Products Product { get; set; }
        public virtual Users User { get; set; }
    }
}
