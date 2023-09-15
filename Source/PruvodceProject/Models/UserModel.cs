using System.ComponentModel.DataAnnotations;

namespace PruvodceProject.Models
{
    public class prihlasovaci_udaje
    {
        [Key]
        public string? heslo { get; set; }
        [Required]
        public string prihlas_jmeno { get; set; } = String.Empty;
        [Required]
        public string mail { get; set; } = String.Empty;
    }
}
