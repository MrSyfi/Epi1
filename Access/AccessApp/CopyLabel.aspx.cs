using AccessApp.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AccessApp
{
    public partial class CopyLabel : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Détermine si l'appareil utilisé est un mobile
            if (!MobileDeviceChecker.fBrowserIsMobile())
            {
                //Détermine si l'utlisateur est déjà authentifié
                if (!Request.IsAuthenticated || Session["Username"] == null)
                {
                    //Redirige vers la page d'authentification
                    Response.Redirect("LogOn.aspx");
                }
            }
        }

        /// <summary>
        /// Permet la communication avec une imprimante afin d'imprimer des étiquettes.
        /// </summary>
        /// <param name="zpl"></param>
        private void Print(string zpl)
        {
            using (TcpClient client = new TcpClient())
            {
                try
                {
                    //Connecte le client à un hôte TCP distant en utilisant l'adresse IP et le numéro de port spécifiés.
                    client.Connect(DAL.SelectPrinterIP(DDL_Printer.SelectedValue).Tables[0].Rows[0]["VALUE"].ToString(), Consts.ZPL_PRINTERS_DEFAULT_PORT);

                    using (StreamWriter writer = new StreamWriter(client.GetStream()))
                    {
                        //Message indiquant l'impression à l'utilisateur.
                        System.Web.HttpContext.Current.Response.Write("<SCRIPT LANGUAGE='JavaScript'>alert('Impression...')</SCRIPT>");

                        //Écrit une chaîne de caractères dans le flux.
                        writer.Write(zpl);
                        // Efface toutes les mémoires tampons pour le writer et provoque l'écriture des données mises en mémoire tampon dans le flux sous-jacent.
                        writer.Flush(); 
                    }
                }
                catch
                {
                    System.Web.HttpContext.Current.Response.Write("<SCRIPT LANGUAGE='JavaScript'>alert('Il y a eu une erreur lors de l'impression.')</SCRIPT>");
                }
            }
        }

        protected void B_apply_Click(object sender, EventArgs e)
        {
            string tmp = string.Empty;
            if (TB_EpiID.Text.Length > Consts.EPIID_PREFIX.Length && (TB_EpiID.Text.ToUpper().StartsWith(Consts.EPIID_PREFIX)))
                tmp = TB_EpiID.Text.Substring(Consts.EPIID_PREFIX.Length);
            else if (TB_EpiID.Text.Length > 0)
                tmp = TB_EpiID.Text;

            // ZPL a imprimer : "^XA^FO215,25^BY2^BCN,100,Y,N,N^FD{0}^FS^XZ", label.Text
            if (DAL.GetProductPerEpiId(tmp).Tables[0].Rows.Count != 0)
            {
                if (DAL.SelectUsernameFromUsers(TB_id_op.Text).Tables[0].Rows.Count != 0)
                {
                    if (DDL_Printer.SelectedIndex != 0)
                    {
                        DAL.InsertInHistoric(TB_id_op.Text, "COPY_LBL", tmp, "0");
                        Print(string.Format("^XA^FO215,25^BY2^BCN,100,Y,N,N^FD{0}^FS^XZ", "EPI"+tmp));
                    }
                    else
                    {
                        System.Web.HttpContext.Current.Response.Write("<SCRIPT LANGUAGE='JavaScript'>alert('Vous devez choisir une imprimante !')</SCRIPT>");
                    }
                }
                else
                {
                    System.Web.HttpContext.Current.Response.Write("<SCRIPT LANGUAGE='JavaScript'>alert('ID opérateur incorrect !')</SCRIPT>");
                }
            } else
            {
                System.Web.HttpContext.Current.Response.Write("<SCRIPT LANGUAGE='JavaScript'>alert('EpiID incorrect !')</SCRIPT>");
            }
           
        }
    }
}