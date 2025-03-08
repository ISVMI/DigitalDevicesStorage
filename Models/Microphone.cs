using DigitalDevices.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalDevices.Models
{
    public class Microphone : Peripheral
    {
        [Display(Name = "Принцип действия")]
        public OperatingPrinciple Principle { get; set; }
        [Display(Name = "Направленность")]
        public MicrophoneDirections Direction { get; set; }
        [Display(Name = "Вид исполнения")]
        public MicrophoneExecutionTypes ExecutionType { get; set; }
        [Display(Name = "Разъём подключения")]
        public AudioConnectionType AudioConnectionType { get; set; }
        [Display(Name = "Минимальная частота")]
        public int MinFrequency { get; set; }
        [Display(Name = "Максимальная частота")]
        public int MaxFrequency { get; set; }
        [Display(Name = "Сопротивление (импенданс) (Ом)")]
        public int Impedance { get; set; }
        public Microphone()
        {
            
        }
    }
}
