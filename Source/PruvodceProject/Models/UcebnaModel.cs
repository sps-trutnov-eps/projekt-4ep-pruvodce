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
        public string patro { get; set; } = String.Empty; //Patro v budovy, na kterém se daná uèebna nachází
        [Required]
        public string druh { get; set; } = String.Empty; //Urèuje, zda-li je uèebna odborná, èi všeobcená "kmenová"
        //Napojení na budovu (jedna tøída mùže být jen na jedné budovì)
        public ICollection<PhotoModel> fotky { get; set; } //vazba na foto 1:N
    }
}