using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace AccessApp
{
    public static class MailSender
    {
        public static void SendPwdPerEmail(string pwd, string destination, string user)
        {
            var from = new MailAddress("sylvain.fissiaux@epicura.be");
            var to = new MailAddress(destination);
            const string passwordFrom = "";

            const string subject = "Your EpiCURA Password";

            var smtp = new SmtpClient
            {
                Host = "mail.epicura.be",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(from.Address, passwordFrom)
            };

            using (var message = new MailMessage(from, to)
            {
                Subject = subject,
                Body = @"Your new password is : <b>" + pwd + "</b>" +
                "Your username : <b>" + user + "</b>",
                IsBodyHtml = true,

            })
            {
                smtp.Send(message);
            }
        }
    }
}