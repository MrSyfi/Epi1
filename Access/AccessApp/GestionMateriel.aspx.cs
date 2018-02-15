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
            L_EpiID.Text = ds.Tables[0].Rows[0]["EPIID"].ToString();
            Label1.Text = ds.Tables[0].Rows[0]["NAME"].ToString() + " " + ds.Tables[0].Rows[0]["MODELE"].ToString();
            Label2.Text = ds.Tables[0].Rows[0]["SERIAL_NUMBER"].ToString();
            Label3.Text = ds.Tables[0].Rows[0]["STOCK_STATUS"].ToString();
            Label4.Text = ds.Tables[0].Rows[0]["LOCALISATION_ID"].ToString();
            Label5.Text = ds.Tables[0].Rows[0]["LAST_NAME"].ToString() + " " + ds.Tables[0].Rows[0]["FIRST_NAME"].ToString() ;
            Label6.Text = ds.Tables[0].Rows[0]["OPERATION_DATE"].ToString();

        }
    }
}