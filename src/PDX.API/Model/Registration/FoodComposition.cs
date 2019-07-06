namespace PDX.API.Model.Registration
{
    public class FoodComposition{
         public FoodComposition (PDX.Domain.Commodity.FoodComposition composition) {
            this.name = composition.Name;
            this.amount = composition.Amount;
            this.unitFunction = composition.UnitFunction;
            this.compositionType = composition.CompositionType;

        }
        public string name { get; set; }
        public decimal amount { get; set; }
        public string unitFunction { get; set; }
        public string compositionType { get; set; }
    }
}