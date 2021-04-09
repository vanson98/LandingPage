using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LandingPage.EmailService
{
    public interface IEmailSender
    {
        Task SendEmailAsync (Message message);
    }
}
