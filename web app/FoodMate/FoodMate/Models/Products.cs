using System;
using System.Collections.Generic;

namespace FoodMate.Models
{
    public partial class Products
    {
        public Products()
        {
            Meals = new HashSet<Meals>();
        }

        public int ProductId { get; set; }
        public string Category { get; set; }
        public string ProductNameEng { get; set; }
        public double? Kcal { get; set; }
        public double? Protein { get; set; }
        public double? Carbs { get; set; }
        public double? Fats { get; set; }
        public string ProductNamePl { get; set; }

        public virtual ICollection<Meals> Meals { get; set; }
    }
}
