using System.ComponentModel.DataAnnotations;

namespace PruvodceProject.Models
{
    /// <summary>
    /// Model pro ukládání dat o uživateli
    /// </summary>
    public class UserModel
    {
        [Key]
        public string? heslo { get; set; } //Udělat z hesla klíč databáze není dobrý nápad!!!
        [Required]
        public string prihlas_jmeno { get; set; } = String.Empty;
        [Required]
        public string mail { get; set; } = String.Empty;
        [Required]
        public string trida { get; set; } = String.Empty;
    }
}
