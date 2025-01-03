namespace MedConnect_API.DTOs
{
    public class EditMedcationrecDTO
    {
        public int Id { get; set; }
        public int Medication_id { get; set; }
        public int MedicalRecord_id { get; set; }
        public int Dosage { get; set; }

    }
}
