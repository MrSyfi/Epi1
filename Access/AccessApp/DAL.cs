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

        /// <summary>
        /// Get All the information about a specific access request
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static DataSet SelectAll(string id)
        {
            List<string> parameters = new List<string>();
            List<string> values = new List<string>();

            parameters.Add(":id");values.Add(id);

            return _db.ExecuteQuery(string.Format("SELECT * FROM {0} WHERE ID = :id", Consts.ACCESS_REQUEST_TABLE), parameters, values);
        }

        /// <summary>
        /// Get some informations about an access request filtered by an user's search
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public static DataSet SelectFromSearchRequest(string search)
        {
            // %search% in request for the LIKE Condition
            if (search.ToUpper() != "OP_READY")
                search = "%" + search.ToUpper() + "%";
            else
                // By doing this, we only take the OP_READY (and not OP_READYF) REQUESTS.
                search = "%" + search.ToUpper();
            List<string> parameters = new List<string>();
            List<string> values = new List<string>();

            parameters.Add(":search");values.Add(search);
            return _db.ExecuteQuery(string.Format("SELECT ID, LAST_NAME, FIRST_NAME , USERNAME , PHONE_NBR , SERVICE  , AR_STATUS , RESP_EMAIL, TICKET_ID FROM {0} WHERE ((UPPER(LAST_NAME) LIKE :search OR UPPER(FIRST_NAME) LIKE :search OR UPPER(USERNAME) LIKE :search OR UPPER(SERVICE) LIKE :search  OR UPPER(AR_STATUS) LIKE:search OR UPPER(TICKET_ID) LIKE :search) AND (AR_STATUS NOT LIKE 'CLOSED' AND AR_STATUS NOT LIKE 'REFUSED' AND AR_STATUS NOT LIKE 'ERROR' AND AR_STATUS NOT LIKE 'UNKNOWN' AND AR_STATUS NOT LIKE 'APPROVED')) ORDER BY ID DESC", Consts.ACCESS_REQUEST_TABLE), parameters, values);
        }

        /// <summary>
        /// Get the username from an access request.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static DataSet SelectUsernameFromId(string id)
        {
            List<string> parameters = new List<string>();
            List<string> values = new List<string>();

            parameters.Add(":id"); values.Add(id);

            return _db.ExecuteQuery(string.Format("SELECT USERNAME FROM {0} WHERE ID = :id", Consts.ACCESS_REQUEST_TABLE), parameters, values);
        }

        /// <summary>
        /// Get the agent from a ticket's id
        /// </summary>
        /// <param name="ticketId"></param>
        /// <returns></returns>
        public static DataSet SelectAgentIdPerTicketID(string ticketId)
        {
            List<string> parameters = new List<string>();
            List<string> values = new List<string>();

            parameters.Add(":id"); values.Add(ticketId);

            return _db.ExecuteQuery(string.Format("SELECT AGENT_ID FROM {0} WHERE ID = :id",Consts.TICKETS_TABLE), parameters,values);
        }

        /// <summary>
        /// get the agent's email from a ticket ID
        /// </summary>
        /// <param name="agentID"></param>
        /// <returns></returns>
        public static DataSet SelectAgentEmail(string ticketID)
        {
            List<string> parameters = new List<string>();
            List<string> values = new List<string>();

            parameters.Add(":id"); values.Add(ticketID);

            return _db.ExecuteQuery(string.Format("SELECT DISTINCT(EMAIL) FROM {0} INNER JOIN {1} ON {0}.ID = {1}.AGENT_ID INNER JOIN {2} ON {2}.TICKET_ID = {1}.ID WHERE {2}.TICKET_ID LIKE :id ",Consts.CONTACTS_TABLE, Consts.TICKETS_TABLE, Consts.ACCESS_REQUEST_TABLE), parameters, values);
        }

        /// <summary>
        /// Get an user's email from its username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public static DataSet SelectUserEmail(string username)
        {
            username = username.ToUpper();
            List<string> parameters = new List<string>();
            List<string> values = new List<string>();

            parameters.Add(":username"); values.Add(username);

            return _db.ExecuteQuery(string.Format("select DISTINCT(EPIDESK.CONTACTS.EMAIL) FROM EPIDESK.CONTACTS INNER JOIN EPIDESK.USERS ON EPIDESK.CONTACTS.ID = EPIDESK.USERS.CONTACT_ID WHERE EPIDESK.USERS.ID = (select ID FROM EPIDESK.USERS WHERE USERNAME LIKE :username);", Consts.CONTACTS_TABLE, Consts.USERS_TABLE, Consts.ACCESS_REQUEST_TABLE), parameters, values);
            
        }

        public static DataSet SelectRef(string ticketID)
        {
            List<string> parameters = new List<string>();
            List<string> values = new List<string>();

            parameters.Add(":id"); values.Add(ticketID);

            return _db.ExecuteQuery(string.Format("SELECT DISTINCT({0}.REFERENCE) FROM {0}  WHERE {0}.ID LIKE :id", Consts.TICKETS_TABLE), parameters, values);
        }

        /// <summary>
        /// Update the status of a specific access request
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public static bool UpdateRequestStatus(string id, string status)
        {
            List<string> parameters = new List<string>();
            List<string> values = new List<string>();

            parameters.Add(":status");values.Add(status);
            parameters.Add(":id");values.Add(id);

            return _db.ExecuteNonQuery(string.Format("UPDATE {0} SET AR_STATUS = :status WHERE ID = :id",Consts.ACCESS_REQUEST_TABLE), parameters, values);
        }

        /// <summary>
        /// Update the mail of responsible by request id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public static bool UpdateRespEmail(string id, string email)
        {
            List<string> parameters = new List<string>();
            List<string> values = new List<string>();

            parameters.Add(":email"); values.Add(email);
            parameters.Add(":id"); values.Add(id);

            return _db.ExecuteNonQuery(string.Format("UPDATE {0} SET RESP_EMAIL = :email WHERE ID = :id", Consts.ACCESS_REQUEST_TABLE), parameters, values);
        }

        public static bool CloseAccessRequest(string ar_ID, string ticketID)
        {
            List<string> parameters = new List<string>();
            List<string> values = new List<string>();

            parameters.Add(":id"); values.Add(ar_ID);

            bool ret = _db.ExecuteNonQuery(string.Format("UPDATE {0} SET AR_STATUS = 'CLOSED' WHERE ID = :id", Consts.ACCESS_REQUEST_TABLE), parameters, values);
            return CloseTicket(ticketID) && ret;
            
        }

        public static bool CloseTicket(string ticketID)
        {
            List<string> parameters = new List<string>();
            List<string> values = new List<string>();
            
            parameters.Add(":id"); values.Add(ticketID);

            return _db.ExecuteNonQuery(string.Format("UPDATE {0} SET TICKET_STATUS = 'CLOSED' WHERE ID = :id", Consts.TICKETS_TABLE), parameters, values);
        }
    }
}