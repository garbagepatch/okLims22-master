
using okLims.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace okLims.Extensions
{

    public static class EmailSenderExtensions
    {
        public static Task SendEmailConfirmationAsync(this IEmailSender emailSender, string email, string link)
        {
            return emailSender.SendEmailAsync(email, "Confirm your email",
                $"Please confirm your account by clicking this link: <a href='{HtmlEncoder.Default.Encode(link)}'>link</a>");
        }
        public static Task SendEmailOnRequestAsync(this IEmailSender emailSender, string email)
        {
            return emailSender.SendEmailAsync(email, "Request Submitted", "A new request was submitted");
        }
        public static Task SendEmailOnCompletion(this IEmailSender emailSender, string email)
        {
            return emailSender.SendEmailAsync(email, "Request Completed", "Your bioreactor/instrumentation request is complete");
        }
    }
}
