using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AccessApp
{
    public partial class EpiStock : System.Web.UI.Page
    {
        //http://support.epicura.lan/epicmdb/main.php

        string[] tab_status = new string[] { "STOCKED", "INSTALLED", "REPARED"};

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void B_apply_Click(object sender, EventArgs e)
        {
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

        }
    }
}