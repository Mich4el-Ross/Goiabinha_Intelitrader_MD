using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Goiabinha_Intelitrader_API.Models
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; } = new Guid();

        [Required(ErrorMessage = "FirstName is a required field")]
        [MinLength(5, ErrorMessage = "FirstName must contain at least 5 characters")]
        public string? FirstName { get; set; }

        public string? SurName { get; set; }

        [Required(ErrorMessage = "Age is a required field")]
        public int Age { get; set; }

        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreationDate { get; set; } = DateTime.UtcNow;
    }
}
