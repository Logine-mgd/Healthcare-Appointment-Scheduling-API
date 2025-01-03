using System.ComponentModel.DataAnnotations.Schema;

namespace MedConnect_API.DTOs
{
    public class Medication_recordDTO
    {
        public int Medication_id { get; set; }
        public int MedicalRecord_id { get; set; }
        public int Dosage { get; set; }
    }
}
