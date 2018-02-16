using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AccessApp
{
    public class Consts
    {
        public static bool isAffiche = false;
        public static string MOT_DE_PASSE;
        // DATABASE TABLE OR VIEW
        public static readonly string ACCESS_REQUEST_TABLE = "EPIDESK.ACCESS_REQUEST";
        public static readonly string CONTACTS_TABLE = "EPIDESK.CONTACTS";
        public static readonly string TICKETS_TABLE = "EPIDESK.TICKETS";
        public static readonly string USERS_TABLE = "EPIDESK.USERS";
        public static readonly string HISTORIC_TABLE = "EPIDESK.HISTORIC";
        public static readonly string LOCALISATION_TABLE = "EPIDESK.LOCALISATION";
        public static readonly string BRAND_TABLE = "EPIDESK.BRAND";
        public static readonly string MODEL_TABLE = "EPIDESK.MODEL";
        public static readonly string STOCK_TABLE = "EPIDESK.STOCK";
        public static readonly string COMMENTAIRE_TABLE = "EPIDESK.TICKETS_COMMENTS";


        public static readonly string ACCESS_REQUEST_VIEW = "ACCESS_REQUESTS_VIEW";
        public static readonly string ACCESS_REQUEST_STATUS = "ACCESS_REQUESTS_STATUS_VIEW";

        public static readonly string CONST_EMAIL_SMTP_SERVER_HOST = "mail.epicura.be";

        public static readonly string EMPTY_STRING = " - ";

        
    }
}