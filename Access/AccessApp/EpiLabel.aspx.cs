using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AccessApp
{
    public partial class EpiLabel : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void B_afficher_Click(object sender, EventArgs e)
        {
            string result = "^XA ^FO50,100 ^BXN,20,200 ^" + TB_code.Text + " ^FS ^XZ";
                
        }
    }
}