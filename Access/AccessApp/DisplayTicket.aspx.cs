﻿using System;
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


            L_Body.Text = "<div class='responsive-table-line' style='margin:0px auto;max-width:700px;'><table class='table table-bordered table-condensed table-body-center' ><tbody>" +
                "<tr><td data-title='Référence'>" + ds.Tables[0].Rows[0]["REFERENCE"].ToString() + "</td></tr>" +
                "<tr><td data-title='Date de création de ticket'>" + ds.Tables[0].Rows[0]["START_TS"].ToString() + "</td></tr>" +
                "<tr><td data-title='Date de dernière mise à jour'>" + ds.Tables[0].Rows[0]["LAST_UPDATE_TS"].ToString() + "</td></tr>";

            if (ds.Tables[0].Rows[0]["RESOLUTION_TS"].ToString() == string.Empty)
                L_Body.Text += "<tr><td data-title='Date de cloture du ticket'>Ticket non encore cloturé.</td></tr>";
            else
                L_Body.Text += "<tr><td data-title='Date de cloture du ticket'>" + ds.Tables[0].Rows[0]["RESOLUTION_TS"].ToString() + "</td></tr>";

            L_Body.Text += "<tr><td data-title='Titre'>" + ds.Tables[0].Rows[0]["TITLE"].ToString() + "</td></tr>" +
                "<tr><td data-title='Description'><p align='justify'>" + ds.Tables[0].Rows[0]["DESCRIPTION"].ToString() + "</p></td></tr>";

            if (ds.Tables[0].Rows[0]["RESOLUTION"].ToString() == string.Empty)
                L_Body.Text += "<tr><td data-title='Solution'><font color='#1A7F09'" + ds.Tables[0].Rows[0]["RESOLUTION"].ToString() + "</font></td></tr>";
            else
                L_Body.Text += "<tr><td data-title='Solution'><font color='#1A7F09'>Pas de solution</font></td></tr>";

            L_Body.Text += "</tbody></table></div><hr/>";
        }
    }
}