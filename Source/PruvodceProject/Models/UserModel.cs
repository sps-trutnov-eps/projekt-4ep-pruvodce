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
        public string heslo { get; set; }
        [Required]
        public string mail { get; set; }

        public string trida { get; set; } = String.Empty;
    }

    public class UserVerify
    {
        [Key]
        public Guid ID { get; set; }
        [Required]
        public string mail { get; set; }
        [Required]
        public string heslo { get; set; }
        
        public string trida { get; set; } = String.Empty;

        [Required]
        public string kod { get; set; } = new Random().Next(1000000, 9999999).ToString();
        [Required]
        public DateTime expirace { get; set; }
    }
}
