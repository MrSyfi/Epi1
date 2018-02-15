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
            string tmp = TB_recherche.Text.Substring(3);
            LoadData(tmp);
        }

        private void LoadData(string id)
        {
            DataSet ds = DAL.getProductPerEpiId(id);
            if (ds.Tables[0].Rows.Count > 0)
            {
                L_Body.Text +="<div class='responsive-table-line' style='margin:0px auto;max-width:700px;'>< table class='table table-bordered table-condensed table-body-center' ><tbody>" +
                    "<tr><td data-title='EpiID'>" + ds.Tables[0].Rows[0]["EPIID"].ToString() + "</td></tr>" +
                    "<tr><td data-title='Marque et modèle'>" + ds.Tables[0].Rows[0]["NAME"].ToString() + " " + ds.Tables[0].Rows[0]["MODELE"].ToString() + "</td></tr>" +
                    "<tr><td data-title='Numéro de série (S/N)'>" + ds.Tables[0].Rows[0]["SERIAL_NUMBER"].ToString() + "</td></tr>" +
                    "<tr><td data-title='Statut'>" + ds.Tables[0].Rows[0]["STOCK_STATUS"].ToString() + "</td></tr>" +
                    "<tr><td data-title='Localisation actuelle'>" + ds.Tables[0].Rows[0]["LOCALISATION_ID"].ToString() + "</td></tr>" +
                    "<tr><td data-title='Stocké par'>" + ds.Tables[0].Rows[0]["LAST_NAME"].ToString() + " " + ds.Tables[0].Rows[0]["FIRST_NAME"].ToString() + "</td></tr>" +
                    "<tr><td data-title='Stocké le'>" + ds.Tables[0].Rows[0]["OPERATION_DATE"].ToString() + "</td></tr></tbody></table></div>";
            }
        }
    }
}