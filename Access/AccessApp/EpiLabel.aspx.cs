using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Controls;
using System.Windows.Forms;

namespace AccessApp
{
    public partial class EpiLabel : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
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
            List<String> listQr = new List<String>();
            List<String> listInfo = new List<String>();

            foreach (string line in File.ReadLines(TB_fichier.Text))
            {
                string[] parts = line.Split(';');
                listQr.Add(parts[0]);
                listInfo.Add(parts[1]);
            }

            for (int i = 0; i < listQr.Count; i++)
            {
                L_result.Text += "<br/>"+listQr.ElementAt(i).ToString();
                L_result.Text += "<br/>" + listInfo.ElementAt(i).ToString();
            }
        }

        private void Print()
        {
            using (TcpClient client = new TcpClient()) {
                try
                {
                    client.Connect(DAL.SelectPrinterIP(DDL_Printer.SelectedValue.ToString()).Tables[0].Rows[0]["VALUE"].ToString(), Consts.ZPL_PRINTERS_DEFAULT_PORT);

                    using (StreamWriter writer = new StreamWriter(client.GetStream()))
                    {
                        writer.Write(L_result.Text);
                        writer.Flush();
                    }
                } catch
                {
                    // Possible exceptions ?!?
                }
            }

        }

        
    }
}