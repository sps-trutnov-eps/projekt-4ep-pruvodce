using System.ComponentModel.DataAnnotations;

namespace PruvodceProject.Models
{
    public class FotoModelBudovy
    {
        [Key] public Guid Id { get; set; }
        [Required] public string Nazev { get; set; }
        [Required] public string Cesta { get; set; }
        [Required] public string Pripona { get; set; }
        public Guid BudovaID { get; set; }  //dva řádky kvůli vazbě 1:N (1 budova: N fotkám) V.K.
        public BudovyModel Budova { get; set; }
    }
    public class FotoModelUcebny
    {
        [Key] public Guid Id { get; set; }
        [Required] public string Nazev { get; set; }
        [Required] public string Cesta { get; set; }
        [Required] public string Pripona { get; set; }
        public Guid UcebnaID { get; set; }  //tady ty další jsou zas kvůli vazbě 1:N (1 učebna: N fotkám) V.K.
        public UcebnaModel Ucebna { get; set; }
    }
}
