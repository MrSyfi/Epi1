using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AccessApp
{
    public partial class DisplayTicket : System.Web.UI.Page
    {
        

        protected void Page_Load(object sender, EventArgs e)
        {
           
            // Check if tId exists
            if (Request.QueryString.AllKeys.Contains("tId"))
            {
                // Check if tId is not null
                if(Request.QueryString["tId"] == string.Empty)
                {
                    Response.Redirect("GestionMateriel.aspx", true);
                }
                else
                {
                    LoadData(Request.QueryString["tId"]);
                }
            } else
            {
                Response.Redirect("GestionMateriel.aspx", true);
            }

        }

        private void LoadData(string id)
        {
            DataSet ds = DAL.SelectAllFromTicketId(id);
            DataSet dsAgent = DAL.SelectAgentIdentity(id);
            DataSet dsCaller = DAL.SelectCallerIdentity(id);

            string machineName = ds.Tables[0].Rows[0]["PRODUCT"].ToString();

       

            //AFFICHAGE DU TABLEAU D'INFORMATIONS
            L_Body.Text = "<div class='responsive-table-line' style='margin:0px auto;max-width:700px;'><table class='table table-bordered table-condensed table-body-center' ><tbody>" +
                "<tr><td data-title='Référence'><p align='justify'>" + ds.Tables[0].Rows[0]["REFERENCE"].ToString() + "</p></td></tr>" +
                "<tr><td data-title='Création de ticket'><p align='justify'>" + ds.Tables[0].Rows[0]["START_TS"].ToString() + "</p></td></tr>" +
                "<tr><td data-title='Dernière mise à jour'><p align='justify'>" + ds.Tables[0].Rows[0]["LAST_UPDATE_TS"].ToString() + "</p></td></tr>";

            if (ds.Tables[0].Rows[0]["RESOLUTION_TS"].ToString() == string.Empty)
                L_Body.Text += "<tr><td data-title='cloture du ticket'><p align='justify'>Ticket non cloturé.</p></td></tr>";
            else
                L_Body.Text += "<tr><td data-title='Cloture du ticket'><p align='justify'>" + ds.Tables[0].Rows[0]["RESOLUTION_TS"].ToString() + "</p></td></tr>";

            L_Body.Text += "<tr><td data-title='Appelant'><p align='justify'>" + dsCaller.Tables[0].Rows[0]["FIRST_NAME"].ToString() + " " + dsCaller.Tables[0].Rows[0]["LAST_NAME"].ToString() + "</p></td></tr>" +
                "<tr><td data-title='Agent'><p align='justify'>" + dsAgent.Tables[0].Rows[0]["FIRST_NAME"].ToString() + " " + dsAgent.Tables[0].Rows[0]["LAST_NAME"].ToString() + "</p></td></tr>" +
                "<tr><td data-title='Titre'><p align='justify'>" + ds.Tables[0].Rows[0]["TITLE"].ToString() + "</p></td></tr>" +
                "<tr><td data-title='Nom de la machine'><p align='justify'>";

            //Gestion de la taille d'un seul mot
            if (machineName.EndsWith(".EPICURA.LAN"))
                L_Body.Text += machineName.TrimEnd(".EPICURA.LAN".ToCharArray()) + "<BR/>.EPICURA.LAN";
            else if (machineName.EndsWith(".RHMS.BE"))
                L_Body.Text += machineName.TrimEnd(".RHMS.BE".ToCharArray()) + "<br/>.RHMS.BE";
            else if (machineName.EndsWith(".CHHF.LOCAL"))
                L_Body.Text += machineName.TrimEnd(".CHHF.LOCAL".ToCharArray()) + "<br/>.CHHF.LOCAL";
            else
                L_Body.Text += machineName;

            L_Body.Text += "</p></td></tr><tr><td data-title='Description'><p align='justify'>" + ds.Tables[0].Rows[0]["DESCRIPTION"].ToString() + "</p></td></tr>";

            if (ds.Tables[0].Rows[0]["RESOLUTION"].ToString() == string.Empty)
                L_Body.Text += "<tr><td data-title='Solution'><font color='#c03b44'><b>Pas de solution</b></font></td></tr>";
            else
                L_Body.Text += "<tr><td data-title='Solution'><p align='justify'><font color='#1A7F09'><b>" + ds.Tables[0].Rows[0]["RESOLUTION"].ToString() + "</b></font></p></td></tr>";

            L_Body.Text += "</tbody></table></div><hr/><h3 style='text-align:center;'>Commentaire</h3><hr/>";


            //AFFICHAGE DES COMMENTAIRES
            ds = DAL.SelectAllFromCommentaire(id);
            DataTable dt = ds.Tables[0];

            foreach (DataRow row in dt.Rows)
            {
                ds = DAL.SelectContact(row["CONTACT_ID"].ToString());
                L_Body.Text += "<div class='jumbotron'><p><table width=100% style='border-bottom: 2px solid #a5a1a1; font-size: 18px;'><tr><td align='left' width=50%>" + ds.Tables[0].Rows[0]["LAST_NAME"].ToString() +" "+ ds.Tables[0].Rows[0]["FIRST_NAME"].ToString() + "</td><td align='right'>" + row["TIMESTAMP"].ToString() +"</td></tr></table></p>" +
                    "<p align='justify' style='font-size: 15px'> " + row["LOG"].ToString() + "</p></div>";
            }

        }
     
    }
}