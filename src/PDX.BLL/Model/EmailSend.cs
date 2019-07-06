using System;

namespace PDX.BLL.Model
{
    public class EmailSend
    {
        public string Subject{get;set;}
        public string Body{get;set;}
        public string Status{get;set;}
        public string UserName{get;set;}
        public string IpermitNumber{get;set;}
        public System.DateTime Date {get{return DateTime.Now;}}
        public string Source{get;set;}
        public string Link{get;set;}
        public EmailSend(string subject,string body,string status,string username,string IpermitNumber,string source="ipermit", string link=null)
        {
            this.Body=body;
            this.Subject = subject;
            this.Status = status;
            this.UserName = username;
            this.IpermitNumber = IpermitNumber;
            this.Source = source;
            this.Link = link;
        }

        
    }
}