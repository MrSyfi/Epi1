﻿using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;

namespace AccessApp
{
    public partial class EpiLabel : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        static string RemoveDiacritics(string info)
        {
    
            string nchaine = string.Empty;
            for (int i = 0; i < info.Length; i++)
            {
                byte k = (byte)info[i];
                if (k == 32) nchaine += " ";
                else if (k > 223 && k < 231) nchaine += "a";
                else if(k > 231 && k < 236) nchaine += "e";
                else if(k > 235 && k < 240) nchaine += "i";
                else if(k > 241 && k < 247) nchaine += "o";
                else if(k > 248 && k < 253) nchaine += "u";
                else if(k == 231) nchaine += "c";
                else nchaine += info[i];
            }
          
            return nchaine;
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
                }
                else
                {
                    System.Web.HttpContext.Current.Response.Write("<SCRIPT LANGUAGE='JavaScript'>alert('L'un des champs ne correspond pas au format demandé.')</SCRIPT>");
                }
            }
        }

        private string PopulateZPL(string code, string info)
        {
            

            // Code QR en ZPL : ^XA^FO100,100^BQN,2,10^FDYourTextHere^FS^XZ
            return "^XA^FO350,25^BQN,10,4^FDHM,A" + code + "^FS^FO220,150^A@N,15,10,E:ARI000.FNT^FD" + RemoveDiacritics(info) + "^FS^XZ";
  
            //Print("^XA^FO350,25^BQN,10,4^FDHM,A 3-001-2^FS^FO220,150^A@N,15,10,E:ARI000.FNT^FDAZERTYUIOPAZERTYUIOPAZERTYUIOP^FS^XZ^XA^FO350,25^BQN,10,4^FDHM,A 3-001-2^FS^FO220,150^A@N,15,10,E:ARI000.FNT^FDAZERTYUIOPAZERTYUIOPAZERTYUIOP^FS^XZ");
            //Print(txt);
        }

        protected void B_generer_fichier_Click(object sender, EventArgs e)
        {
            String savePath = Server.MapPath("~/");
            
            try
            {

                if (FileUploader.HasFile)
                {
                    savePath += FileUploader.FileName;
                    FileUploader.SaveAs(savePath);
                    string result = string.Empty;

                    foreach (string line in File.ReadLines(savePath, Encoding.UTF7))
                    {
                      
                        string[] parts = line.Split(';');
                        
                        result += PopulateZPL(parts[0], parts[1]);
                    }
                    Print(result);
                    File.Delete(savePath);
                }
                else
                {
                    System.Web.HttpContext.Current.Response.Write("<SCRIPT LANGUAGE='JavaScript'>alert('Pas de fichier !')</SCRIPT>");
                }

            }
            catch
            {
                File.Delete(savePath);
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