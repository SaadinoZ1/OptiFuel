namespace OptiFuel.Models
{
    public class BonDeLivraison
    {
        public int Id { get; set; }
        public int PlanningId { get; set; }
        public byte [] BLFile { get; set; }
        public int QuantitésLivrée { get; set; }
        public DateTime DateValidation { get; set; }
        public Planning Planning { get; set; }
    }
}
