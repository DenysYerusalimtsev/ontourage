using System.Threading.Tasks;
using MailKit;
using Microsoft.AspNetCore.Mvc;
using Ontourage.Core.Email;

namespace Ontourage.Web.Controllers
{
    public class EmailController : Controller
    {
        private IEmailSender _emailSender;

        public EmailController(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

       
    }
}