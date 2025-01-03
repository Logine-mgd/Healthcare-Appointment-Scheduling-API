using System.ComponentModel.DataAnnotations;

namespace MedConnect_API.DTOs
{
    public class ProviderDTO
    {
        [Required]
        public string id { get; set; }
        public string Username {  get; set; }
        public string Password { get; set; }

    }
}
