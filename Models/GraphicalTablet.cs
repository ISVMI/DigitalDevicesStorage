using DigitalDevices.Enums;
using DigitalDevices.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace DigitalDevices.Models
{
    public class GraphicalTablet : Peripheral, IDisplayable
    {
        [Display(Name = "Диагональ")]
        public int Diagonal { get; set; }
        [Display(Name = "Разрешение изображения")]
        public string Definition { get; set; }
        [Display(Name = "Кадров/секунду")]
        public float FPS { get; set; }
        [Display(Name = "Тип матрицы")]
        public MatrixTypes MatrixType { get; set; }
        [Display(Name = "Время отклика")]
        public int ResponceTime { get; set; }
        [Display(Name = "Рабочая ширина")]
        public float WorkWidth { get;set; }
        [Display(Name = "Рабочая высота")]
        public float WorkHeight { get; set; }
        [Display(Name = "Разрешение планшета")]
        public int TabletDefinition { get; set; }
        [Display(Name = "Чувствительность")]
        public int Sensivity { get;set; }
        public GraphicalTablet()
        {
            
        }
    }
}
