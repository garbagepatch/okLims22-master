using Microsoft.AspNetCore.Mvc;
using okLims.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace okLims.Extensions
{
    public static class UrlHelperExtensions
    {
        public static string EmailConfirmationLink(this IUrlHelper urlHelper, string userId, string code, string scheme)
        {
            return urlHelper.Action(
                action: nameof(AccountController.ConfirmEmail),
                controller: "Account",
                values: new { userId, code },
                protocol: scheme);
        }

        public static string ResetPasswordCallbackLink(this IUrlHelper urlHelper, string userId, string code, string scheme)
        {
            return urlHelper.Action(
                action: nameof(AccountController.ResetPassword),
                controller: "Account",
                values: new { userId, code },
                protocol: scheme);
        }
       // public static string EmailOnCompletion(this IUrlHelper urlHelper, string requesterEmail, string scheme)
        //{
          //  return urlHelper.Action(
           //     action: nameof(RequestController.EmailerOnCompletion),
          //      controller: "Request",
            //    values: new { requesterEmail });
        //}
    }

}
