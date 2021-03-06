﻿using System;
using System.Data;
using System.Web.UI.WebControls;

namespace AccessApp
{
    public partial class EpiACCESS_Close : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Détermine si l'utlisateur est déjà authentifié
            if (!Request.IsAuthenticated)
            {
                //Redirige vers la page d'authentification
                Response.Redirect("LogOn.aspx");
            }
        }

        //Récupére les données de la base de données
        private DataTable LoadData(string search = "")
        {
            DataTable dt = DAL.SelectOPReadyFromSearchRequest(search).Tables[0];
            return dt;
        }

        //Charge les données du tableau
        public void LoadTable()
        {

            DataTable dt = null;
            if (TB_recherche.Text != string.Empty)
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
            if (TB_recherche.Text == string.Empty)
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

        //Permet de charger les éléments quand on click sur une ligne du tableau
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

                Consts.MOT_DE_PASSE = PasswordGenerator.Generate();

                // get the text from sendemail
                L_mail.Text = Server.HtmlDecode(MailSender.SendEmailToView(Consts.MOT_DE_PASSE, TB_username.Text, TB_first_name.Text, TB_last_name.Text));
                B_apply.Visible = true;

                L_preview.Visible = true;
            }
        }

        //Rajoute un 'onclick' sur chaque élément du tableau
        protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.GridView1, "Select$" + e.Row.RowIndex);
            }
        }

        protected void B_apply_Click(object sender, EventArgs e)
        {

            string username = TB_username.Text;
            string mailresp = TB_resp_mail.Text;
            string fullUserName = TB_first_name.Text + " " + TB_last_name.Text;

            DataSet ds = DAL.SelectAgentEmail(TB_ticket.Text);
            string mailAgent = (string)ds.Tables[0].Rows[0]["EMAIL"];

            ds = DAL.SelectRef(TB_ticket.Text);
            string reff = (string)ds.Tables[0].Rows[0]["REFERENCE"];

            //MAIL TO AGENT / RESP
            MailSender.SendPwdPerEmail(Consts.MOT_DE_PASSE, mailAgent, mailresp, username, TB_first_name.Text,TB_last_name.Text, reff, out bool sended);


            if (sended)
            {
                
                ChangePassword(TB_username.Text, Consts.MOT_DE_PASSE);

                DAL.UpdateRequestStatus(TB_id.Text, "USER_READY");
                DAL.InsertTicketsComment(TB_ticket.Text, "1", "Clôture de la demande d'accès et email envoyé le " + DateTime.Now.ToString("dd/MM/yyyy") + "avec les identifiants : \n\t Username : "+TB_username.Text+"\n\tPassword : "+Consts.MOT_DE_PASSE, "1");
                DataSet _ds = DAL.SelectAgentIdentity(TB_ticket.Text);
                
                DAL.InsertInTicketLog(long.Parse(TB_ticket.Text), _ds.Tables[0].Rows[0]["LAST_NAME"].ToString() + " " + _ds.Tables[0].Rows[0]["FIRST_NAME"].ToString(), "ACCCLOSED", _ds.Tables[0].Rows[0]["LAST_NAME"].ToString() + " " + _ds.Tables[0].Rows[0]["FIRST_NAME"].ToString());
                DAL.InsertInTicketLog(long.Parse(TB_ticket.Text), _ds.Tables[0].Rows[0]["LAST_NAME"].ToString() + " " + _ds.Tables[0].Rows[0]["FIRST_NAME"].ToString(), "EMAIL", mailAgent);
                DAL.InsertInTicketLog(long.Parse(TB_ticket.Text), _ds.Tables[0].Rows[0]["LAST_NAME"].ToString() + " " + _ds.Tables[0].Rows[0]["FIRST_NAME"].ToString(), "EMAIL", mailresp);
                DAL.CloseTicket(TB_ticket.Text, Server.HtmlDecode(MailSender.SendEmailToView(Consts.MOT_DE_PASSE, TB_username.Text, TB_first_name.Text, TB_last_name.Text)));
                // Refresh the table
                LoadTable();
            }
        }

        //Permet de changer un mot de passe via un service
        public void ChangePassword(string username, string motDePasse, bool resetPassword = true, bool restreint = true)
        {
            EpiService.MyServicesSoapClient client = new EpiService.MyServicesSoapClient();
            //Utilisation du service spécifique 
            client.ChgPwdWindows("EPICURA", username, String.Empty, motDePasse, motDePasse, resetPassword, restreint);
        }

        public void Reset()
        {
            TB_id.Text = string.Empty;
            TB_last_name.Text = string.Empty;
            TB_first_name.Text = string.Empty;
            TB_username.Text = string.Empty;
            TB_service.Text = string.Empty;
            TB_resp_mail.Text = string.Empty;
            TB_ticket.Text = string.Empty;
            L_mail.Text = string.Empty;
            L_preview.Visible = false;
        }

    }
}
