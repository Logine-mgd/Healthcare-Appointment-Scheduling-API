using System.ComponentModel.DataAnnotations.Schema;

namespace MedConnect_API.Models
{
    public class Record_medications
    {
        public int Id { get; set; }

        [ForeignKey("Medication")]
        public int? Medication_id { get; set; }

        [ForeignKey("MedicalRecord")]
        public int? MedicalRecord_id { get; set; }
        public decimal Dosage { get; set; }

        public virtual MedicalRecord? MedicalRecord { get; set; }
        public virtual Medication? Medication { get; set; }

    }
}
