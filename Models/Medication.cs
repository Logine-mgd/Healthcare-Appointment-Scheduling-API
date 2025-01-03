namespace MedConnect_API.Models
{
    public class Medication
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Record_medications> record_Medications { get; set; } = new List<Record_medications> { };
    }
}
