using System.ComponentModel.DataAnnotations;


namespace ApplicationCore.Model
{
    public class LoginModel
    {
        [Required]
        public string email { get; set; }
        [Required]
        public string password { get; set; }
    }
}
