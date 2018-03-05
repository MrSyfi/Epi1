using System;
using System.Data;
using System.Web.UI.WebControls;

namespace AccessApp
{
    public partial class EpiStock : System.Web.UI.Page
    {

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
                if (DDL_status.SelectedValue.ToString() != "OBSOLETE")
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


                    if (DAL.InsertInHistoric(TB_id_resp.Text, DDL_status.SelectedValue.ToString(), TB_id_materiel.Text, Consts.ID_LOCALISATION, TB_note.Text) && DAL.UpdateStockStatus(TB_id_materiel.Text, DDL_status.SelectedValue.ToString()))
                    {
                        System.Web.HttpContext.Current.Response.Write("<SCRIPT LANGUAGE='JavaScript'>alert('Mise à jour effectuée.')</SCRIPT>");

                        SetVisible(false);
                        B_afficher.Visible = true;
                    }



                    Reset();
                    SetFocus();
                }

            }
        }   

        public void Reset()
        {
            L_obsolete.Text = string.Empty;
            B_obsolete.Visible = false;
            DDL_status.Enabled = false;
            TB_id_materiel.Text = string.Empty;
            TB_note.Text = string.Empty;
            TB_id_local.Enabled = true;
        }
        
        public void SetFocus()
        {
            if (TB_id_resp.Text == string.Empty)
                TB_id_resp.Focus();
            else if (TB_id_materiel.Text == string.Empty)
                TB_id_materiel.Focus();
            else if (TB_id_local.Text == string.Empty)
                TB_id_local.Focus();
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

        protected void DDL_status_SelectedIndexChanged(object sender, EventArgs e)
        {
            // An obsolete object don't have any location..
            if (((DropDownList)sender).SelectedValue.ToString() == "OBSOLETE")
            {
                TB_id_local.Enabled = false;

                B_obsolete.Visible = true;
                B_apply.Visible = false;
            }
            else
            {

                TB_id_local.Enabled = true;
                B_obsolete.Visible = false;
                B_apply.Visible = true;
            }

            SetFocus();
        }

        public void CheckEpiID()
        {
            string tmp = string.Empty;
            // Reset the obsolete literal when the user change of epiid
           
            B_obsolete.Visible = false;


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
                        string[] tab_status = new string[] { "INSTALLED", "UNDER_REPAIR", "OBSOLETE" };

                        DDL_status.DataSource = tab_status;
                        DDL_status.DataBind();
                    }
                    else if (statut == "INSTALLED")
                    {
                        string[] tab_status = new string[] { "STOCKED", "UNDER_REPAIR", "OBSOLETE" };

                        DDL_status.DataSource = tab_status;
                        DDL_status.DataBind();
                    }
                    else if (statut == "UNDER_REPAIR")
                    {
                        string[] tab_status = new string[] { "STOCKED", "INSTALLED", "OBSOLETE" };

                        DDL_status.DataSource = tab_status;
                        DDL_status.DataBind();
                    }
                    else if (statut == "TRANSIT")
                    {
                        string[] tab_status = new string[] { "STOCKED", "INSTALLED", "UNDER_REPAIR", "OBSOLETE" };

                        DDL_status.DataSource = tab_status;
                        DDL_status.DataBind();
                    }

                    DDL_status.Enabled = true;

                    B_afficher.Visible = false;
                    SetVisible(true);
                    Populate(DAL.GetProductPerEpiId(TB_id_materiel.Text));
                    B_modifier.Visible = true;
                }
                else
                {
                    B_apply.Enabled = false;
                    DDL_status.Enabled = false;
                    System.Web.HttpContext.Current.Response.Write("<SCRIPT LANGUAGE='JavaScript'>alert('Matériel obsolète !')</SCRIPT>");
                    TB_id_materiel.Text = string.Empty;
                    SetFocus();
                }

            }
            else
            {
                B_apply.Enabled = false;
                DDL_status.Enabled = false;

                System.Web.HttpContext.Current.Response.Write("<SCRIPT LANGUAGE='JavaScript'>alert('EpiID incorrect !')</SCRIPT>");
                TB_id_materiel.Text = string.Empty;
                SetFocus();
            }
        }

        private void Populate(DataSet ds)
        {
            L_obsolete.Text = string.Empty;
            L_obsolete.Text += "<div class='responsive-table-line' style='margin:0px auto;max-width:700px;'><table class='table table-bordered table-condensed table-body-center' ><tbody>" +
                    "<tr><td data-title='EpiID'>" + ds.Tables[0].Rows[0]["EPIID"].ToString() + "</td></tr>" +
                    "<tr><td data-title='Marque et modèle'>" + ds.Tables[0].Rows[0]["NAME"].ToString() + " " + ds.Tables[0].Rows[0]["MODELE"].ToString() + "</td></tr>" +
                    "<tr><td data-title='Numéro de série'>" + ds.Tables[0].Rows[0]["SERIAL_NUMBER"].ToString() + "</td></tr></tbody></table></div><hr/>";

        }

        private void SetVisible(bool isVisible)
        {

            TB_id_materiel.Enabled = !isVisible;

            L_obsolete.Visible = isVisible;
            IdOperateur.Visible = isVisible;
            statut.Visible = isVisible;
            info.Visible = isVisible;
            localisation.Visible = isVisible;
            note.Visible = isVisible;
            B_modifier.Visible = isVisible;
            B_apply.Visible = isVisible;
            TB_id_local.Visible = isVisible;
            TB_id_resp.Visible = isVisible;
            TB_note.Visible = isVisible;
            TB_id_local.Visible = isVisible;
            DDL_status.Visible = isVisible;

            if (!isVisible)
            {
                B_apply.Visible = false;
                B_obsolete.Visible = false;
            }
        }

        protected void B_obsolete_Click(object sender, EventArgs e)
        {

            // Envoi Mail au responsable
            DataSet ds = DAL.GetProductPerEpiId(TB_id_materiel.Text);
            string model = ds.Tables[0].Rows[0]["MODELE"].ToString();
            string marque = ds.Tables[0].Rows[0]["NAME"].ToString();
            string numSerie = ds.Tables[0].Rows[0]["SERIAL_NUMBER"].ToString();
            ds = DAL.SelectContact(TB_id_resp.Text);
            string nameAgent = ds.Tables[0].Rows[0]["LAST_NAME"].ToString() + " " + ds.Tables[0].Rows[0]["FIRST_NAME"].ToString();
            string mailAgent = ds.Tables[0].Rows[0]["EMAIL"].ToString();
            ds = DAL.GetRespMail();
            string respMail = ds.Tables[0].Rows[0]["VALUE"].ToString();

            //MailSender.SendObsoleteEmail("resp", mailAgent, TB_id_materiel.Text, marque, model, numSerie, nameAgent);
            MailSender.SendObsoleteEmail(respMail, mailAgent, TB_id_materiel.Text, marque, model, numSerie, nameAgent);

            // Modif DB..
            DAL.InsertInHistoric(TB_id_resp.Text, DDL_status.SelectedValue.ToString(), TB_id_materiel.Text, "0");
            DAL.UpdateStockStatus(TB_id_materiel.Text, DDL_status.SelectedValue.ToString());

            Reset();
            SetFocus();


        }

        protected void B_afficher_Click(object sender, EventArgs e)
        {
            CheckEpiID();
        }

        protected void B_modifier_Click(object sender, EventArgs e)
        {
            TB_id_materiel.Enabled = true;
            SetVisible(false);
            B_afficher.Visible = true;
            B_modifier.Visible = false;
        }
    }
}