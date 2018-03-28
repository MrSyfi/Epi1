using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AccessApp
{
    public partial class LinkObject : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void B_apply_Click(object sender, EventArgs e)
        {

        }

        protected void TB_id_ticket_TextChanged(object sender, EventArgs e)
        {
           DataSet _ds = DAL.SelectAllFromTicketId(TB_id_ticket.Text);
            if (_ds.Tables[0].Rows.Count != 0)
            {
                SetFocus();

            } else
            {
                TB_id_ticket.Text = string.Empty;
                System.Web.HttpContext.Current.Response.Write("<SCRIPT LANGUAGE='JavaScript'>alert('Identifiant du ticket incorrect !')</SCRIPT>");
                SetFocus();
            }
        }

        protected void TB_id_op_TextChanged(object sender, EventArgs e)
        {
            DataSet _ds = DAL.SelectContact(TB_id_op.Text);
            if (_ds.Tables[0].Rows.Count != 0)
            {
                SetFocus();
            } else
            {
                TB_id_op.Text = string.Empty;
                System.Web.HttpContext.Current.Response.Write("<SCRIPT LANGUAGE='JavaScript'>alert('Identifiant de l'opérateur incorrect !')</SCRIPT>");
                SetFocus();
            }
        }

        protected void TB_EpiID_TextChanged(object sender, EventArgs e)
        {
            DataSet _ds = DAL.GetProduct(TB_EpiID.Text);
            if (_ds.Tables[0].Rows.Count != 0)
            {
                SetFocus();
            } else
            {
                TB_EpiID.Text = string.Empty;
                System.Web.HttpContext.Current.Response.Write("<SCRIPT LANGUAGE='JavaScript'>alert('Epi ID incorrect !')</SCRIPT>");
                SetFocus();
            }
        }

        public void SetFocus()
        {
            if (TB_id_ticket.Text == string.Empty)
                TB_id_ticket.Focus();
            else if (TB_id_op.Text == string.Empty)
                TB_id_op.Focus();
            else if (TB_EpiID.Text == string.Empty)
                TB_EpiID.Focus();
        }
    }
}