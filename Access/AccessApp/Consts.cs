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
        public static string ID_LOCALISATION;

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
        public static readonly string BIOS_GUID_TABLE = "EPIDESK.BIOS_GUID";
        public static readonly string APP_PARAM_TABLE = "EPIDESK.APP_PARAM";
        public static readonly string SITES_TABLE = "EPIDESK.SITES";
        public static readonly string ACCESS_REQUEST_VIEW = "ACCESS_REQUESTS_VIEW";
        public static readonly string ACCESS_REQUEST_STATUS = "ACCESS_REQUESTS_STATUS_VIEW";
        public const string CONST_ORACLE_CONNECTION_STRING = @"DATA SOURCE=HUINFORC17.epicura.lan:1521/XE;PASSWORD=epidesk;USER ID = epidesk";

        /* Default Ports or Values */
        public static readonly int ZPL_PRINTERS_DEFAULT_PORT = 9100;
        public static readonly string CONST_EMAIL_SMTP_SERVER_HOST = "mail.epicura.be";

        // Interface
        public static readonly string EMPTY_STRING = " - ";
        public static readonly int LABEL_STRING_LENGHT_LIMIT = 30;

        public static readonly string CONST_NETWORK_EDUC_SERVER  = "VMHUINFORV02.epicura.lan";
        public static readonly int CONST_NETWORK_EDUC_PORT = 2607;
        public const string CONST_NETWORK_EDUC_SERVICE_NAME = "EDSOCK";



    }
}