using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace PruvodceProject.Models
{
    /// <summary>
    /// Datov� model pro u�ebnu.
    /// Prim�rn�m kl��em je Id, kter� je typu GUID
    /// </summary>
    public class Ucebna
    {
        [Key]
        public Guid Id { get; set; }  
        [Required]
        public string patro { get; set; } = String.Empty; //Patro v budovy, na kter�m se dan� u�ebna nach�z�
        [Required]
        public string druh { get; set; } = String.Empty; //Ur�uje, zda-li je u�ebna odborn�, �i v�eobcen� "kmenov�"
        [Required] // Učebna má 1 budovu a 1 budova má více učeben.
        public int budovaID { get; set; }
        
    }
}