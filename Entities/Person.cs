using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConnectionLibrary.Entities
{
    public class Person
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string? LinkedIn {  get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        [Required]
        public string Description { get; set; } // description of what I remember about someone
    }
}
