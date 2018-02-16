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

            L_Body.Text = "<div class='responsive-table-line' style='margin:0px auto;max-width:700px;'><table class='table table-bordered table-condensed table-body-center' ><tbody>" +
                "<tr><td data-title='Référence'>" + ds.Tables[0].Rows[0]["REFERENCE"].ToString() + "</td></tr>" +
                "<tr><td data-title='Création de ticket'>" + ds.Tables[0].Rows[0]["START_TS"].ToString() + "</td></tr>" +
                "<tr><td data-title='Dernière mise à jour'>" + ds.Tables[0].Rows[0]["LAST_UPDATE_TS"].ToString() + "</td></tr>";

            if (ds.Tables[0].Rows[0]["RESOLUTION_TS"].ToString() == string.Empty)
                L_Body.Text += "<tr><td data-title='cloture du ticket'>Ticket non cloturé.</td></tr>";
            else
                L_Body.Text += "<tr><td data-title='Cloture du ticket'>" + ds.Tables[0].Rows[0]["RESOLUTION_TS"].ToString() + "</td></tr>";

            L_Body.Text += "<tr><td data-title='Appelant'> " + dsCaller.Tables[0].Rows[0]["FIRST_NAME"].ToString() + " " + dsCaller.Tables[0].Rows[0]["LAST_NAME"].ToString() + " </td></tr>" +
                "<tr><td data-title='Agent'> " + dsAgent.Tables[0].Rows[0]["FIRST_NAME"].ToString() + " " + dsAgent.Tables[0].Rows[0]["LAST_NAME"].ToString() + "</td></tr>"+
                "<tr><td data-title='Titre'>" + ds.Tables[0].Rows[0]["TITLE"].ToString() + "</td></tr>" +
                "<tr><td data-title='Nom de la machine'> ??? </td></tr>" +
                "<tr><td data-title='Description'><p align='justify'>" + ds.Tables[0].Rows[0]["DESCRIPTION"].ToString() + "</p></td></tr>";

            if (ds.Tables[0].Rows[0]["RESOLUTION"].ToString() == string.Empty)
                L_Body.Text += "<tr><td data-title='Solution'><font color='#1A7F09'><b>" + ds.Tables[0].Rows[0]["RESOLUTION"].ToString() + "</b></font></td></tr>";
            else
                L_Body.Text += "<tr><td data-title='Solution'><font color='#c03b44'><b>Pas de solution</b></font></td></tr>";

            L_Body.Text += "</tbody></table></div><hr/><h3 style='text-align:center;'>Commentaire</h3><hr/>";

            ds = DAL.SelectAllFromCommentaire(id);
            DataTable dt = ds.Tables[0];

            foreach (DataRow row in dt.Rows)
            {
                L_Body.Text += "<div class='jumbotron'>" + row["LOG"].ToString() + "</div>";
            }
        }
    }
}