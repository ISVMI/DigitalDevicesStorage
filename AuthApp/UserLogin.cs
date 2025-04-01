using System.ComponentModel.DataAnnotations;

namespace DigitalDevices.AuthApp
{
    public class UserLogin
    {
        [Required, MaxLength(256)]
        [Display(Name = "Логин")]
        public string Username { get; set; }
        [Required, DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
    }
}
