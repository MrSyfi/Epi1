using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AccessApp
{
    public class Consts
    {
        // DATABASE TABLE OR VIEW
        public static readonly string ACCESS_REQUEST_TABLE = "EPIDESK.ACCESS_REQUEST";
        public static readonly string CONTACTS_TABLE = "EPIDESK.CONTACTS";
        public static readonly string TICKETS_TABLE = "EPIDESK.TICKETS";

        public static readonly string ACCESS_REQUEST_VIEW = "ACCESS_REQUESTS_VIEW";
        public static readonly string ACCESS_REQUEST_STATUS = "ACCESS_REQUESTS_STATUS_VIEW";
    }
}