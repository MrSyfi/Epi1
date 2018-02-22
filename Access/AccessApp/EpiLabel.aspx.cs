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

        }

        protected void B_afficher_Click(object sender, EventArgs e)
        {

            PopulateZPL(TB_code.Text, TB_info.Text);
        }

        private void PopulateZPL(string code, string info)
        {
            // Code QR en ZPL : ^XA^FO100,100^BQN,2,10^FDYourTextHere^FS^XZ
            L_result.Text = "";
            L_result.Text = "^XA";
            L_result.Text += "^FO50,100^BXN,10,200^FD" + code + "^FS";
            L_result.Text += "^CFA,40";
            L_result.Text += "^FO50,250^FD" + info + "^FS";
            L_result.Text += "^XZ";

        }

        protected void B_generer_fichier_Click(object sender, EventArgs e)
        {
            List<String> listQr = new List<String>();
            List<String> listInfo = new List<String>();

            String savePath = Server.MapPath("~/");
            L_result.Text += savePath;

            if (FileUploader.HasFile)
            {
                savePath += FileUploader.FileName;
                FileUploader.SaveAs(savePath);
            }
            
            foreach (string line in File.ReadLines(savePath))
            {
                string[] parts = line.Split(';');
                listQr.Add(parts[0]);
                listInfo.Add(parts[1]);
            }

            File.Delete(savePath);

            for (int i = 0; i < listQr.Count; i++)
            {
                L_result.Text += "</br>";
                PopulateZPL(listQr.ElementAt(i), listInfo.ElementAt(i));
            }
        }
    }
}