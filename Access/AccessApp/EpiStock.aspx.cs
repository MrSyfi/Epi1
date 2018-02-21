using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AccessApp
{
    public partial class EpiStock : System.Web.UI.Page
    {
        //http://support.epicura.lan/epicmdb/main.php

        protected void Page_Load(object sender, EventArgs e)
        {
            SetFocus();
        }

        protected void B_apply_Click(object sender, EventArgs e)
        {

            if (TB_id_local.Text == string.Empty || TB_id_materiel.Text == string.Empty || TB_id_resp.Text == string.Empty)
            {
                System.Web.HttpContext.Current.Response.Write("<SCRIPT LANGUAGE='JavaScript'>alert('Champs vides !')</SCRIPT>");

            }
            else
            {
                string locId = string.Empty;
                try
                {
                    locId = DAL.SelectLocalisationId(TB_id_local.Text).Tables[0].Rows[0]["ID"].ToString();
                }
                catch
                {
                    // Unknown Localisation.. Insert it. (idOp: Who insert the localisation ? )
                    DAL.InsertLocalisationId(TB_id_local.Text, TB_id_resp.Text);
                    locId = DAL.SelectLocalisationId(TB_id_local.Text).Tables[0].Rows[0]["ID"].ToString();


                }
                Consts.ID_LOCALISATION = locId;

                // We get the room Id, between () in TB_ID_local 
                if (DAL.InsertInHistoric(TB_id_resp.Text, DDL_status.SelectedValue.ToString(), TB_id_materiel.Text, Consts.ID_LOCALISATION) && DAL.UpdateStockStatus(TB_id_materiel.Text, DDL_status.SelectedValue.ToString()))
                {
                    System.Web.HttpContext.Current.Response.Write("<SCRIPT LANGUAGE='JavaScript'>alert('Mise à jour effectuée.')</SCRIPT>");
                }

                // We get the room Id, between () in TB_ID_local 
                DAL.InsertInHistoric(TB_id_resp.Text, DDL_status.SelectedValue.ToString(), TB_id_materiel.Text,Consts.ID_LOCALISATION);
                DAL.UpdateStockStatus(TB_id_materiel.Text, DDL_status.SelectedValue.ToString());

                Reset();
                SetFocus();
            }
            // Get the localisation id;
            //string locId = DAL.SelectLocalisationId(TB_id_local.Text).Tables[0].Rows[0]["ID"].ToString();
            //if (locId == string.Empty)
            // Unknown Localisation..
            // DAL.InsertLocalisationId(TB_id_local.Text);
            //locId = DAL.SelectLocalisationId(TB_id_local.Text).Tables[0].Rows[0]["ID"].ToString();
            //DAL.InsertInHistoric(TB_id_resp.Text, DDL_status.SelectedValue.ToString(), TB_id_materiel.Text, locId);
        }

        protected void TB_id_materiel_TextChanged(object sender, EventArgs e)
        {
            
            string tmp = "";
            if (TB_id_materiel.Text.Length > 3 && (TB_id_materiel.Text.ToUpper().StartsWith("EPI")))
                tmp = TB_id_materiel.Text.Substring(3);
            else if (TB_id_materiel.Text.Length > 0)
                tmp = TB_id_materiel.Text;

            DataSet ds = DAL.GetProduct(tmp);

            if (ds.Tables[0].Rows.Count != 0)
            {

                B_apply.Enabled = true;
                DDL_status.Enabled = true;

                string statut = ds.Tables[0].Rows[0]["STOCK_STATUS"].ToString();

                if (statut != "OBSOLETE")
                {
                    if (statut == "STOCKED")
                    {
                        string[] tab_status = new string[] { "INSTALLED", "UNDER_REPAIR" };
                        DDL_status.DataSource = tab_status;
                        DDL_status.DataBind();
                    }
                    else if (statut == "INSTALLED")
                    {
                        string[] tab_status = new string[] { "STOCKED", "UNDER_REPAIR" };
                        DDL_status.DataSource = tab_status;
                        DDL_status.DataBind();
                    }
                    else if (statut == "UNDER_REPAIR")
                    {
                        string[] tab_status = new string[] { "STOCKED", "INSTALLED" };
                        DDL_status.DataSource = tab_status;
                        DDL_status.DataBind();
                    }

                    DDL_status.Enabled = true;
                    DDL_status.Focus();
                }
                else
                {
                    B_apply.Enabled = false;
                    DDL_status.Enabled = false;
                    System.Web.HttpContext.Current.Response.Write("<SCRIPT LANGUAGE='JavaScript'>alert('Matériel obsolète !')</SCRIPT>");
                    TB_id_materiel.Text = string.Empty;
                    SetFocus();
                }
                
            } else
            {
                B_apply.Enabled = false;
                DDL_status.Enabled = false;
                
                System.Web.HttpContext.Current.Response.Write("<SCRIPT LANGUAGE='JavaScript'>alert('EpiID incorrect !')</SCRIPT>");
                TB_id_materiel.Text = string.Empty;
                SetFocus();
            }
        }

        public void Reset()
        {
            DDL_status.Enabled = false;
            TB_id_materiel.Text = string.Empty;
            TB_note.Text = string.Empty;
        }

        public void SetFocus()
        {
            if (TB_id_resp.Text == string.Empty)
                TB_id_resp.Focus();
            else if (TB_id_materiel.Text == string.Empty)
                TB_id_materiel.Focus();
            else if (TB_id_local.Text == string.Empty)
                TB_id_local.Focus();
            else if (TB_note.Text == string.Empty)
                TB_note.Focus();
        }

        protected void TB_id_resp_TextChanged(object sender, EventArgs e)
        {
            DataSet ds = DAL.SelectUsernameFromUsers(TB_id_resp.Text);

            if (ds.Tables[0].Rows.Count == 0)
            {
                TB_id_resp.Text = string.Empty;
                System.Web.HttpContext.Current.Response.Write("<SCRIPT LANGUAGE='JavaScript'>alert('Identifiant incorrect !')</SCRIPT>");
                SetFocus();
            }
            else
            {
                SetFocus();
            }

            
        }

        protected void TB_id_local_TextChanged(object sender, EventArgs e)
        {
            // Checking if location exists
           
            SetFocus();


        }

        protected void DDL_status_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetFocus();
        }
    }
}