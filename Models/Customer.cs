using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BeerApi.Models
{
    public class Customer
    {
        [Key]
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        [JsonPropertyName("firstName")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(200)]
        [JsonPropertyName("lastName")]
        public string LastName { get; set; }

        [MaxLength(320)]
        [EmailAddress]
        [JsonPropertyName("email")]
        public string? Email { get; set; }

        [MaxLength(50)]
        [JsonPropertyName("phone")]
        public string? Phone { get; set; }

        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
