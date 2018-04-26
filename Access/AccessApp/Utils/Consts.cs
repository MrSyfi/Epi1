namespace AccessApp
{
    public class Consts
    {
        // OTHERS

        
        public static string MOT_DE_PASSE;
        public static string ID_LOCALISATION;

        // DATABASE TABLE OR VIEW

        public static readonly string ACCESS_REQUEST_TABLE = "EPIDESK.ACCESS_REQUEST";
        public static readonly string TICKETS_COMMENTS = "EPIDESK.TICKETS_COMMENTS";
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
        public static readonly string TICKET_OBJECTS_TABLE = "EPIDESK.TICKETS_OBJECTS";
        public static readonly string ACCESS_REQUEST_VIEW = "ACCESS_REQUESTS_VIEW";
        public static readonly string ACCESS_REQUEST_STATUS = "ACCESS_REQUESTS_STATUS_VIEW";
        public static readonly string TICKETS_LOG_TABLE = "EPIDESK.TICKETS_LOG";
       public const string CONST_ORACLE_CONNECTION_STRING = @"DATA SOURCE=VMHUINFORV01.epicura.lan:1521/XEXDB;PASSWORD=TL19xPo53;USER ID=EPIDESK";
        //public const string CONST_ORACLE_CONNECTION_STRING = @"DATA SOURCE=HUINFORC17.epicura.lan:1521/XE;PASSWORD=epidesk;USER ID = epidesk";

        // INTERFACE

        public static readonly string EMPTY_STRING = " - ";
        public static readonly int LABEL_STRING_LENGHT_LIMIT = 30;
        public static readonly int LABEL_QR_LENGHT_LIMIT = 15;
        public static readonly int CONST_DB_FIELDS_ACTIVE_STATUS = 0;
        public static readonly string CONST_PARAM_EPITOOLS_USR = "EPITOOLS_USR";
        public static readonly string EPIID_PREFIX = "EPI";

        // NETWORK
        public static readonly int ZPL_PRINTERS_DEFAULT_PORT = 9100;
        public static readonly string CONST_EMAIL_SMTP_SERVER_HOST = "mail.epicura.be";
        public static readonly string CONST_NETWORK_EDUC_SERVER = "VMHUINFORV02.epicura.lan";
        public static readonly int CONST_NETWORK_EDUC_PORT = 2607;
        public static readonly string VARIABLE_USR = "EPITOOLS_USR";
        public static readonly string VARIABLE_PWD = "EPITOOLS_PWD";
        public const string CONST_NETWORK_EDUC_SERVICE_NAME = "EDSOCK";
        
    }
}