﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AccessApp
{
    public partial class Gestion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Chargement des données
        }

        protected void TB_recherhce_TextChanged(object sender, EventArgs e)
        {
            Lbl_resultat.Text = TB_recherhce.Text;
        }
    }
}