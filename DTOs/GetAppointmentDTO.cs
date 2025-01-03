using System.ComponentModel.DataAnnotations;

namespace MedConnect_API.DTOs
{
    public class GetAppointmentDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Must Enter Patient ID")]
        public string Patient_id { get; set; }
        [Required(ErrorMessage = "Must Enter Provider ID")]
        public string Provider_id { get; set; }
        public string Date { get; set; }
        public string Status { get; set; }
        public string Reason { get; set; }

    }
}
