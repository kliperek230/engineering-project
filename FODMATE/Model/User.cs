﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FOODMATE.Model
{
    public partial class User
    {
        public User()
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
        [Column("password")]
        [StringLength(100)]
        public string Password { get; set; }
        [Required]
        [Column("username")]
        [StringLength(20)]
        public string Username { get; set; }
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
        [StringLength(20)]
        public string Token { get; set; }
        [StringLength(20)]
        public string Role { get; set; }

        [ForeignKey(nameof(Bench))]
        [InverseProperty("UserBenchNavigation")]
        public virtual Lifts BenchNavigation { get; set; }
        [ForeignKey(nameof(Deadlift))]
        [InverseProperty("UserDeadliftNavigation")]
        public virtual Lifts DeadliftNavigation { get; set; }
        [ForeignKey(nameof(Measurements))]
        [InverseProperty("UserNavigation")]
        public virtual Measurements Measurements1 { get; set; }
        [ForeignKey(nameof(Ohp))]
        [InverseProperty("UserOhpNavigation")]
        public virtual Lifts OhpNavigation { get; set; }
        [ForeignKey(nameof(Squat))]
        [InverseProperty("UserSquatNavigation")]
        public virtual Lifts SquatNavigation { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<Lifts> Lifts { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<Meals> Meals { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<Measurements> MeasurementsNavigation { get; set; }
    }
}
