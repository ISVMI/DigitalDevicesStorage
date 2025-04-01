using System.ComponentModel.DataAnnotations;

namespace DigitalDevices.AuthApp
{
    public class UserRegistration
    {
        [Required, MaxLength(256)]
        [Display(Name = "Логин")]
        public string Username { get; set; }
        [Required, DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
        [DataType(DataType.Password), Compare(nameof(Password))]
        [Display(Name = "Подтверждение пароля")]
        public string PasswordConfirmation { get; set; }
        [Display(Name = "Код доступа (опционально)")]
        public string? SecretCode { get; set; }
    }
}
