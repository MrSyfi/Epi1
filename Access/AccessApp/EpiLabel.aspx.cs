using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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
            DataSet ds = DAL.SelectAllSites();
            DDL_Printer.DataTextField = "ABBREVIATION";
            DDL_Printer.DataValueField = "ABBREVIATION";
            DDL_Printer.DataSource = ds.Tables[0];
            DDL_Printer.DataBind();
        }

        protected void B_afficher_Click(object sender, EventArgs e)
        {

            PopulateZPL();
        }

        private void PopulateZPL()
        {
            // Code QR en ZPL : ^XA^FO100,100^BQN,2,10^FDYourTextHere^FS^XZ
            L_result.Text = "";
            L_result.Text = "^XA";
            L_result.Text += "^FO50,100^BXN,10,200^FD" + TB_code.Text+"^FS";
            L_result.Text += "^CFA,40";
            L_result.Text += "^FO50,250^FD" + TB_info.Text + "^FS";
            L_result.Text += "^XZ";

        }

        protected void B_generer_fichier_Click(object sender, EventArgs e)
        {
            L_result.Text += TB_fichier.Text;
            /*
            List<String> listQr = new List<String>();
            List<String> listInfo = new List<String>();

            foreach (string line in File.ReadLines(""))
            {
                string[] parts = line.Split(';');
                listQr.Add(parts[0]);
                listInfo.Add(parts[1]);
            }

            for (int i = 0; i < listQr.Count; i++)
            {
                L_result.Text += "<br/>"+listQr.ElementAt(i).ToString();
                L_result.Text += "<br/>" + listInfo.ElementAt(i).ToString();
            }*/
        }
    }
}