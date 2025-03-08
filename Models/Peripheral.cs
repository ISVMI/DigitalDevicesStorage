using DigitalDevices.Enums;
using System.ComponentModel.DataAnnotations;

namespace DigitalDevices.Models
{
    public abstract class Peripheral : Product
    {
        [Display(Name = "Тип подключения")]
        public ConnectionType ConnectionType { get; set; }
    }
}
