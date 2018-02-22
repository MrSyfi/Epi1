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
            if (TB_code.Text != string.Empty && TB_info.Text != string.Empty)
                PopulateZPL(TB_code.Text, TB_info.Text);
        }

        private void PopulateZPL(string code, string info)
        {
            // Code QR en ZPL : ^XA^FO100,100^BQN,2,10^FDYourTextHere^FS^XZ
            string txt ="^XA^FO150,25^BXN,10,200^FD" + code + "^FS^CFA,25^FO110,150^FD" + info + "^FS^XZ";
            //Print(txt);

        }

        protected void B_generer_fichier_Click(object sender, EventArgs e)
        {
            List<String> listQr = new List<String>();
            List<String> listInfo = new List<String>();

            String savePath = Server.MapPath("~/");

            if (FileUploader.HasFile)
            {
                savePath += FileUploader.FileName;
                FileUploader.SaveAs(savePath);
            }

            try
            {
                // Exceptions can be thrown from here.
                foreach (string line in File.ReadLines(savePath))
                {
                    string[] parts = line.Split(';');
                    listQr.Add(parts[0]);
                    listInfo.Add(parts[1]);
                }

                File.Delete(savePath);

                for (int i = 0; i < listQr.Count; i++)
                {
                    PopulateZPL(listQr.ElementAt(i), listInfo.ElementAt(i));
                }
            } catch
            {

            }
        }

        private void Print(string text)
        {
            using (TcpClient client = new TcpClient()) {
                try
                {
                    client.Connect(DAL.SelectPrinterIP(DDL_Printer.SelectedValue.ToString()).Tables[0].Rows[0]["VALUE"].ToString(), Consts.ZPL_PRINTERS_DEFAULT_PORT);

                    using (StreamWriter writer = new StreamWriter(client.GetStream()))
                    {
                        writer.Write(text);
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