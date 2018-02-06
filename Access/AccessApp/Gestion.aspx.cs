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

        private DataTable LoadData(string search = "")
        {
            
            DataTable dt = DAL.SelectFromSearchRequest(search).Tables[0];
            return dt;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            DDL_status.Items.Add("Dynamically added");
        }

        protected void TB_recherche_TextChanged(object sender, EventArgs e)
        {
            GridView1.PageIndex = 0;
            LoadTable();
        }

        public void LoadTable()
        {
            DataTable dt = LoadData(TB_recherche.Text);
            if (dt.Rows.Count == 0)
            {
                L_result.Text = "Pas de résultat";
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
            else
            {
                L_result.Text = "";
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
            if (TB_recherche.Text == "")
            {
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }

            Reset();
        }

        protected void OnPaging(object sender, GridViewPageEventArgs e)
        {
            
            GridView1.PageIndex = e.NewPageIndex;
            LoadTable();

        }


        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            
            int currentRowIndex = Int32.Parse(e.CommandArgument.ToString());
            
            if (currentRowIndex < 10)
            {
                TB_id.Text = GridView1.Rows[currentRowIndex].Cells[0].Text;
                TB_last_name.Text = GridView1.Rows[currentRowIndex].Cells[1].Text;
                TB_first_name.Text = GridView1.Rows[currentRowIndex].Cells[2].Text;
                TB_username.Text = GridView1.Rows[currentRowIndex].Cells[3].Text;
                TB_service.Text = GridView1.Rows[currentRowIndex].Cells[5].Text;
            }
        }

        protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.GridView1, "Select$" + e.Row.RowIndex);
            }
        }

        protected void Btn_Click(object sender, EventArgs e)
        {
            DAL.UpdateRequestStatus(TB_id.Text, TextBox3.Text);

            GridView1.DataSource = LoadData(TB_recherche.Text);
            GridView1.DataBind();
        }

        public void Reset()
        {
            TB_id.Text = "";
            TB_last_name.Text = "";
            TB_first_name.Text = "";
            TB_first_name.Text = "";
            TB_username.Text = "";
            TB_service.Text = "";
        }
    }
}