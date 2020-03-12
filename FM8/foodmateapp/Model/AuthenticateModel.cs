using System.ComponentModel.DataAnnotations;

namespace foodmateapp.Model
{
    public class AuthenticateModel
    {
        [Required]
        public string Nickname { get; set; }

        [Required]
        public string Pswd { get; set; }
    }
}
