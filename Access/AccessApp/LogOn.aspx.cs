using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows;

namespace AccessApp
{
    public partial class LogOn : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.cmdLogin.ServerClick += new System.EventHandler(this.cmdLogin_ServerClick);
        }

        private bool ValidateUser(string userName, string passWord)
        {
            DataSet _ds = DAL.SelectCredentialsFromAppParam();

            string Password = "";
            string UserName = "";
            
            foreach (DataRow dr in _ds.Tables[0].Rows)
            {
                // _ds contains two rows : password and username
                if(dr["VARIABLE"].ToString() == Consts.CONST_PARAM_EPITOOLS_USR)
                {
                    UserName = dr["VALUE"].ToString();
                } else
                {
                    Password = dr["VALUE"].ToString();
                }
            }

           

            if ((null == userName) || (0 == userName.Length))
            {    
                return false;
            }
 
            if ((null == passWord) || (0 == passWord.Length))
            {
                return false;
            }

            if (null == Password)
            {
                // Vous pouvez écrire ici les échecs de tentative de connexion dans le journal des événements, pour une sécurité accrue.
                return false;
            }
 
            return (0 == string.Compare(Password, passWord, false) && 0 == string.Compare(UserName, userName, false));

        }

        private void cmdLogin_ServerClick(object sender, System.EventArgs e)
        {
            //Vérifie les indentifiants sont correctes.
            if (ValidateUser(txtUserName.Text, txtUserPass.Text))
            {
                FormsAuthenticationTicket tkt;
                string cookiestr;
                HttpCookie ck;

                //Création d'une instance contenant la structure du ticket.
                tkt = new FormsAuthenticationTicket(1, txtUserName.Text, DateTime.Now, DateTime.Now.AddMinutes(1), true, "Authentification");
                //Crée une chaîne contenant un ticket d’authentification chiffré utilisable dans un cookie HTTP.
                cookiestr = FormsAuthentication.Encrypt(tkt);
                //Création d'une instance contenant le cookie à partir du ticket.
                ck = new HttpCookie(FormsAuthentication.FormsCookieName, cookiestr);
                //Définit la date et l'heure d'expiration du cookie.
                ck.Expires = tkt.Expiration;
                //Définit le chemin d'accès du cookie.
                ck.Path = FormsAuthentication.FormsCookiePath;
                //Ajoute le cookie.
                Response.Cookies.Add(ck);
                //Redirige vers la page demandée.
                FormsAuthentication.RedirectFromLoginPage(txtUserName.Text, false);
                //Variable de session
                Session["Username"] = txtUserName.Text;
            }
            else
            {
                //Redirige vers la page d'authentification.
                Response.Redirect("LogOn.aspx", true);
            }
        }
    }
}