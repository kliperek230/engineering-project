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
            UsersBenchNavigation = new HashSet<User>();
            UsersDeadliftNavigation = new HashSet<User>();
            UsersOhpNavigation = new HashSet<User>();
            UsersSquatNavigation = new HashSet<User>();
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
        [InverseProperty(nameof(Model.User.Lifts))]
        public virtual User User { get; set; }
        [InverseProperty(nameof(Model.User.BenchNavigation))]
        public virtual ICollection<User> UsersBenchNavigation { get; set; }
        [InverseProperty(nameof(Model.User.DeadliftNavigation))]
        public virtual ICollection<User> UsersDeadliftNavigation { get; set; }
        [InverseProperty(nameof(Model.User.OhpNavigation))]
        public virtual ICollection<User> UsersOhpNavigation { get; set; }
        [InverseProperty(nameof(Model.User.SquatNavigation))]
        public virtual ICollection<User> UsersSquatNavigation { get; set; }
    }
}
