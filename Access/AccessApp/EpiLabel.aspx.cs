using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text.RegularExpressions;
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
            if (TB_code.Text == string.Empty || TB_info.Text == string.Empty)
            {
                System.Web.HttpContext.Current.Response.Write("<SCRIPT LANGUAGE='JavaScript'>alert('Champs vides !')</SCRIPT>");
            }
            else
            {
                if (TB_info.Text.Length <= Consts.LABEL_STRING_LENGHT_LIMIT && Regex.IsMatch(TB_code.Text, "[a-zA-Z0-9]{1} ? [a-zA-Z0-9]{3} ? [a-zA-Z0-9]{2}"))
                {
                    PopulateZPL(TB_code.Text, TB_info.Text);
                    TB_code.Text = string.Empty;
                    TB_info.Text = string.Empty;
                } else
                {
                    System.Web.HttpContext.Current.Response.Write("<SCRIPT LANGUAGE='JavaScript'>alert('L'un des champs ne correspond pas au format demandé.')</SCRIPT>");
                }
            }
        }

        private void PopulateZPL(string code, string info)
        {
            // Code QR en ZPL : ^XA^FO100,100^BQN,2,10^FDYourTextHere^FS^XZ
            string txt = "^XA^FO350,25^BQN,10,4^FDQA" + code + "^FS^FO220,150^A@N,15,10,E:ARI000.FNT^FD" + info + "^FS^XZ";

            //Print("^XA^FO350,25^BQN,10,4^FDQA1 30012^FS^FO220,150^A@N,15,10,E:ARI000.FNT^FDAZERTYUIOPAZERTYUIOPAZERTYUIOP^FS^XZ");
            //Print(txt);
        }

        protected void B_generer_fichier_Click(object sender, EventArgs e)
        {
            try
            {
                String savePath = Server.MapPath("~/");

                if (FileUploader.HasFile)
                {
                    savePath += FileUploader.FileName;
                    FileUploader.SaveAs(savePath);

                    foreach (string line in File.ReadLines(savePath))
                    {
                        string[] parts = line.Split(';');
                        PopulateZPL(parts[0], parts[1]);
                    }

                    File.Delete(savePath);
                }
                else
                {
                    System.Web.HttpContext.Current.Response.Write("<SCRIPT LANGUAGE='JavaScript'>alert('Pas de fichier !')</SCRIPT>");
                }
                
            } catch
            {
                System.Web.HttpContext.Current.Response.Write("<SCRIPT LANGUAGE='JavaScript'>alert('Le fichier ne respecte pas le bon format ! ')</SCRIPT>");
            }
        }

        private void Print(string text)
        {
            using (TcpClient client = new TcpClient())
            {
                try
                {
                    client.Connect(DAL.SelectPrinterIP(DDL_Printer.SelectedValue.ToString()).Tables[0].Rows[0]["VALUE"].ToString(), Consts.ZPL_PRINTERS_DEFAULT_PORT);

                    using (StreamWriter writer = new StreamWriter(client.GetStream()))
                    {
                        writer.Write(text);
                        writer.Flush();
                    }
                }
                catch
                {
                    // Possible exceptions ?!?
                }
            }

        }

        
    }
}