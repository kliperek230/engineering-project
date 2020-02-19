using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace foodmateapp.Model
{
    public partial class Lifts
    {
        public Lifts()
        {
            UsersBenchNavigation = new HashSet<Users>();
            UsersDeadliftNavigation = new HashSet<Users>();
            UsersOhpNavigation = new HashSet<Users>();
            UsersSquatNavigation = new HashSet<Users>();
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
        [InverseProperty(nameof(Users.Lifts))]
        public virtual Users User { get; set; }
        [InverseProperty(nameof(Users.BenchNavigation))]
        public virtual ICollection<Users> UsersBenchNavigation { get; set; }
        [InverseProperty(nameof(Users.DeadliftNavigation))]
        public virtual ICollection<Users> UsersDeadliftNavigation { get; set; }
        [InverseProperty(nameof(Users.OhpNavigation))]
        public virtual ICollection<Users> UsersOhpNavigation { get; set; }
        [InverseProperty(nameof(Users.SquatNavigation))]
        public virtual ICollection<Users> UsersSquatNavigation { get; set; }
    }
}
