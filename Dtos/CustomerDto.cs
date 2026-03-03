using System;
using System.ComponentModel.DataAnnotations;

namespace BeerApi.Dtos
{
    public class CustomerDto
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(200)]
        public string LastName { get; set; }

        [EmailAddress]
        [MaxLength(320)]
        public string? Email { get; set; }

        [MaxLength(50)]
        public string? Phone { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
