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
        }
    }
}