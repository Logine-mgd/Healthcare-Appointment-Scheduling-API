using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedConnect_API.DTOs
{
    public class AppointmentDTO
    {
        //public int Id { get; set; }
        [Required(ErrorMessage = "Must Enter Patient ID")]
        public string Patient_id { get; set; }
        [Required(ErrorMessage = "Must Enter Provider ID")]
        public string Provider_id { get; set; }
        public string Date { get; set; }
        public string Time {  get; set; }
        public string Status { get; set; }
        public string Reason { get; set; }
    }
}
