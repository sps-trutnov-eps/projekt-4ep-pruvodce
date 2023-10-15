using System.ComponentModel.DataAnnotations;

namespace PruvodceProject.Models
{
    public class PhotoModel
    {
        [Key] 
        public Guid Id { get; set; }
        [Required]
        public string Nazev { get; set; }
        [Required]
        public byte[] DataPhoto { get; set; } //Tomuto úplně nerozumím
        /*Chybí k fotce
        [Required]
        public string Cesta { get; set; }
        */
        [Required]
        public string Pripona { get; set; }

        [Required]
        public Guid BudovaID { get; set; }  //poslední dva řádky kvůli vazbě 1:N (1 budova: N fotkám) V.K.
        public BudovyModel IdBudovy { get; set; } 
    }
}
