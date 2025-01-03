using System.ComponentModel.DataAnnotations;

namespace MedConnect_API.DTOs
{
    public class Patient_LoginDTO
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }   
    }
}
