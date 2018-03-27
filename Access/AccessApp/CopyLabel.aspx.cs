using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AccessApp
{
    public partial class CopyLabel : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        // ZPL a imprimer : "^XA^FO215,25^BY2^BCN,100,Y,N,N^FD{0}^FS^XZ", label.Text
        private void Print(string zpl)
        {
            using (TcpClient client = new TcpClient())
            {
                try
                {
                    client.Connect(DAL.SelectPrinterIP(DDL_Printer.SelectedValue).Tables[0].Rows[0]["VALUE"].ToString(), Consts.ZPL_PRINTERS_DEFAULT_PORT);

                    using (StreamWriter writer = new StreamWriter(client.GetStream()))
                    {

                        writer.Write(zpl);
                        writer.Flush();
                    }
                }
                catch
                {
                    System.Web.HttpContext.Current.Response.Write("<SCRIPT LANGUAGE='JavaScript'>alert('Il y a eu une erreur lors de l'impression.')</SCRIPT>");
                    // Possible exceptions ?!?
                }
            }

        }
    }
}