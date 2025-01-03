using System.ComponentModel.DataAnnotations;

namespace MedConnect_API.DTOs
{
    public class Patient_RegDTO
    {
        public string id { get; set; }
        public string Fullname { get; set; }
        public string DoB { get; set; }
        //DateOnly dob = DateOnly.Parse("1990-05-15");
        [Required(ErrorMessage = "Must Enter username")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Must Enter password")]
        public string Password { get; set; }

        public string Gender { get; set; }
        [Required(ErrorMessage = "Must Enter NationalID")]
        [StringLength(14, MinimumLength = 14, ErrorMessage = "National ID must be exactly 14 numbers.")]
        public string NationalID { get; set; }
        [StringLength(150)]
        public string Address { get; set; }
        [StringLength(4)]
        public string BloodGP { get; set; }
        public decimal Weight { get; set; }
        public decimal Height { get; set; }

    }
}
