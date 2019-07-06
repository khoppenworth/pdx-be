using System;
using System.Collections.Generic;
using System.Linq;
using PDX.Domain.Document;
using PDX.Domain.Procurement;

namespace PDX.API.Model.Ipermit
{
    public class IpermitPDFData
    {
        public IpermitPDFData()
        {

        }
        public IpermitPDFData(ImportPermit ipermit, LetterHeading letterHeading, Letter letter, string comment = null)
        {
            this.ipermitId = ipermit.ID;
            this.company = letterHeading.CompanyName;
            this.logoUrl = letterHeading.LogoUrl;
            this.header = letter.Title;
            this.address = ipermit.Agent?.Address?.City;
            this.to = ipermit.Agent?.Name;
            this.date = DateTime.Now.ToString();
            this.body = letter.Body;
            this.submissionDate = ipermit.CreatedDate.ToString();
            this.applicationNumber = ipermit.ImportPermitNumber;
            this.totalPrice = String.Format("{0:n2}", ipermit.Amount);
            this.shippingMethod = ipermit.ShippingMethod.Name;
            this.delivery = ipermit.Delivery;
            this.freight = String.Format("{0:n2}", ipermit.FreightCost);
            this.insurance = ipermit.Insurance != null ? String.Format("{0:n2}", ipermit.Insurance) : "0";
            this.discount = ipermit.Discount != null ? String.Format("{0:n2}", ipermit.Discount) : "0";
            this.comment = ipermit.Remark;
            this.portOfEntry = ipermit.PortOfEntry.Name;
            this.paymentMode = ipermit.PaymentMode.Name;
            this.performaInvoiceNumber = ipermit.PerformaInvoiceNumber;
            this.fileName = letter.ModuleDocument.DocumentType.Name + ".pdf";
            this.tempFolderName = ipermit.RowGuid.ToString();
            this.rejectedDate = ipermit.ImportPermitStatus.CreatedDate.ToString();
            this.approvedDate = ipermit.ImportPermitStatus.CreatedDate.ToString();
            this.expiryDate = ipermit.ImportPermitStatus.CreatedDate.ToString();
            this.currency = ipermit.Currency?.Symbol;
            this.currencyName = ipermit.Currency?.Name;
            this.items = ipermit.ImportPermitDetails.ToList().Select(d => new IpermitPDFDataDetail(d)).ToList();
        }
        public int ipermitId { get; set; }
        public string company { get; set; }
        public string logoUrl { get; set; }
        public string header { get; set; }
        public string address { get; set; }
        public string to { get; set; }
        public string date { get; set; }
        public string body { get; set; }
        public string submissionDate { get; set; }
        public string applicationNumber { get; set; }
        public string totalPrice { get; set; }
        public string price { get; set; }
        public string shippingMethod { get; set; }
        public string delivery { get; set; }
        public string freight { get; set; }
        public string insurance { get; set; }
        public string discount { get; set; }
        public string comment { get; set; }
        public string reason { get; set; }
        public string portOfEntry { get; set; }
        public string paymentMode { get; set; }
        public string performaInvoiceNumber { get; set; }
        public string tempFolderName { get; set; }
        public string fileName { get; set; }
        public string rejectedDate { get; set; }
        public string approvedDate { get; set; }
        public string expiryDate { get; set; }
        private string currency{ get; set; }
        private string currencyName{ get; set; }

        public List<IpermitPDFDataDetail> items { get; set; }
    }

    public class IpermitPDFDataDetail
    {
        public IpermitPDFDataDetail()
        {

        }
        public IpermitPDFDataDetail(ImportPermitDetail ipermitDetail)
        {
            this.product = ipermitDetail.Product.BrandName;
            this.productfullname = ipermitDetail.Product.GenericName + " - " + ipermitDetail.Product.DosageStrength + " - " + ipermitDetail.Product.DosageUnit + " - " + ipermitDetail.Product.DosageForm;
            // use different productfullname format for Medical Devices
            if(ipermitDetail.Product.ProductType?.ProductTypeCode == "MDS") {
                this.productfullname = ipermitDetail.Product.GenericName + " - " + ipermitDetail.MDDevicePresentation.Model + " - " + ipermitDetail.MDDevicePresentation.Size + " - " + ipermitDetail.Product.Description;
            }
            this.manufacturer = ipermitDetail.ManufacturerAddress.Manufacturer.Name;
            this.site = ipermitDetail.ManufacturerAddress.Manufacturer.Site;
            this.country = ipermitDetail.ManufacturerAddress.Address.Country?.Name;
            this.quantity = String.Format("{0:n2}", ipermitDetail.Quantity);
            this.unitprice = String.Format("{0:n2}", ipermitDetail.UnitPrice);
            this.discount = ipermitDetail.Discount != null ? String.Format("{0:n2}", ipermitDetail.Discount) : "0";
            this.currency = ipermitDetail.ImportPermit.Currency?.Symbol;
            // use MD Presentation for Medical Devices
            this.presentation = ipermitDetail.Product.ProductType?.ProductTypeCode == "MDS" ? ipermitDetail.MDDevicePresentation.PackSize.Name : ipermitDetail.Product.Presentation;
            this.shelfLife = ipermitDetail.Product.ShelfLife;
            this.totalamount = String.Format("{0:n2}", ((ipermitDetail.UnitPrice * ipermitDetail.Quantity) - (ipermitDetail.Discount != null ? (decimal)ipermitDetail.Discount : 0)));
            this.productstatus = ipermitDetail.Product.ProductStatus;
        }
        public string product { get; set; }
        public string productfullname { get; set; }
        public string manufacturer { get; set; }
        public string site { get; set; }
        public string country { get; set; }
        public string quantity { get; set; }
        public string unitprice { get; set; }
        public string discount { get; set; }
        public string currency { get; set; }
        public string presentation { get; set; }
        public string shelfLife { get; set; }
        public string totalamount { get; set; }
        public string productstatus { get; set; }

    }

}