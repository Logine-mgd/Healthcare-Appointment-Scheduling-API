namespace MedConnect_API.DTOs
{
    public class EditMedRecordDTO
    {
        public int Id { get; set; }
        public string Doctor_Name { get; set; }
        public string Patient_id { get; set; }
        public DateTime Visit_date { get; set; }
        public string Reason { get; set; }
        public string Treatmentnotes { get; set; }
        public List<EditMedcationrecDTO> medication_Record { get; set; } = new List<EditMedcationrecDTO>();

    }
}
