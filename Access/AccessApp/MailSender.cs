﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace AccessApp
{
    public static class MailSender
    {

        public static string SendEmailToView(string pwd, string userName)
        {
             return System.Net.WebUtility.HtmlEncode(@"<p align='right'>Epicura, " + DateTime.Now.ToString("dd/MM/yyyy") + "</p>" +
                "<p>Madame, Monsieur, </p> " +
                "<p>Nous sommes heureux de vous accueillir  dans notre infrastructure informatique et nous vous communiquons ci-dessous les informations pratique concernant votre accès :</p> <br/>" +
                "<table><tr><td><b>Nom d'utilisateur</td><td>:</b></td><td> " + userName + "</td></tr>" +
                "<tr><td><b>Mot de passe </td><td>:</td></b><td>" + pwd + "</td></tr>" +
                "<tr><td><b>Adresse mail   </td><td>   : </b></td><td>" + userName + "</td></tr></table><br/>" +
                "<p>Lors de votre première entrèe  en session, le système vous invitera à changer immédiatement votre mot de passe, ceci afin de garantir la confidentialité de celui-ci ainsi que vos document. Votre nouveau mot de passe doit comporter au minimum 6 caractères.</p>" +
                "<p style='color:red;'><b><i>N'oubliez pas de valider la charte en cliquant sur le lien reçu dans votre nouvelle boite mail EpiCURA pour finaliser votre demande d'accès.</b></i></p>" +
                "<p><u>Gardez votre mot de passe secret et n'autorisez personne à travailler sous votre identité.</u></p>" +
                "<p>Nous profitons également de la présente pour vous informer que nous mettons un support à votre disposition accessible via les informations de 7h15 à 16h30 :<p><br/>" +
                "<table><tr><td>Hornu</td><td> :</td><td> 065 71 39 05 </td></tr>" +
                "<tr><td>Baudour/Ath/Beloeil</td><td> :</td><td> 065 76 81 95 </td></tr>" +
                "<tr><td>Site internet</td><td> : </td><td> http://support.epicura.lan/ </td></tr></table><br/>" +
                "<p>Nous restons à votre disposition pour tout complément d'information.</p>" +
                "<p align='right'>Le service informatique</p>" +
                "<p>Cordialement.</p><hr/>");
        }

        public static void SendPwdPerEmail(string pwd, string expediteur, string destination, string newUserName, string usermail, string fullNameUser, string refTicket)
        {
            var from = new MailAddress("yorick.lepape@epicura.be");
            var to = new MailAddress("yorick-1996@hotmail.com");
            const string passwordFrom = "";

            string subject = refTicket + " - Information de compte : " + fullNameUser;

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

                Body = @"<p align='right'>Epicura, " + DateTime.Now.ToString("dd/MM/yyyy") + "</p>" +
                "<p>Madame, Monsieur, </p> " +
                "<p>Nous sommes heureux de vous accueillir  dans notre infrastructure informatique et nous vous communiquons ci-dessous les informations pratique concernant votre accès :</p> <br/>"+
                "<table><tr><td><b>Nom d'utilisateur</td><td>:</b></td><td> " + newUserName + "</td></tr>"+
                "<tr><td><b>Mot de passe </td><td>:</td></b><td>" + pwd + "</td></tr>" +
                "<tr><td><b>Adresse mail   </td><td>   : </b></td><td>" + usermail + "</td></tr></table><br/>" +
                "<p>Lors de votre première entrèe  en session, le système vous invitera à changer immédiatement votre mot de passe, ceci afin de garantir la confidentialité de celui-ci ainsi que vos document. Votre nouveau mot de passe doit comporter au minimum 6 caractères.</p>"+
                "<p style='color:red;'><b><i>N'oubliez pas de valider la charte en cliquant sur le lien reçu dans votre nouvelle boite mail EpiCURA pour finaliser votre demande d'accès.</b></i></p>"+
                "<p><u>Gardez votre mot de passe secret et n'autorisez personne à travailler sous votre identité.</u></p>"+
                "<p>Nous profitons également de la présente pour vous informer que nous mettons un support à votre disposition accessible via les informations de 7h15 à 16h30 :<p><br/>"+
                "<table><tr><td>Hornu</td><td> :</td><td> 065 71 39 05 </td></tr>" +
                "<tr><td>Baudour/Ath/Beloeil</td><td> :</td><td> 065 76 81 95 </td></tr>" +
                "<tr><td>Site internet</td><td> : </td><td> http://support.epicura.lan/ </td></tr></table></br>" +
                "<p>Nous restons à votre disposition pour tout complément d'information.</p>" +
                "<p align='right'>Le service informatique</p>" +
                "<p>Cordialement.</p>",
                IsBodyHtml = true,
               
            })
            {
                smtp.Send(message);
            }
        }
        
    }
}