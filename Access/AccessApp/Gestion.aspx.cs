using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AccessApp
{
    public partial class Gestion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void TB_recherche_TextChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            dt = DAL.SelectFromSearchRequest(TB_recherche.Text).Tables[0];

            GridView1.DataSource = dt;
            GridView1.DataBind();

            /*
            DataTable dt = new DataTable();
            dt.Columns.Add("Sl");
            dt.Columns.Add("data");
            dt.Columns.Add("heading1");
            dt.Columns.Add("heading2");
            for (int i = 0; i < 40; i++)
            {
                dt.Rows.Add(new object[] { i, 123 * i, 4567 * i, 2 * i, });
            }

            GridView1.DataSource = dt;
            GridView1.DataBind();*/
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int currentRowIndex = Int32.Parse(e.CommandArgument.ToString());
            test.Text = GridView1.Rows[currentRowIndex].Cells[1].Text;
        }

        protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.GridView1, "Select$" + e.Row.RowIndex);
            }
        }
    }
}