using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace AccessApp
{
    public class DAL
    {
        private static Database _db = new Database();

        public static DataSet SelectFromSearchRequest(string search)
        {
            // %search% in request for the LIKE Condition
            search = "%" + search + "%";
            List<string> parameters = new List<string>();
            List<string> values = new List<string>();

            parameters.Add(":search");values.Add(search);
            return _db.ExecuteQuery("SELECT * FROM EPIDESK.ACCESS_REQUEST WHERE LAST_NAME LIKE :search OR FIRST_NAME LIKE :search OR USERNAME LIKE :search", parameters, values);
        }

        public static bool UpdateRequestStatus(int id, string status)
        {
            List<string> parameters = new List<string>();
            List<string> values = new List<string>();

            parameters.Add(":status");values.Add(status);
            parameters.Add(":id");values.Add(id.ToString());

            return _db.ExecuteNonQuery("UPDATE EPIDESK.ACCESS_REQUEST SET AR_STATUS = :status WHERE ID = :id", parameters, values);
        }
    }
}