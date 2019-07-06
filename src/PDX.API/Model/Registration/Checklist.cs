namespace PDX.API.Model.Registration
{
    public class Checklist {
        public Checklist (PDX.Domain.Catalog.Checklist checklist) {
            this.label = checklist.Label;
            this.name = checklist.Name;

        }
        public string label { get; set; }
        public string name { get; set; }
    }
}