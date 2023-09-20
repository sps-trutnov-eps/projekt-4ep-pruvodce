using System.ComponentModel.DataAnnotations;

namespace PruvodceProject.Models
{
    /// <summary>
    /// Model pro ukládání dat o uživateli
    /// </summary>
    public class UserModel
    {
        [Key]
        public Guid ID { get; set; }
        [Required]
        public string? heslo { get; set; }
        [Required]
        public string prihlas_jmeno { get; set; } = String.Empty;
        [Required]
        public string mail { get; set; } = String.Empty;
        [Required]
        public string trida { get; set; } = String.Empty;
    }
}
