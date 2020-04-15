using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FOODMATE.Model
{
    public partial class Products
    {
        public Products()
        {
            Meals = new HashSet<Meals>();
        }

        [Key]
        [Column("product_id")]
        public int ProductId { get; set; }
        [Column("category")]
        [StringLength(20)]
        public string Category { get; set; }
        [Column("product_name_eng")]
        [StringLength(50)]
        public string ProductNameEng { get; set; }
        [Column("kcal")]
        public double? Kcal { get; set; }
        [Column("protein")]
        public double? Protein { get; set; }
        [Column("carbs")]
        public double? Carbs { get; set; }
        [Column("fats")]
        public double? Fats { get; set; }
        [Column("product_name_pl")]
        [StringLength(50)]
        public string ProductNamePl { get; set; }

        [InverseProperty("Product")]
        public virtual ICollection<Meals> Meals { get; set; }
    }
}
