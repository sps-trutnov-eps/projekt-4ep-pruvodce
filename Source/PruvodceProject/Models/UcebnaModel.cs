using System.ComponentModel.DataAnnotations;

namespace PruvodceProject.Models
{
    public class Ucebna
    {
        [Key]
        public Guid Id { get; set; }  

        [Required]
        public string patro { get; set; } = String.Empty;
        [Required]
        public string druh { get; set; } = String.Empty;

    }
}