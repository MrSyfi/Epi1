using AccessApp.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Xml;

namespace AccessApp
{
    public partial class LinkObject : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!MobileDeviceChecker.fBrowserIsMobile())
            {
                if (Request.IsAuthenticated && Session["Username"] != null)
                {
                    // do nothing
                }
                else
                {
                    Response.Redirect("LogOn.aspx");
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

            if (DAL.SelectContact(TB_id_op.Text).Tables[0].Rows.Count != 0)
            {
                if (DAL.GetProduct(tmp).Tables[0].Rows.Count != 0)
                {
                    if (DAL.SelectAllFromTicketId(TB_id_ticket.Text).Tables[0].Rows.Count != 0)
                    {

                        DataSet _Ds = DAL.GetProduct(tmp);
                        if (_Ds.Tables[0].Rows[0]["STOCK_STATUS"].ToString() == "STOCKED" || _Ds.Tables[0].Rows[0]["STOCK_STATUS"].ToString() == "INSTALLED" || _Ds.Tables[0].Rows[0]["STOCK_STATUS"].ToString() == "UNDER_REPAIR")
                        {
                            _Ds = DAL.SelectFromTicketObjects(TB_id_ticket.Text);

                            List<string> listEpiID = new List<string>();
                            if (_Ds.Tables[0].Rows.Count > 0)
                            {
                                listEpiID = GetListFromXml(_Ds.Tables[0].Rows[0]["SVALUE"].ToString());
                            }

                            if (!listEpiID.Contains(tmp))
                            {
                                listEpiID.Add(tmp);
                                DAL.InsertInHistoric(TB_id_op.Text, "TRANSIT", tmp, "0", TB_id_ticket.Text);
                                DAL.UpdateStockStatus(tmp, "TRANSIT");
                                DAL.InsertInTicketObject(TB_id_ticket.Text, "DEVICE", GetXmlFromList(listEpiID), TB_id_op.Text);
                            }
                            else
                            {
                                System.Web.HttpContext.Current.Response.Write("<SCRIPT LANGUAGE='JavaScript'>alert('Ce matériel a déjà été ajouté')</SCRIPT>");
                            }
                        }
                        else
                        {
                            System.Web.HttpContext.Current.Response.Write("<SCRIPT LANGUAGE='JavaScript'>alert('Ce matériel n'est pas dans le stock.')</SCRIPT>");
                        }
                    }
                    else
                    {
                        TB_id_ticket.Text = string.Empty;
                        System.Web.HttpContext.Current.Response.Write("<SCRIPT LANGUAGE='JavaScript'>alert('Identifiant du ticket incorrect !')</SCRIPT>");
                    }
                }
                else
                {
                    TB_EpiID.Text = string.Empty;
                    System.Web.HttpContext.Current.Response.Write("<SCRIPT LANGUAGE='JavaScript'>alert('Epi ID incorrect !')</SCRIPT>");
                }
            }
            else
            {
                TB_id_op.Text = string.Empty;
                System.Web.HttpContext.Current.Response.Write("<SCRIPT LANGUAGE='JavaScript'>alert('Identifiant de l'opérateur incorrect !')</SCRIPT>");
            }
        }

        public string GetXmlFromList(List<string>Liste)
        {
            string _tmp = "<ITEMS>\n";
            if (Liste != null)
            {
                foreach (string l in Liste)
                {
                    _tmp += "\t<EPIID>" + l + "</EPIID>\n";
                }
            }
            _tmp += "</ITEMS>";
            return _tmp;

        }

        public List<string> GetListFromXml(string _Xml, int bit = 0)
        {
            List<string> _Ids = new List<string>();
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(_Xml);
            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                switch (node.Name)
                {
                    case "EPIID":
                        _Ids.Add(node.InnerText);
                        break;
                }
            }
            return _Ids;
        }
    }
}