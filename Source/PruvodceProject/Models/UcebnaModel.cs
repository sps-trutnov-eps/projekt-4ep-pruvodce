using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace PruvodceProject.Models
{
    /// <summary>
    /// Datov� model pro u�ebnu.
    /// Prim�rn�m kl��em je Id, kter� je typu GUID
    /// </summary>
    public class UcebnaModel
    {
        [Key]
        public Guid Id { get; set; }  

        /// <summary>
        /// Kvůli interakčnímu bodu
        /// </summary>
        [Required]
        public string Nazev { get; set; }  
        [Required]
        public string Patro { get; set; } = String.Empty; //Patro v budovy, na kter�m se dan� u�ebna nach�z�
        [Required]
        public string Druh { get; set; } = String.Empty; //Ur�uje, zda-li je u�ebna odborn�, �i v�eobcen� "kmenov�"
        public string Poddruh {  get; set; } = String.Empty;
        public BudovyModel Budova { get; set; }

        [Required]
        public Guid BudovaID { get; set; }
        
        //Napojen� na budovu (jedna t��da m��e b�t jen na jedn� budov�)
        public ICollection<PhotoModelUcebny> fotky { get; set; } //vazba na foto 1:N
    }
}