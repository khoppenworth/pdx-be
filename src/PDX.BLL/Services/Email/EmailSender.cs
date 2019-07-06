using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using PDX.BLL.Helpers;
using PDX.BLL.Model;
using PDX.BLL.Model.Config;
using PDX.BLL.Services.Interfaces.Email;
using PDX.BLL.Services.Notification;
using PDX.Domain.Account;

namespace PDX.BLL.Services.Email {
    public class EmailSender : IEmailSender {
      
        private readonly NotificationFactory _notificationFacory;
        public EmailSender (NotificationFactory notificationFacory) {

            _notificationFacory = notificationFacory;
        }
        public async Task SendEmailAsync (EmailSend emailSendObject,User user, string template = "APR") {
            emailSendObject.Source = emailSendObject.Source == "ipermit" ? "Order" : "Application";
            var notifier = _notificationFacory.GetNotification(NotificationType.EMAIL);
            if(template=="IPAR"){
                 emailSendObject.Link = emailSendObject.Body;
                //await notifier.Notify(user,emailSendObject,"IPAR");
            }
            else{
                //await notifier.Notify(user,emailSendObject,"APR");
            }
        }
    }
}