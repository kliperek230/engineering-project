using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FoodMateApp.Models
{
    public partial class Users
    {
        public Users()
        {
            Lifts = new HashSet<Lifts>();
            Meals = new HashSet<Meals>();
            MeasurementsNavigation = new HashSet<Measurements>();
        }

        public int UserId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Sex { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        public int UHeight { get; set; }
        public int UWeight { get; set; }
        public string Email { get; set; }
        [Required]
        public string Pswd { get; set; }
        [Required]
        public string Nickname { get; set; }
        [Required]
        public double? Kcal { get; set; }
        public double? Protein { get; set; }
        public double? Carbs { get; set; }
        public double? Fats { get; set; }
        public int? Bench { get; set; }
        public int? Ohp { get; set; }
        public int? Squat { get; set; }
        public int? Deadlift { get; set; }
        public int? Measurements { get; set; }
        public string UType { get; set; }

        public virtual Lifts BenchNavigation { get; set; }
        public virtual Lifts DeadliftNavigation { get; set; }
        public virtual Measurements Measurements1 { get; set; }
        public virtual Lifts OhpNavigation { get; set; }
        public virtual Lifts SquatNavigation { get; set; }
        public virtual ICollection<Lifts> Lifts { get; set; }
        public virtual ICollection<Meals> Meals { get; set; }
        public virtual ICollection<Measurements> MeasurementsNavigation { get; set; }
    }
}
