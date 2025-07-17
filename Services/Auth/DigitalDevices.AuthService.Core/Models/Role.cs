namespace DigitalDevices.AuthService.Core.Models
{
    public class Role
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int PermissionLevel { get; set; } = 0;
        public string? SecretCode { get; set; }

        public virtual ICollection<User> Users { get; set; } = new List<User>();
    }
}
