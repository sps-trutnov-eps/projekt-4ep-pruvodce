using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace PruvodceProject.Models
{
    /// <summary>
    /// Datový model pro uèebnu.
    /// Primárním klíèem je Id, které je typu GUID
    /// </summary>
    public class Ucebna
    {
        [Key]
        public Guid Id { get; set; }  

        [Required]
        public string patro { get; set; } = String.Empty;
        [Required]
        public string druh { get; set; } = String.Empty; //Urèuje, zda-li je uèebna odborná, èi všeobcená "kmenová"
    }
}