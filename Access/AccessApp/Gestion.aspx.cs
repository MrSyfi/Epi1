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
                

                LoadStatus();
            }
            if (TB_recherche.Text == "")
            {
                GridView1.DataSource = dt;
                GridView1.DataBind();
                
            }
            DDL_status.Enabled = false;
            Reset();
        }

        protected void OnPaging(object sender, GridViewPageEventArgs e)
        {
            LoadStatus();

            GridView1.PageIndex = e.NewPageIndex;
            LoadTable();

        }

        public void LoadStatus()
        {
            DataSet ds = DAL.SelectAllStatus();
            DDL_status.DataSource = ds.Tables[0];
            DDL_status.DataValueField = ds.Tables[0].Columns["AR_STATUS"].ToString();
            DDL_status.DataBind();
        }


        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            
            int currentRowIndex = Int32.Parse(e.CommandArgument.ToString());
            
            if (currentRowIndex < 10)
            {
                TB_id.Text = GridView1.Rows[currentRowIndex].Cells[0].Text;
                TB_last_name.Text = System.Web.HttpUtility.HtmlDecode(GridView1.Rows[currentRowIndex].Cells[1].Text);
                TB_first_name.Text = System.Web.HttpUtility.HtmlDecode(GridView1.Rows[currentRowIndex].Cells[2].Text);
                TB_username.Text = System.Web.HttpUtility.HtmlDecode(GridView1.Rows[currentRowIndex].Cells[3].Text);
                TB_service.Text = System.Web.HttpUtility.HtmlDecode(GridView1.Rows[currentRowIndex].Cells[5].Text);

                DDL_status.SelectedValue = GridView1.Rows[currentRowIndex].Cells[6].Text;
                DDL_status.Enabled = true;
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
            DAL.UpdateRequestStatus(TB_id.Text, DDL_status.SelectedValue);

            LoadTable();
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