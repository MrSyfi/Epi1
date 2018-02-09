using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AccessApp
{
    public partial class Gestion : System.Web.UI.Page
    {
        String[] tab_status = new String[] { "UNAPPROVED", "RESP_MAILED", "PART_APPROVED", "OP_READY", "USER_READY", "USER_MAILED", "OP_READYF", "CLOSED"};
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
            DataTable dt = null;
            if (TB_recherche.Text != String.Empty)
            {
                 dt = LoadData(TB_recherche.Text);
            }
             
            if (dt == null || dt.Rows.Count == 0)
            {
                L_result.Text = "Pas de résultat";
                GridView1.DataSource = dt;
                GridView1.DataBind();
                
            }
            else
            {
                L_result.Text = "Correspondances : " +dt.Rows.Count;
                GridView1.DataSource = dt;
                GridView1.DataBind();

            }
            if (TB_recherche.Text == String.Empty)
            {
                GridView1.DataSource = dt;
                GridView1.DataBind();
                
            }

            Btn.Enabled = false;
            DDL_status.Enabled = false;
            TB_resp_mail.Enabled = false;
            Reset();
        }

        protected void OnPaging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            LoadTable();

        }

        public void LoadStatus()
        {
           
            DDL_status.DataSource = tab_status;
            DDL_status.DataBind();
        }

        public void ChangeStatus(int index)
        {
            if (index == 5)
            {
                string[] listtmp = new string[2];
                listtmp[0] = tab_status[index - 1];
                listtmp[1] = tab_status[index];

                DDL_status.DataSource = listtmp;
                DDL_status.DataBind();

                TB_resp_mail.Enabled = false;
            }
            else if (index == 1)
            {
                string[] listtmp = new string[2];
                listtmp[0] = tab_status[index - 1];
                listtmp[1] = tab_status[index];

                DDL_status.DataSource = listtmp;
                DDL_status.DataBind();

                TB_resp_mail.Enabled = true;
            }
            else
            {

                string[] listtmp = new string[1];
                listtmp[0] = tab_status[index];

                DDL_status.DataSource = listtmp;
                DDL_status.DataBind();

                TB_resp_mail.Enabled = false;
            }
        }


        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            
            int currentRowIndex = Convert.ToInt32(e.CommandArgument) % GridView1.PageSize;
            
            if (currentRowIndex < GridView1.Rows.Count)
            {
                
                LoadStatus();
                TB_id.Text = GridView1.Rows[currentRowIndex].Cells[0].Text;
            
                TB_last_name.Text = System.Web.HttpUtility.HtmlDecode(GridView1.Rows[currentRowIndex].Cells[1].Text);
                TB_first_name.Text = System.Web.HttpUtility.HtmlDecode(GridView1.Rows[currentRowIndex].Cells[2].Text);
                TB_username.Text = System.Web.HttpUtility.HtmlDecode(GridView1.Rows[currentRowIndex].Cells[3].Text);
                TB_service.Text = System.Web.HttpUtility.HtmlDecode(GridView1.Rows[currentRowIndex].Cells[5].Text);
                DDL_status.SelectedValue = GridView1.Rows[currentRowIndex].Cells[6].Text;
                ChangeStatus(DDL_status.SelectedIndex);
                DDL_status.Enabled = true;
                Btn.Enabled = true;
                
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
            DAL.UpdateRequestStatus(TB_id.Text, DDL_status.SelectedItem.Text);

            LoadTable();
        }

        public void Reset()
        {
            TB_id.Text = String.Empty;
            TB_last_name.Text = String.Empty;
            TB_first_name.Text = String.Empty;
            TB_first_name.Text = String.Empty;
            TB_username.Text = String.Empty;
            TB_service.Text = String.Empty;
        }

        protected void DDL_nb_page_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridView1.PageSize =  Int32.Parse(DDL_nb_page.SelectedValue);
            LoadTable();
        }
    }
}