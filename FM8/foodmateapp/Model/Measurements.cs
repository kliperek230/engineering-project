using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace foodmateapp.Model
{
    public partial class Measurements
    {
        public Measurements()
        {
            Users = new HashSet<Users>();
        }

        [Key]
        [Column("measurement_id")]
        public int MeasurementId { get; set; }
        [Column("m_date", TypeName = "date")]
        public DateTime? MDate { get; set; }
        [Column("user_id")]
        public int UserId { get; set; }
        [Column("l_calve")]
        public int? LCalve { get; set; }
        [Column("r_calve")]
        public int? RCalve { get; set; }
        [Column("l_thigh")]
        public int? LThigh { get; set; }
        [Column("r_thigh")]
        public int? RThigh { get; set; }
        [Column("butt")]
        public int? Butt { get; set; }
        [Column("waist")]
        public int? Waist { get; set; }
        [Column("chest")]
        public int? Chest { get; set; }
        [Column("l_arm")]
        public int? LArm { get; set; }
        [Column("r_arm")]
        public int? RArm { get; set; }
        [Column("l_forearm")]
        public int? LForearm { get; set; }
        [Column("r_forearm")]
        public int? RForearm { get; set; }
        [Column("u_weight")]
        public int? UWeight { get; set; }

        [ForeignKey(nameof(UserId))]
        [InverseProperty("MeasurementsNavigation")]
        public virtual Users User { get; set; }
        [InverseProperty("Measurements1")]
        public virtual ICollection<Users> Users { get; set; }
    }
}
