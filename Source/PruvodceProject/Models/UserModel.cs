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
        [Required]
        public bool jeAdmin { get; set; } = true;
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
        public int kod { get; set; }
        [Required]
        public DateTime expirace { get; set; } = DateTime.Now.AddMinutes(2);
    }
}
