using Microsoft.AspNetCore.Mvc;
using PDX.Domain.Common;
using PDX.Domain.Document;
using PDX.BLL.Services.Interfaces;
using PDX.BLL.Services.Interfaces.Email;
using System.Threading.Tasks;
using Microsoft.AspNetCore.NodeServices;
using Newtonsoft.Json;
using PDX.BLL.Model;
using PDX.API.Model.Ipermit;
using PDX.API.Services;
using PDX.API.Services.Interfaces;
using System.Collections.Generic;
using PDX.Logging.Models;
using System.Linq;
using PDX.Domain.Account;
using PDX.BLL.Services.Notification;
using System;

namespace PDX.API.Controllers
{

    [Route("api/[controller]")]
    public class CurrencyController : CrudBaseController<Currency>
    {
        private readonly IService<LetterHeading> _letterHeadingService;
        private readonly IService<Letter> _letterService;
        private readonly NotificationFactory _notificationFactory;
        private readonly IGenerateDocuments _generateDocument;
        private readonly PDX.Logging.ILogger _logger;

        public CurrencyController(IService<Currency> currencyService, IService<LetterHeading> letterHeadingService,
        IService<Letter> letterService, NotificationFactory notificationFactory, IGenerateDocuments generateDocument, PDX.Logging.ILogger logger)
        : base(currencyService)
        {
            _letterHeadingService = letterHeadingService;
            _letterService = letterService;
            _notificationFactory = notificationFactory;
            _generateDocument = generateDocument;
            _logger = logger;
        }
        [HttpGet]
        [Route("Node")]
        public async Task<IActionResult> MyAction([FromServices] INodeServices nodeServices)
        {
            var filePath = "./external/templates/registration/firRequest.html";

            var letterHeading = await _letterHeadingService.GetAsync(1);
            var letter = (await _letterService.GetAsync(x => x.ModuleDocument.DocumentType.DocumentTypeCode == "PORR"));

            var data = new IpermitPDFData()
            {
                company = letterHeading.CompanyName,
                logoUrl = letterHeading.LogoUrl,
                header = letter.Title,//"PURCHASE ORDER APPLICATION ACKNOWLEDGMENT LETTER",
                address = "Nifas silk Lafto subcity, wored 06, H.No 803 ETHIOPIA",
                to = "HOSAM PHARMACEUTICALS TRADING",
                date = "3/28/17 11:35 AM",
                body = letter.Body,//"Your purchase order application for the below products have been accepted for evaluation. It is anticipated that the evaluation will be completed by approximately 3 days from the date of submission. The anticipated date of completion of the evaluation has been provided for your convenience and it is an estimate only",
                submissionDate = "March 28, 2017",
                applicationNumber = "00018/IP/2017",
                totalPrice = "6750",
                price = "500",
                shippingMethod = "By Air",
                delivery = "90 days",
                freight = "0",
                comment = "Freightcost to be paid at sight",
                portOfEntry = "Addis Ababa Bole air port",
                paymentMode = "L/c at site",
                performaInvoiceNumber = "0015/2016-17",
                tempFolderName = "5c5c90ba-dcf0-4152-979e-bedb249c9a46",
                fileName = "Approve.pdf",
                items = new List<IpermitPDFDataDetail> {
                    new IpermitPDFDataDetail{ product = "OXOL",manufacturer="VENUS REMEDIES LTD",site="HILL TOP INDUSTRIAL ESTATE, JHARMAJIRI EPIP,PHSEI,BHATOLI KALAN,BADDI DIST SOLAN (H.P.)",country="India", quantity = "1", unitprice = "1000",currency="USD"},
                    new IpermitPDFDataDetail{ product = "OXOL",manufacturer="VENUS REMEDIES LTD",site="HILL TOP INDUSTRIAL ESTATE, JHARMAJIRI EPIP,PHSEI,BHATOLI KALAN,BADDI DIST SOLAN (H.P.)",country="India", quantity = "4", unitprice = "1000",currency="USD"},
                    new IpermitPDFDataDetail{ product = "OXOL",manufacturer="VENUS REMEDIES LTD",site="HILL TOP INDUSTRIAL ESTATE, JHARMAJIRI EPIP,PHSEI,BHATOLI KALAN,BADDI DIST SOLAN (H.P.)",country="India", quantity = "3", unitprice = "1000",currency="USD"},
                }
            };
            //await _generateDocument.GeneratePDFDocument(filePath, data, nodeServices);
            var htmlString = string.Join(" ", System.IO.File.ReadAllLines(filePath));

            var result = await nodeServices.InvokeAsync<byte[]>("./external/js/pdf", htmlString, data);
            HttpContext.Response.ContentType = "application/pdf";

            string filename = @"report.pdf";
            HttpContext.Response.Headers.Add("x-filename", filename);
            HttpContext.Response.Headers.Add("Access-Control-Expose-Headers", "x-filename");
            HttpContext.Response.Body.Write(result, 0, result.Length);
            return new ContentResult();
        }

        [HttpGet]
        [Route("Node/Posts")]
        public async Task<IActionResult> NodePosts([FromServices] INodeServices nodeServices)
        {
            var filePath = "./external/templates/posts.html";

            var data = new
            {
                page_title = "Posts",
                posts = new object[]{
                    new {id = 1, title = "title", body= "body"}
                },
                people = new object[]{
                    new{name="Yehuda Katz"},
                    new{name="Alan Johnson"},
                    new{name="Charles Jolley"}
                }
            };

            var htmlString = string.Join(" ", System.IO.File.ReadAllLines(filePath));

            var result = await nodeServices.InvokeAsync<byte[]>("./external/js/pdf", htmlString, data);
            HttpContext.Response.ContentType = "application/pdf";

            string filename = @"report.pdf";
            HttpContext.Response.Headers.Add("x-filename", filename);
            HttpContext.Response.Headers.Add("Access-Control-Expose-Headers", "x-filename");
            HttpContext.Response.Body.Write(result, 0, result.Length);
            return new ContentResult();
        }

        [HttpGet]
        [Route("Email")]
        public async Task SendEmailAsync()
        {
            var subject = "Import Permit Approved";
            var message = "Your import permit has been approved";
            var user = new User()
            {
                ID = 1063,
                FirstName = "Test",
                LastName = "Test Last",
                Username = "testuser 1",
                Email = "test_email@domain.com",
                RowGuid = new System.Guid("0769d183-5e0d-11e7-bbae-40b0340e5ce6")
            };

            string ipermitNumber = "01295/IP/2018";
            IList<User> users = new List<User> { user };

            var emailNotifier = _notificationFactory.GetNotification(NotificationType.EMAIL);
            var pushNotifier = _notificationFactory.GetNotification(NotificationType.PUSHNOTIFICATION);


            await emailNotifier.Notify(users, new EmailSend(subject, message, "APR", users.FirstOrDefault().Username, ipermitNumber, "Order"), "APR");
            //push notification
            var body = String.Format(AlertTemplates.MarketAuthorizationStatusChange, "APR", ipermitNumber, DateTime.Now);
            await pushNotifier.Notify(users, new { body = body, subject = subject }, "IPSC");


        }

        [HttpGet]
        [Route("Log")]
        public async Task LogToFileAsync()
        {
            _logger.Log(LogType.Info, "Logging information from test controller");
            try
            {
                throw new System.Exception();
            }
            catch (System.Exception ex)
            {
                _logger.Log(ex);
            }
        }

    }
}
