using System;
using System.Collections.Generic;

namespace FoodMateApp.Models
{
    public partial class Measurements
    {
        public Measurements()
        {
            Users = new HashSet<Users>();
        }

        public int MeasurementId { get; set; }
        public DateTime? MDate { get; set; }
        public int UserId { get; set; }
        public int? LCalve { get; set; }
        public int? RCalve { get; set; }
        public int? LThigh { get; set; }
        public int? RThigh { get; set; }
        public int? Butt { get; set; }
        public int? Waist { get; set; }
        public int? Chest { get; set; }
        public int? LArm { get; set; }
        public int? RArm { get; set; }
        public int? LForearm { get; set; }
        public int? RForearm { get; set; }
        public int? UWeight { get; set; }

        public virtual Users User { get; set; }
        public virtual ICollection<Users> Users { get; set; }
    }
}
