using System.ComponentModel.DataAnnotations;

namespace DigitalDevices.AuthService.Core.Models
{
    public class User
    {
        [Key]
        [Required]
        public long Id { get; set; }
        [Required, MaxLength(256)]
        [Display(Name = "Логин")]
        public string Username { get; set; }
        [Required, DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string PasswordHash { get; set; }
        public long RoleId { get; set; }
        public virtual Role Role { get; set; }
    }
}
