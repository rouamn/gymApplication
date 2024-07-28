using GymApplication.Repository.Models.Dto;

namespace GymApplication.UtilityService
{
    public interface IEmailService
    {
        void SendEmail(EmailModel emailModel);

        void SendMail(MailData mailData);
    }
}
