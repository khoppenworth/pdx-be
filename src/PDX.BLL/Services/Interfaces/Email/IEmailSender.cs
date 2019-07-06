using System.Threading.Tasks;
using PDX.BLL.Model;
using PDX.Domain.Account;

namespace PDX.BLL.Services.Interfaces.Email
{
    public interface IEmailSender
    {
        Task SendEmailAsync(EmailSend emailSendObject,User user, string template = "APR");
    }
}