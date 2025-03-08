using System.ComponentModel.DataAnnotations;

namespace DigitalDevices.Models
{
    public class WebCam : Peripheral
    {
        [Display(Name = "Количество мегапикселей")]
        public int MegaPixels { get; set; }
        [Display(Name = "Разрешение изображения")]
        public string Definition { get; set; }
        [Display(Name = "Кадров/секунду")]
        public int FPS { get; set; }
        [Display(Name = "Микрофон")]
        public bool Microphone { get; set; }
        public WebCam()
        {
            
        }
    }
}
