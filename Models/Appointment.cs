using System.ComponentModel.DataAnnotations.Schema;

namespace MedConnect_API.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        [ForeignKey("Patient")]
        public string? Patient_id { get; set; }
        [ForeignKey("Provider")]
        public string? Provider_id { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
        public string Reason { get; set; }
        public virtual Patient? Patient { get; set; }
        public virtual Provider? Provider { get; set; }
    }
}
