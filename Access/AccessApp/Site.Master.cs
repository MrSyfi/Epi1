using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AccessApp
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Utils.MobileDeviceChecker.fBrowserIsMobile())
            {
                if (Request.IsAuthenticated)
                {
                    RemotingConfiguration.CustomErrorsEnabled(true);
                }
                else
                {
                    Response.Redirect("LogOn.aspx");
                }
            }
        }
    }
}