using System;
using System.Collections.Generic;
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
                    Server.Transfer("GestionMateriel.aspx", true);
                }
                else
                {
                    Param.Text = Request.QueryString["tId"];
                }
            } else
            {
                Server.Transfer("GestionMateriel.aspx", true);
            }

        }
    }
}