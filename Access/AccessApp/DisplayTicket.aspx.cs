using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AccessApp
{
    public partial class DisplayTicket : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Check if tId exists
            if (Request.QueryString.AllKeys.Contains("tId"))
            {
                // Check if tId is not null
                if(Request.QueryString["tId"] == string.Empty)
                {
                    Response.Redirect("GestionMateriel.aspx", true);
                }
                else
                {
                    LoadData(Request.QueryString["tId"]);
                    Param.Text = Request.QueryString["tId"];
                }
            } else
            {
                Response.Redirect("GestionMateriel.aspx", true);
            }

        }

        private void LoadData(string id)
        {
            DataSet ds = DAL.SelectAllFromTicketId(id);
            L_Body.Text = "<div class='responsive-table-line' style='margin:0px auto;max-width:700px;'><table class='table table-bordered table-condensed table-body-center' ><tbody>" +
                "<tr><td data-title='EpiID'>" + ds.Tables[0].Rows[0]["ID"].ToString() + "</td></tr></tbody></table></div><hr/>";
        }
    }
}