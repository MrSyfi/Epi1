using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AccessApp
{
    public partial class EpiACCESS_Close : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private DataTable LoadData(string search = "")
        {
            DataTable dt = DAL.SelectFromSearchRequest("OP_READY").Tables[0];
            return dt;
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
                L_result.Text = "Pas de résultat...";
                GridView1.DataSource = dt;
                GridView1.DataBind();

            }
            else
            {
                GridView1.Columns[7].Visible = true;
                L_result.Text = "Correspondances : " + dt.Rows.Count;
                GridView1.DataSource = dt;
                GridView1.DataBind();
                GridView1.Columns[7].Visible = false;

            }
            if (TB_recherche.Text == String.Empty)
            {
                GridView1.DataSource = dt;
                GridView1.DataBind();

            }

            B_apply.Enabled = false;
            TB_resp_mail.Enabled = false;
            Reset();
        }

        protected void TB_recherche_TextChanged(object sender, EventArgs e)
        {
            GridView1.PageIndex = 0;
            LoadTable();
        }

        protected void DDL_nb_page_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridView1.PageSize = Int32.Parse(DDL_nb_page.SelectedValue);
            LoadTable();
        }

        protected void OnPaging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            LoadTable();
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int currentRowIndex = Convert.ToInt32(e.CommandArgument) % GridView1.PageSize;

            if (currentRowIndex < GridView1.Rows.Count)
            {
                TB_id.Text = GridView1.Rows[currentRowIndex].Cells[0].Text;

                TB_last_name.Text = System.Web.HttpUtility.HtmlDecode(GridView1.Rows[currentRowIndex].Cells[1].Text);
                TB_first_name.Text = System.Web.HttpUtility.HtmlDecode(GridView1.Rows[currentRowIndex].Cells[2].Text);
                TB_username.Text = System.Web.HttpUtility.HtmlDecode(GridView1.Rows[currentRowIndex].Cells[3].Text);
                TB_service.Text = System.Web.HttpUtility.HtmlDecode(GridView1.Rows[currentRowIndex].Cells[5].Text);
                TB_resp_mail.Text = System.Web.HttpUtility.HtmlDecode(GridView1.Rows[currentRowIndex].Cells[7].Text);
                TB_ticket.Text = System.Web.HttpUtility.HtmlDecode(GridView1.Rows[currentRowIndex].Cells[8].Text);

                B_apply.Enabled = true;
            }
        }

        protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.GridView1, "Select$" + e.Row.RowIndex);
            }
        }

        protected void B_apply_Click(object sender, EventArgs e)
        {
            DataSet ds = DAL.SelectAll(TB_id.Text);
            string mailResp = (string)ds.Tables[0].Rows[0]["RESP_EMAIL"];
            string username = (string)ds.Tables[0].Rows[0]["USERNAME"];
            string fullName = TB_first_name.Text + " " + TB_last_name.Text;

            DataSet dsTicket = DAL.SelectRef(TB_ticket.Text);
            string reff = (string)dsTicket.Tables[0].Rows[0]["REFERENCE"];

            DataSet dsContactUser = DAL.SelectUserEmail(TB_username.Text);
            string mailUser = "";//(string)dsContactUser.Tables[0].Rows[0]["EMAIL"];

            DataSet dsContactResp =  DAL.SelectAgentEmail(TB_ticket.Text);
            string mailAgent = (string)dsContactResp.Tables[0].Rows[0]["EMAIL"];

            //MAIL TO AGENT
            MailSender.SendPwdPerEmail( PasswordGenerator.Generate(6), "exp", mailAgent, username, mailUser, fullName, reff);
            //MAIL TO RESP
            MailSender.SendPwdPerEmail(PasswordGenerator.Generate(6), "exp", mailResp, username, mailUser, fullName, reff);

        }

        public void Reset()
        {
            TB_id.Text = String.Empty;
            TB_last_name.Text = String.Empty;
            TB_first_name.Text = String.Empty;
            TB_username.Text = String.Empty;
            TB_service.Text = String.Empty;
            TB_resp_mail.Text = String.Empty;
            TB_ticket.Text = String.Empty;
        }


    }
}