using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodMate.Models
{
    [Table("UserRole")]
    public partial class UserRole
    {
        [Key,Required]
        public int Id { get; set; }
        [Required, ForeignKey(nameof(Users))]
        public int RoleId { get; set; }
        [Required, ForeignKey(nameof(Role))]
        public int UserId { get; set; }

        public virtual Role Role { get; set; }
        public virtual Users Users { get; set; }
    }
}
