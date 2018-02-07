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


        /*public static DataSet SelectAllStatus()
        {
            return _db.ExecuteQuery(string.Format("SELECT DISTINCT AR_STATUS FROM {0}", Consts.ACCESS_REQUEST_TABLE));
        }*/

        public static DataSet SelectAll(string id)
        {
            List<string> parameters = new List<string>();
            List<string> values = new List<string>();

            parameters.Add(":id");values.Add(id);

            return _db.ExecuteQuery(string.Format("SELECT * FROM {0} WHERE ID = :id", Consts.ACCESS_REQUEST_TABLE), parameters, values);
        }

        public static DataSet SelectFromSearchRequest(string search)
        {
            // %search% in request for the LIKE Condition
            search = "%" + search.ToUpper() + "%";
            List<string> parameters = new List<string>();
            List<string> values = new List<string>();

            parameters.Add(":search");values.Add(search);
            return _db.ExecuteQuery(string.Format("SELECT ID, LAST_NAME, FIRST_NAME, USERNAME, PHONE_NBR, PRIV_EMAIL, SERVICE ,RA_DATE, AR_STATUS FROM {0} WHERE ((UPPER(LAST_NAME) LIKE :search OR UPPER(FIRST_NAME) LIKE :search OR UPPER(USERNAME) LIKE :search OR UPPER(SERVICE) LIKE :search  OR UPPER(AR_STATUS) LIKE:search) AND (AR_STATUS NOT LIKE 'CLOSED' AND AR_STATUS NOT LIKE 'REFUSED' AND AR_STATUS NOT LIKE 'ERROR' AND AR_STATUS NOT LIKE 'UNKNOWN' AND AR_STATUS NOT LIKE 'APPROVED')) ORDER BY ID DESC", Consts.ACCESS_REQUEST_TABLE), parameters, values);
        }

        public static bool UpdateRequestStatus(string id, string status)
        {
            List<string> parameters = new List<string>();
            List<string> values = new List<string>();

            parameters.Add(":status");values.Add(status);
            parameters.Add(":id");values.Add(id);

            return _db.ExecuteNonQuery(string.Format("UPDATE {0} SET AR_STATUS = :status WHERE ID = :id",Consts.ACCESS_REQUEST_TABLE), parameters, values);
        }
    }
}