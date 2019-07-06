namespace PDX.API.Model.Registration
{
    public class MDModelSize{
         public MDModelSize (PDX.Domain.Commodity.MDModelSize mdModelSize) {
            this.model = mdModelSize.Model;
            this.size = mdModelSize.Size;
            this.presentation = mdModelSize.Presentation;

        }
        public string model { get; set; }
        public string size { get; set; }
        public string presentation { get; set; }
    }
}