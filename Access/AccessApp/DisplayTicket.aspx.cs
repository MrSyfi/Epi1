using System;
using System.Data;
using System.Linq;

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
                if (Request.QueryString["tId"] == string.Empty)
                {
                    Response.Redirect("GestionMateriel.aspx", true);
                }
                else
                {
                    try
                    {
                        LoadData(Request.QueryString["tId"]);
                    }
                    catch
                    {
                        EmptyTable();
                    }
                }
            }
            else
            {
                Response.Redirect("GestionMateriel.aspx", true);
            }

        }

        private void EmptyTable()
        {
            L_Body.Text = "<div class='responsive-table-line' style='margin:0px auto;max-width:700px;'><table class='table table-bordered table-condensed table-body-center' ><tbody>" +
            "<tr><td data-title='Référence'>" + Consts.EMPTY_STRING + "</td></tr>" +
            "<tr><td data-title='Création de ticket'>" + Consts.EMPTY_STRING + "</td></tr>" +
            "<tr><td data-title='Dernière mise à jour'>" + Consts.EMPTY_STRING + "</td></tr>" +
            "<tr><td data-title ='Cloture du ticket' >" + Consts.EMPTY_STRING + "</td></tr> " +
            "<tr><td data-title='Appelant'> " + Consts.EMPTY_STRING + " </td></tr>" +
            "<tr><td data-title='Agent'> " + Consts.EMPTY_STRING + "</td></tr>" +
            "<tr><td data-title='Titre'>" + Consts.EMPTY_STRING + "</td></tr>" +
            "<tr><td data-title='Nom de la machine'>" + Consts.EMPTY_STRING + "</td></tr>" +
            "<tr><td data-title='Description'>" + Consts.EMPTY_STRING + "</td></tr>" +
            "<tr><td data-title='Solution'>" + Consts.EMPTY_STRING + "</td></tr></tbody></table></div>";

        }

        private void LoadData(string id)
        {
            DataSet ds = DAL.SelectAllFromTicketId(id);
            DataSet dsAgent = DAL.SelectAgentIdentity(id);
            DataSet dsCaller = DAL.SelectCallerIdentity(id);

            //AFFICHAGE DU TABLEAU D'INFORMATIONS
            L_Body.Text = "<div class='responsive-table-line' style='margin:0px auto;max-width:700px;'><table class='table table-bordered table-condensed table-body-center' ><tbody>" +
                "<tr><td data-title='Référence'>" + ds.Tables[0].Rows[0]["REFERENCE"].ToString() + "</td></tr>" +
                "<tr><td data-title='Création de ticket'>" + ds.Tables[0].Rows[0]["START_TS"].ToString() + "</td></tr>" +
                "<tr><td data-title='Dernière mise à jour'>" + ds.Tables[0].Rows[0]["LAST_UPDATE_TS"].ToString() + "</td></tr>";

            if (ds.Tables[0].Rows[0]["RESOLUTION_TS"].ToString() == string.Empty)
                L_Body.Text += "<tr><td data-title='cloture du ticket'>Ticket non cloturé.</td></tr>";
            else
                L_Body.Text += "<tr><td data-title='Cloture du ticket'>" + ds.Tables[0].Rows[0]["RESOLUTION_TS"].ToString() + "</td></tr>";

            L_Body.Text += "<tr><td data-title='Appelant'> " + dsCaller.Tables[0].Rows[0]["FIRST_NAME"].ToString() + " " + dsCaller.Tables[0].Rows[0]["LAST_NAME"].ToString() + " </td></tr>" +
                "<tr><td data-title='Agent'> " + dsAgent.Tables[0].Rows[0]["FIRST_NAME"].ToString() + " " + dsAgent.Tables[0].Rows[0]["LAST_NAME"].ToString() + "</td></tr>" +
                "<tr><td data-title='Titre'>" + ds.Tables[0].Rows[0]["TITLE"].ToString() + "</td></tr>" +
                "<tr><td data-title='Nom de la machine'>" + ds.Tables[0].Rows[0]["PRODUCT"].ToString() + "</td></tr>" +
                "<tr><td data-title='Description'><p align='justify'>" + ds.Tables[0].Rows[0]["DESCRIPTION"].ToString() + "</p></td></tr>";

            if (ds.Tables[0].Rows[0]["RESOLUTION"].ToString() == string.Empty)
                L_Body.Text += "<tr><td data-title='Solution'><font color='#c03b44'><b>Pas de solution</b></font></td></tr>";
            else
                L_Body.Text += "<tr><td data-title='Solution'><p align='justify'><font color='#1A7F09'><b>" + ds.Tables[0].Rows[0]["RESOLUTION"].ToString() + "</b></font></p></td></tr>";

           


            //AFFICHAGE DES COMMENTAIRES
            ds = DAL.SelectAllFromCommentaire(id);
            DataTable dt = ds.Tables[0];

           
            if(dt == null || dt.Rows.Count == 0)
            {
                L_Body.Text += "</tbody></table></div><hr/><h3 style='text-align:center;'>Pas de commentaires</h3><hr/>";
            } else
            {
                if(dt.Rows.Count > 1)
                {
                    L_Body.Text += "</tbody></table></div><hr/><h3 style='text-align:center;'>Commentaires</h3><hr/>";
                } else
                {
                    L_Body.Text += "</tbody></table></div><hr/><h3 style='text-align:center;'>Commentaire</h3><hr/>";
                }
            }

            foreach (DataRow row in dt.Rows)
            {
                ds = DAL.SelectContact(row["CONTACT_ID"].ToString());
                L_Body.Text += "<div class='jumbotron'><p><table width=100% style='border-bottom: 2px solid #a5a1a1; font-size: 18px;'><tr><td align='left' width=50%>" + ds.Tables[0].Rows[0]["LAST_NAME"].ToString() + " " + ds.Tables[0].Rows[0]["FIRST_NAME"].ToString() + "</td><td align='right'>" + row["TIMESTAMP"].ToString() + "</td></tr></table></p>" +
                    "<p align='justify' style='font-size: 15px'> " + row["LOG"].ToString() + "</p></div>";
            }

        }

    }
}