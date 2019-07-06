namespace PDX.API.Model.Registration
{
    public class Ingredient {
        public Ingredient (PDX.Domain.Commodity.ProductComposition composition) {
            this.name = composition.INN != null ? composition.INN.Name : composition.Excipient.Name;
            this.dosageStrength = composition.DosageStrengthObj?.Name;
            this.dosageUnit = composition.DosageUnit?.Name;
            this.standard = composition.PharmacopoeiaStandard?.Name;
        }
        public string name { get; set; }
        public string dosageStrength { get; set; }
        public string dosageUnit { get; set; }
        public string standard { get; set; }
    }
}