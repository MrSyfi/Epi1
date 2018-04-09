using System;

namespace AccessApp
{
    public static class MailSender
    {

        // Utilisation du WebService.
        public static string GetUserEmail(string username, string firstName="", string lastName="")
        {
            EpiService.MyServicesSoapClient client = new EpiService.MyServicesSoapClient();
            EpiService.UsrZimbra zimbra = new EpiService.UsrZimbra();
            try
            {
                zimbra = client.RtvUsrZimbra(username).iRet;
                return zimbra.mail[0].Replace("'", ""); // ? // 
            }
            catch
            {
                // Si l'utilisateur n'existe pas dans le web service rtvurZimbra (et qu'il n'a donc pas encore d'adresse mail (?))
                // On en génère une, purement visuelle.
                string tmp =  firstName+"."+lastName+"@epicura.be";
                return RemoveAccent(tmp.ToLower().Replace(" ", ""));
                
            }


        }

        static string RemoveAccent(string info)
        {

            string nchaine = string.Empty;
            for (int i = 0; i < info.Length; i++)
            {
                byte k = (byte)info[i];
                if (k == 32) nchaine += " ";
                else if (k > 223 && k < 231) nchaine += "a";
                else if (k > 231 && k < 236) nchaine += "e";
                else if (k > 235 && k < 240) nchaine += "i";
                else if (k > 241 && k < 247) nchaine += "o";
                else if (k > 248 && k < 253) nchaine += "u";
                else if (k == 231) nchaine += "c";
                else nchaine += info[i];
            }

            return nchaine;
        }

        public static string SendEmailToView(string pwd, string userName, string firstName, string lastName)
        {
            return System.Net.WebUtility.HtmlEncode(string.Format(@"<span style='font-family:Verdana;font-size:14pt;'><p align='right' style='font-family:Helvetica;'>Epicura, {0} </p>" +
               "<p>Madame, Monsieur, </p> " +
               "<p>Nous sommes heureux de vous accueillir  dans notre infrastructure informatique et nous vous communiquons ci-dessous les informations pratique concernant votre accès :</p> <br/>" +
               "<div style='padding-left:50px;'><table><tr><td><b>Nom d'utilisateur</td><td>:</b></td><td> {1} </td></tr>" +
               "<tr><td><b>Mot de passe </td><td>:</td></b><td> {2} </td></tr>" +
               "<tr><td><b>Adresse mail   </td><td>   : </b></td><td> {3} </td></tr></table></div><br/>" +
               "<p>Lors de votre première entrèe  en session, le système vous invitera à changer immédiatement votre mot de passe, ceci afin de garantir la confidentialité de celui-ci ainsi que vos documents. Votre nouveau mot de passe doit comporter au minimum 8 caractères.</p>" +
               "<p style='color:red;'><b><i>N'oubliez pas de valider la charte en cliquant sur le lien reçu dans votre nouvelle boite mail EpiCURA pour finaliser votre demande d'accès.</b></i></p>" +
               "<p><u>Gardez votre mot de passe secret et n'autorisez personne à travailler sous votre identité.</u></p>" +
               "<p>Nous profitons également de la présente pour vous informer que nous mettons un support à votre disposition accessible via les informations suivantes de 7h15 à 17h30 :<p><br/>" +
               "<div style='padding-left:50px;'><table><tr><td>Téléphone</td><td> :</td><td> <b>065 76 90 10</b> </td></tr>" +
               "<tr><td>Site internet</td><td> : </td><td><a href='http://support.epicura.lan/'>http://support.epicura.lan/</a></td></tr></table></div><br/>" +
               "<p>Nous restons à votre disposition pour tout complément d'information.</p>" +
               "<p align='right'>Le service informatique</p>" +
               "<p>Cordialement.</p><hr/></span>", DateTime.Now.ToString("dd/MM/yyyy"), userName, pwd, GetUserEmail(userName,firstName,lastName)));
        }

        public static string SendObsolete(string EpiID, string Marque, string Modele, string NumSerie, string Agent)
        {
            return System.Net.WebUtility.HtmlEncode(string.Format(@"<span style='font-family:Verdana;font-size:14pt;'><p style='font-family:Helvetica;'><h2>EpiDESK - Information</h2><p>Ceci est un mail pour signifier le déclassement de ce matériel :</p>" +
                "<div style='padding-left:50px;'><p style='color: rgb(67, 130, 195); font-weight: bold;'>EpiId</p><p>{0}</p><br/>" +
                "<p style='color: rgb(67, 130, 195); font-weight: bold;'>Marque</p><p> {1}</p><br/>" +
                 "<p style='color: rgb(67, 130, 195); font-weight: bold;'>Modèle</p><p> {2}</p><br/>" +
                "<p style='color: rgb(67, 130, 195); font-weight: bold;'>Numéro de série</p><p> {3}</p></div><br/>" +
                "<p>Effectué par <b>{4}</b></p></p></span>", EpiID, Marque, Modele, NumSerie, Agent));
        }

        public static void SendObsoleteEmail(string RespMail, string AgentMail, string EpiID, string Marque, string Modele, string NumSerie, string Agent)
        {

            System.Net.Mail.MailMessage _EMail = new System.Net.Mail.MailMessage();
            System.Net.Mail.SmtpClient _smtpServer = new System.Net.Mail.SmtpClient(Consts.CONST_EMAIL_SMTP_SERVER_HOST);

            _EMail.BodyEncoding = System.Text.Encoding.Default;
            _EMail.From = new System.Net.Mail.MailAddress(AgentMail, AgentMail);
            _EMail.Priority = System.Net.Mail.MailPriority.Normal;

            _EMail.Subject = " Message automatique : Obsolescence de matériel ";

            _EMail.IsBodyHtml = true;
            _EMail.Body = System.Net.WebUtility.HtmlDecode(SendObsolete(EpiID, Marque, Modele, NumSerie, Agent));

            _EMail.To.Add(RespMail);
            

            _smtpServer.Send(_EMail);

        }

        public static void SendPwdPerEmail(string pwd, string mailAgent, string destResp, string newUserName, string firstName, string lastName, string refTicket, out bool sended)
        {

            System.Net.Mail.MailMessage _EMail = new System.Net.Mail.MailMessage();
            System.Net.Mail.SmtpClient _smtpServer = new System.Net.Mail.SmtpClient(Consts.CONST_EMAIL_SMTP_SERVER_HOST);

            _EMail.BodyEncoding = System.Text.Encoding.Default;
            _EMail.From = new System.Net.Mail.MailAddress(mailAgent, mailAgent);
            _EMail.Priority = System.Net.Mail.MailPriority.Normal;

            _EMail.Subject = refTicket + " - Information de compte : " + firstName + " " + lastName;

            _EMail.IsBodyHtml = true;
            _EMail.Body = System.Net.WebUtility.HtmlDecode(SendEmailToView(pwd, newUserName, firstName, lastName));

            _EMail.To.Add(mailAgent);
            _EMail.To.Add(destResp);
           

            try
            {
                _smtpServer.Send(_EMail);
                sended = true;
            }
            catch
            {
                sended = false;
            }
        }

    }
}