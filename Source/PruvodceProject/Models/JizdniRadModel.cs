namespace PruvodceProject.Models
{
    public class JizdniRadModel
    {
        public Guid Id { get; set; }
        public List<ZastavkaModel>? Zastavka { get; set; }
        public int CisloSpoje { get; set; }
        public string? TypDopravy { get; set; }
    }
}
