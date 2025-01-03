using System.ComponentModel.DataAnnotations.Schema;

namespace MedConnect_API.Models
{
    public class MedicalRecord
    {
        public int Id { get; set; }
        [ForeignKey("Patient")]
        public string Patient_id { get; set; }
        public string Doctor_Name { get; set; }
        public DateTime Visit_date { get;set; }
        public string Reason {  get; set; }
        public string Treatmentnotes { get; set; }
        public virtual Patient Patient { get; set; }
        public List<Record_medications> Record_medications { get; set; } = new List<Record_medications> { };
    }
}
