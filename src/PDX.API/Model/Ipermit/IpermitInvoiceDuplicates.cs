namespace PDX.API.Model.Ipermit
{
    public class IpermitInvoiceDuplicates
    {
        public int? ImportPermitID { get; set; }
        public int SupplierID { get; set; }
        public int AgentID { get; set; }
        public string PerformaInvoiceNumber { get; set; }
    }
}