using System;
using System.Collections.Generic;

namespace FoodMateApp.Models
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

        public int LiftId { get; set; }
        public int UserId { get; set; }
        public DateTime MDate { get; set; }
        public string LiftName { get; set; }
        public int Value { get; set; }

        public virtual Users User { get; set; }
        public virtual ICollection<Users> UsersBenchNavigation { get; set; }
        public virtual ICollection<Users> UsersDeadliftNavigation { get; set; }
        public virtual ICollection<Users> UsersOhpNavigation { get; set; }
        public virtual ICollection<Users> UsersSquatNavigation { get; set; }
    }
}
