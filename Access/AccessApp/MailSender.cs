using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Web;

namespace AccessApp
{
    public static class MailSender
    {

        // Utilisation du WebService.
        public static string GetUserEmail(string username)
        {
            EpiService.MyServicesSoapClient client = new EpiService.MyServicesSoapClient();
            EpiService.UsrZimbra zimbra = new EpiService.UsrZimbra();
            try {
                zimbra = client.RtvUsrZimbra(username).iRet;
                return zimbra.mail[0].Replace("'",""); // ? // 
            } catch
            {
                // ne sera pas réel en prod...
                return "";
            }


        }

        public static string SendEmailToView(string pwd, string userName)
        {
             return System.Net.WebUtility.HtmlEncode(@"<p align='right'>Epicura, " + DateTime.Now.ToString("dd/MM/yyyy") + "</p>" +
                "<p>Madame, Monsieur, </p> " +
                "<p>Nous sommes heureux de vous accueillir  dans notre infrastructure informatique et nous vous communiquons ci-dessous les informations pratique concernant votre accès :</p> <br/>" +
                "<div style='padding-left:50px;'><table><tr><td><b>Nom d'utilisateur</td><td>:</b></td><td> " + userName + "</td></tr>" +
                "<tr><td><b>Mot de passe </td><td>:</td></b><td>" + pwd + "</td></tr>" +
                "<tr><td><b>Adresse mail   </td><td>   : </b></td><td>" + GetUserEmail(userName) + "</td></tr></table></div><br/>" +
                "<p>Lors de votre première entrèe  en session, le système vous invitera à changer immédiatement votre mot de passe, ceci afin de garantir la confidentialité de celui-ci ainsi que vos documents. Votre nouveau mot de passe doit comporter au minimum 6 caractères.</p>" +
                "<p style='color:red;'><b><i>N'oubliez pas de valider la charte en cliquant sur le lien reçu dans votre nouvelle boite mail EpiCURA pour finaliser votre demande d'accès.</b></i></p>" +
                "<p><u>Gardez votre mot de passe secret et n'autorisez personne à travailler sous votre identité.</u></p>" +
                "<p>Nous profitons également de la présente pour vous informer que nous mettons un support à votre disposition accessible via les informations suivantes de 7h15 à 16h30 :<p><br/>" +
                "<div style='padding-left:50px;'><table><tr><td>Hornu</td><td> :</td><td> 065 71 39 05 </td></tr>" +
                "<tr><td>Baudour/Ath/Beloeil</td><td> :</td><td> 065 76 81 95 </td></tr>" +
                "<tr><td>Site internet</td><td> : </td><td><a href='http://support.epicura.lan/'>http://support.epicura.lan/</a>  </td></tr></table></div><br/>" +
                "<p>Nous restons à votre disposition pour tout complément d'information.</p>" +
                "<p align='right'>Le service informatique</p>" +
                "<p>Cordialement.</p><hr/>");
        }

        public static void SendPwdPerEmail(string pwd, string mailAgent, string destResp, string newUserName, string fullNameUser, string refTicket)
        {

            System.Net.Mail.MailMessage _EMail = new System.Net.Mail.MailMessage();
            System.Net.Mail.SmtpClient _smtpServer = new System.Net.Mail.SmtpClient(Consts.CONST_EMAIL_SMTP_SERVER_HOST);

            _EMail.BodyEncoding = System.Text.Encoding.Default;
            _EMail.From = new System.Net.Mail.MailAddress(mailAgent, mailAgent);
            _EMail.Priority = System.Net.Mail.MailPriority.Normal;

            _EMail.Subject = refTicket + " - Information de compte : " + fullNameUser;

            _EMail.IsBodyHtml = true;
            _EMail.Body = System.Net.WebUtility.HtmlDecode(SendEmailToView(pwd, newUserName));
            
            //_EMail.To.Add(mailAgent);
            //_EMail.To.Add(destResp);
            _EMail.To.Add("yorick.lepape@epicura.be");
            

            _smtpServer.Send(_EMail);
        }
        
    }
}