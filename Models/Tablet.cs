using DigitalDevices.Enums;
using DigitalDevices.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace DigitalDevices.Models
{
    public class Tablet : Product, IDisplayable
    {
        [Display(Name = "Диагональ")]
        public int Diagonal { get; set; }
        [Display(Name = "Разрешение изображения")]
        public string Definition { get; set; }
        [Display(Name = "Кадров/секунду")]
        public float FPS { get; set; }
        [Display(Name = "Тип матрицы")]
        public MatrixTypes MatrixType { get; set; }
        public string CPU { get; set; }
        public Tablet()
        {
            
        }
    }
}
