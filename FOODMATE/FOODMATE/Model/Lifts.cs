using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FOODMATE.Model
{
    public partial class Lifts
    {
        public Lifts()
        {
            UserBenchNavigation = new HashSet<User>();
            UserDeadliftNavigation = new HashSet<User>();
            UserOhpNavigation = new HashSet<User>();
            UserSquatNavigation = new HashSet<User>();
        }

        [Key]
        [Column("lift_id")]
        public int LiftId { get; set; }
        [Column("user_id")]
        public int UserId { get; set; }
        [Column("m_date", TypeName = "date")]
        public DateTime MDate { get; set; }
        [Required]
        [Column("lift_name")]
        [StringLength(8)]
        public string LiftName { get; set; }
        [Column("value")]
        public int Value { get; set; }

        [ForeignKey(nameof(UserId))]
        [InverseProperty("Lifts")]
        public virtual User User { get; set; }
        [InverseProperty("BenchNavigation")]
        public virtual ICollection<User> UserBenchNavigation { get; set; }
        [InverseProperty("DeadliftNavigation")]
        public virtual ICollection<User> UserDeadliftNavigation { get; set; }
        [InverseProperty("OhpNavigation")]
        public virtual ICollection<User> UserOhpNavigation { get; set; }
        [InverseProperty("SquatNavigation")]
        public virtual ICollection<User> UserSquatNavigation { get; set; }
    }
}
