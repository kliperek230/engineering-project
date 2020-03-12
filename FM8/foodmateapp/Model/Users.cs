using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace foodmateapp.Model
{
    public class Users
    {
        public Users()
        {
            Lifts = new HashSet<Lifts>();
            Meals = new HashSet<Meals>();
            MeasurementsNavigation = new HashSet<Measurements>();
        }

        [Key]
        [Column("user_id")]
        public int UserId { get; set; }

        [Required]
        [Column("first_name")]
        [StringLength(20)]
        public string FirstName { get; set; }

        [Required]
        [Column("last_name")]
        [StringLength(20)]
        public string LastName { get; set; }

        [Required]
        [Column("sex")]
        [StringLength(1)]
        public string Sex { get; set; }

        [Column("birth_date", TypeName = "date")]
        public DateTime BirthDate { get; set; }

        [Column("u_height")]
        public int UHeight { get; set; }

        [Column("u_weight")]
        public int UWeight { get; set; }

        [Column("email")]
        [StringLength(100)]
        public string Email { get; set; }

        [Column("pswd")]
        [StringLength(100)]
        public string Pswd { get; set; }

        [Required]
        [Column("nickname")]
        [StringLength(20)]
        public string Nickname { get; set; }

        [Column("kcal")]
        public double? Kcal { get; set; }

        [Column("protein")]
        public double? Protein { get; set; }

        [Column("carbs")]
        public double? Carbs { get; set; }

        [Column("fats")]
        public double? Fats { get; set; }

        [Column("bench")]
        public int? Bench { get; set; }

        [Column("ohp")]
        public int? Ohp { get; set; }

        [Column("squat")]
        public int? Squat { get; set; }

        [Column("deadlift")]
        public int? Deadlift { get; set; }

        [Column("measurements")]
        public int? Measurements { get; set; }

        [Column("Token")]
        [StringLength(20)]
        public string Token { get; set; }


        [ForeignKey(nameof(Bench))]
        [InverseProperty("UsersBenchNavigation")]
        public virtual Lifts BenchNavigation { get; set; }
        [ForeignKey(nameof(Deadlift))]
        [InverseProperty("UsersDeadliftNavigation")]
        public virtual Lifts DeadliftNavigation { get; set; }
        [ForeignKey(nameof(Measurements))]
        [InverseProperty("Users")]
        public virtual Measurements Measurements1 { get; set; }
        [ForeignKey(nameof(Ohp))]
        [InverseProperty("UsersOhpNavigation")]
        public virtual Lifts OhpNavigation { get; set; }
        [ForeignKey(nameof(Squat))]
        [InverseProperty("UsersSquatNavigation")]
        public virtual Lifts SquatNavigation { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<Lifts> Lifts { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<Meals> Meals { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<Measurements> MeasurementsNavigation { get; set; }
    }
}
