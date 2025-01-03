using System.ComponentModel.DataAnnotations;

namespace MedConnect_API.DTOs
{
    public class EditProviderDTO
    {
        [Required]
        public string Id { get; set; }
        public string Username { get; set; }
    }
}
