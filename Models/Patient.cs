using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedConnect_API.Models
{
    public class Patient: IdentityUser
    {
        public string Fullname { get; set; }
        public DateTime DoB { get; set; }
        public string Gender { get; set; }
        [Required]
        public string NationalID { get; set; }
        public string Address { get; set; }
        public string BloodGP { get; set; }
      //  [Precision(3, 2)] 
        public decimal Weight { get; set; }
      //  [Precision(3, 2)] 
        public decimal Height { get; set; }
        public virtual List<MedicalRecord> MedicalRecords { get; set; } = new List<MedicalRecord>();
    }
}
