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
            LoadData(TB_recherche.Text);
        }

        private void LoadData(string id)
        {
            DataSet ds = DAL.getProductPerEpiId(id);
            L_EpiID.Text = String.Format("{0}",ds.Tables[0].Rows[0]["EPIID"]);
            L_Marque.Text = (string) ds.Tables[0].Rows[0]["NAME"] + " " + (string) ds.Tables[0].Rows[0]["MODELE"];
            L_NumSe.Text = (string) ds.Tables[0].Rows[0]["SERIAL_NUMBER"];
            L_Statut.Text = (string)ds.Tables[0].Rows[0]["STOCK_STATUS"];
            L_Loc.Text = (string)ds.Tables[0].Rows[0]["LOCALISATION_ID"];
            L_Agent.Text = (string)ds.Tables[0].Rows[0]["LAST_NAME"] + " " + (string)ds.Tables[0].Rows[0]["FIRST_NAME"];
            L_Date.Text = String.Format("{0}", ds.Tables[0].Rows[0]["OPERATION_DATE"]);
        }
    }
}