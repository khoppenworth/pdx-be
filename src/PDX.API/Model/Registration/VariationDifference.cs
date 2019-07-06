namespace PDX.API.Model.Registration
{
    public class VariationDifference {
        public VariationDifference (PDX.BLL.Model.Difference difference) {
            this.propertyName = difference.PropertyName;
            this.oldValue = difference.OldValue;
            this.newValue = difference.NewValue;

        }
        public string propertyName { get; set; }
        public object oldValue { get; set; }
        public object newValue { get; set; }
    }
}