﻿using System;
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

        }

        protected void B_apply_Click(object sender, EventArgs e)
        {
            if (TB_id_local.Text == string.Empty || TB_id_materiel.Text == string.Empty || TB_id_resp.Text == string.Empty)
            {
                L_Body.Text = "<font color='#c03b44'><p>Champs vide !</p></font>";
            }
            else
            {
                L_Body.Text = string.Empty;

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
            if (ds.Tables[0].Rows.Count > 0)
            {
                B_apply.Enabled = true;
                DDL_status.Enabled = true;
                L_Body.Text = string.Empty;

                string statut = ds.Tables[0].Rows[0]["STOCK_STATUS"].ToString();
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
                else
                {
                    string[] tab_status = new string[] { "STOCKED", "INSTALLED" };
                    DDL_status.DataSource = tab_status;
                    DDL_status.DataBind();
                }
                DDL_status.Enabled = true;
            }
            else
            {
                B_apply.Enabled = false;
                DDL_status.Enabled = false;
                L_Body.Text = "<font color='#c03b44'><p>EpiID incorrect !</p></font>";
            }
        }

        public void Reset()
        {
            TB_id_materiel.Text = string.Empty;
            TB_note.Text = string.Empty;
        }
    }
}