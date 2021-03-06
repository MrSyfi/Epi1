﻿using AccessApp.Utils;
using System;
using System.Data;
using System.Web.Security;

namespace AccessApp
{
    public partial class Timeline : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            //Détermine si l'appareil utilisé est un mobile
            if (!MobileDeviceChecker.fBrowserIsMobile())
            {
                //Détermine si l'utlisateur est déjà authentifié
                if (!Request.IsAuthenticated)
                {
                    //Redirige vers la page d'authentification
                    Response.Redirect("LogOn.aspx");
                }
            }
        }

        protected void TB_recherche_TextChanged(object sender, EventArgs e)
        {
            
            string tmp = string.Empty;
            if (TB_recherche.Text.Length > 3 && (TB_recherche.Text.ToUpper().StartsWith("EPI")))
                tmp = TB_recherche.Text.Substring(3);
            else if (TB_recherche.Text.Length > 0)
                tmp = TB_recherche.Text;
            LoadData(tmp);
        }

        private void LoadData(string id)
        {
            DataSet ds = DAL.GetProductPerEpiId(id);
            if (ds.Tables[0].Rows.Count > 0)
            {
                // LOCALISATION_ID CAN BE NULL (OBSOLETE OBJECTS)
                ds.Tables[0].Rows[0]["LOCALISATION_ID"] = ds.Tables[0].Rows[0]["LOCALISATION_ID"].ToString() == string.Empty ? Consts.EMPTY_STRING : ds.Tables[0].Rows[0]["LOCALISATION_ID"].ToString();
                ds.Tables[0].Rows[0]["NAME"] = ds.Tables[0].Rows[0]["NAME"].ToString() == string.Empty ? " - " : ds.Tables[0].Rows[0]["NAME"].ToString();
                ds.Tables[0].Rows[0]["SERIAL_NUMBER"] = ds.Tables[0].Rows[0]["SERIAL_NUMBER"].ToString() == string.Empty ? " - " : ds.Tables[0].Rows[0]["SERIAL_NUMBER"].ToString();
                L_Body.Text = "<div class='responsive-table-line' style='margin:0px auto;max-width:700px;'><table class='table table-bordered table-condensed table-body-center' ><tbody>" +
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

            L_Histo.Text = string.Empty;
            L_Histo.Text += "<h2 style='text-align:center;'>HISTORIQUE</h2><hr/>";
            L_Histo.Text += "<section id = 'cd-timeline' class='cd-container'>";
            foreach (DataRow row in dt.Rows)
            {
                L_Histo.Text += "<div class='cd-timeline-block'>";
                switch (row["STATUS_TO"].ToString())
                {
                    case "STOCKED": L_Histo.Text += "<div class='cd-timeline-img cd-stocked'><img src='Image/instock.png' alt='stocked'></div>"; break;
                    case "TRANSIT": L_Histo.Text += "<div class='cd-timeline-img cd-transit'><img src='Image/outstock.png' alt='transit'></div>"; break;
                    case "INSTALLED": L_Histo.Text += "<div class='cd-timeline-img cd-installed'><img src='Image/installed.png' alt='installed'></div>"; break;
                    case "UNDER_REPAIR": L_Histo.Text += "<div class='cd-timeline-img cd-repairing'><img src='Image/repair.png' alt='repairing'></div>"; break;
                    case "OBSOLETE": L_Histo.Text += "<div class='cd-timeline-img cd-obsolete'><img src='Image/deleteobsolete.png' alt='obsolete'></div>"; break;
                    case "COPY_LBL": L_Histo.Text += "<div class='cd-timeline-img cd-label'><img src='Image/label.png' alt='label'></div>"; break;
                }



                L_Histo.Text += "<div class='cd-timeline-content'>";
                // Content


                switch (row["STATUS_TO"].ToString())
                {
                    case "STOCKED": L_Histo.Text += "<b><h3> MIS EN STOCK </h3></b>"; break;
                    case "TRANSIT": L_Histo.Text += "<a href='DisplayTicket?tId=" + row["TICKET_ID"] + "'><h4> SORTI DU STOCK (TICKET: " + row["TICKET_ID"].ToString() + " )</h4></a>"; break;
                    case "INSTALLED": L_Histo.Text += "<b><h3> INSTALLÉ </h3></b>"; break;
                    case "UNDER_REPAIR": L_Histo.Text += "<b><h3> MIS EN RÉPARATION </h3></b>"; break;
                    case "OBSOLETE": L_Histo.Text += "<b><h3> DECLASSEMENT</h3></b>"; break;
                    case "COPY_LBL": L_Histo.Text += "<b><h3> COPIE D'ÉTIQUETTES</h3></b>"; break;
                }

                L_Histo.Text += row["LOCALISATION_ID"].ToString() == Consts.EMPTY_STRING ? "" : "<h4>" + row["LOCALISATION_ID"].ToString() + "</h4><br/>";

                L_Histo.Text += "<h5>Opération effectuée par " + row["FIRST_NAME"].ToString() + " " + row["LAST_NAME"].ToString() + " (CONTACT_ID : " + row["ID"].ToString() + ")</h5>";

                L_Histo.Text += "<span class='cd-date'> <h5>" + row["OPERATION_DATE"].ToString() + "</h5></span>";

                L_Histo.Text += "</div></div>";
            }
            L_Histo.Text += "</div>";
        }

        protected void B_afficher_Click(object sender, EventArgs e)
        {
            string tmp = string.Empty;
            if (TB_recherche.Text.Length > 3 && (TB_recherche.Text.ToUpper().StartsWith("EPI")))
                tmp = TB_recherche.Text.Substring(3);
            else if (TB_recherche.Text.Length > 0)
                tmp = TB_recherche.Text;
            LoadData(tmp);
        }
    }
}