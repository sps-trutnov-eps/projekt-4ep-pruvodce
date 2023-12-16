using System.ComponentModel.DataAnnotations;

namespace PruvodceProject.Models
{
    /// <summary>
    /// Model pro ukládání dat o uživateli
    /// </summary>
    public class UzivatelModel
    {
        [Key] public Guid Id { get; set; }
        [Required] public string Heslo { get; set; }
        [Required] public string Mail { get; set; }
        public string Trida { get; set; } = String.Empty;
        [Required] public bool JeAdmin { get; set; } = false;
    }

    public class UzivatelOvereni
    {
        [Key] public Guid Id { get; set; }
        [Required] public string Mail { get; set; }
        [Required] public string Heslo { get; set; }
        public string Trida { get; set; } = String.Empty; 
        [Required] public int KodOvereni { get; set; }
        [Required] public DateTime ExpiraceOvereni { get; set; } = DateTime.Now.AddMinutes(2);
    }
}
