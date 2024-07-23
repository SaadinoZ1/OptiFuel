namespace OptiFuel.Models
{
    public class Planning
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Center { get; set; }
        public int QuantiteALivrer { get; set; }


        public BonDeLivraison BonDeLivraison { get; set; }
        public Certificat Certificat { get; set; }

    }
}
