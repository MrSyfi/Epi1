using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AccessApp
{
    public partial class GestionMateriel : System.Web.UI.Page
    {

       

        protected void Page_Load(object sender, EventArgs e)
        {
            TB_recherche.Focus();
        }

        protected void TB_recherche_TextChanged(object sender, EventArgs e)
        {
            string tmp="";
            if (TB_recherche.Text.Length > 3 && TB_recherche.Text.StartsWith("EPI"))
                tmp = TB_recherche.Text.Substring(3);
            else if (TB_recherche.Text.Length > 0)
                tmp = TB_recherche.Text;
            LoadData(tmp);
        }

        private void LoadData(string id)
        {
            DataSet ds = DAL.getProductPerEpiId(id);            
            if (ds.Tables[0].Rows.Count > 0)
            {
                // LOCALISATION_ID CAN BE NULL (OBSOLETE OBJECTS)
                ds.Tables[0].Rows[0]["LOCALISATION_ID"] = ds.Tables[0].Rows[0]["LOCALISATION_ID"].ToString() == string.Empty ? " - " : ds.Tables[0].Rows[0]["LOCALISATION_ID"].ToString();
                L_Body.Text ="<div class='responsive-table-line' style='margin:0px auto;max-width:700px;'><table class='table table-bordered table-condensed table-body-center' ><tbody>" +
                    "<tr><td data-title='EpiID'>" + ds.Tables[0].Rows[0]["EPIID"].ToString() + "</td></tr>" +
                    "<tr><td data-title='Marque et modèle'>" + ds.Tables[0].Rows[0]["NAME"].ToString() + " " + ds.Tables[0].Rows[0]["MODELE"].ToString() + "</td></tr>" +
                    "<tr><td data-title='Numéro de série'>" + ds.Tables[0].Rows[0]["SERIAL_NUMBER"].ToString() + "</td></tr>" +
                    "<tr><td data-title='Statut'>" + ds.Tables[0].Rows[0]["STATUS_TO"].ToString() + "</td></tr>" +
                    "<tr><td data-title='Localisation actuelle'>" + ds.Tables[0].Rows[0]["LOCALISATION_ID"].ToString() + "</td></tr>" +
                    "<tr><td data-title='Stocké par'>" + ds.Tables[0].Rows[0]["LAST_NAME"].ToString() + " " + ds.Tables[0].Rows[0]["FIRST_NAME"].ToString() + "</td></tr>" +
                    "<tr><td data-title='Stocké le'>" + ds.Tables[0].Rows[0]["OPERATION_DATE"].ToString() + "</td></tr></tbody></table></div><hr/>";
                PopulateHisto(ds.Tables[0]);
            }
            else
            {
                L_Histo.Text = string.Empty;
                L_Body.Text = "<center><h2><i>Pas de résultat...</i></h2><center>";
            }
        }

        private void PopulateHisto(DataTable dt)
        {
            L_Histo.Text = "";
            L_Histo.Text += "<h2 style='text-align:center;'>HISTORIQUE</h2><hr/>";
            L_Histo.Text += "<section id = 'cd-timeline' class='cd-container'>";
            foreach(DataRow row in dt.Rows)
            {
                L_Histo.Text += "<div class='cd-timeline-block'>"
               +  "<div class='cd-timeline-img cd-picture'>";
                switch (row["STATUS_TO"].ToString())
                {
                    case "STOCKED": L_Histo.Text += "<img src='green_tick.png' alt='picture'>";break;
                    case "TRANSIT": L_Histo.Text += "<img src='transit.svg' alt='picture'>";break;
                    case "INSTALLED": L_Histo.Text += "<img src='green_tick.png' alt='picture'>"; break;
                    case "UNDER_REPAIR": L_Histo.Text += "<img src='repair.png' alt='picture'>"; break;
                    case "OBSOLETE": L_Histo.Text += "<img src='expired.png' alt='picture'>"; break;
                }
                L_Histo.Text += "</div>";

                
                L_Histo.Text += "<div class='cd-timeline-content'>";
                // Content


                switch (row["STATUS_TO"].ToString())
                {
                    case "STOCKED": L_Histo.Text += "<h4> EN STOCK </h4>"; break;
                    case "TRANSIT": L_Histo.Text += "<h4> EN TRANSIT </h4>"; break;
                    case "INSTALLED": L_Histo.Text += "<h4> INSTALLÉ </h4>"; break;
                    case "UNDER_REPAIR": L_Histo.Text += "<h4> EN RÉPARATION </h4>"; break;
                    case "OBSOLETE": L_Histo.Text += "<h4> OBSOLÈTE </h4>"; break;
                }

                L_Histo.Text += "<h4>"+ row["LOCALISATION_ID"].ToString()+"</h4>";

                L_Histo.Text += "<br /><p>Déplacé par " + row["LAST_NAME"].ToString() + " " + row["FIRST_NAME"].ToString() + "</p>";

                L_Histo.Text += "<span class='cd-date'> <h5>" + row["OPERATION_DATE"].ToString() + "</h5></span>";

                L_Histo.Text += "</div></div>";
            }
            L_Histo.Text += "</div>";
        }
    }
}