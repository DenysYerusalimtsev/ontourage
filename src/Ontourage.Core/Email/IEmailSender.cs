using System.Threading.Tasks;

namespace Ontourage.Core.Email
{
    public interface IEmailSender
    {
        Task SendEmail(string email, string subject, string message);
    }
}
