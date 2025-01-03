using System.ComponentModel.DataAnnotations;

namespace MedConnect_API.DTOs
{
    public class MedicalRecordDTO
    {
        //public int Id { get; set; }
        public string Doctor_Name { get; set; }
        
        [Required(ErrorMessage = "Must Enter Patient ID")]
        public string Patient_id { get; set; }
        public DateTime Visit_date { get; set; }
        public string Reason { get; set; }
        public string Treatmentnotes { get; set; }
        public List<Medication_recordDTO> medication_Record { get; set; } = new List<Medication_recordDTO>();

    }
}
