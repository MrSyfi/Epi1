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

        /* GESTION EPIACCESS */

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

        public static bool UpdateRequestStatus(string id, string status)
        {
            List<string> parameters = new List<string>();
            List<string> values = new List<string>();

            parameters.Add(":status"); values.Add(status);
            parameters.Add(":id"); values.Add(id);

            return _db.ExecuteNonQuery(string.Format("UPDATE {0} SET AR_STATUS = :status WHERE ID = :id", Consts.ACCESS_REQUEST_TABLE), parameters, values);
        }

        public static bool UpdateRespEmail(string id, string email)
        {
            List<string> parameters = new List<string>();
            List<string> values = new List<string>();

            parameters.Add(":email"); values.Add(email);
            parameters.Add(":id"); values.Add(id);

            return _db.ExecuteNonQuery(string.Format("UPDATE {0} SET RESP_EMAIL = :email WHERE ID = :id", Consts.ACCESS_REQUEST_TABLE), parameters, values);
        }

        /* EPIACCESS_CLOSE */

        public static DataSet SelectOPReadyFromSearchRequest(string search)
        {
            search = "%" + search.ToUpper() + "%";
            List<string> parameters = new List<string>();
            List<string> values = new List<string>();

            parameters.Add(":search"); values.Add(search);
            return _db.ExecuteQuery(string.Format("SELECT ID, LAST_NAME, FIRST_NAME , USERNAME , PHONE_NBR , SERVICE  , AR_STATUS , RESP_EMAIL, TICKET_ID FROM {0} WHERE ((UPPER(LAST_NAME) LIKE :search OR UPPER(FIRST_NAME) LIKE :search OR UPPER(USERNAME) LIKE :search OR UPPER(SERVICE) LIKE :search  OR UPPER(AR_STATUS) LIKE:search OR UPPER(TICKET_ID) LIKE :search) AND (AR_STATUS NOT LIKE 'CLOSED' AND AR_STATUS NOT LIKE 'REFUSED' AND AR_STATUS LIKE 'OP_READY')) ORDER BY ID DESC", Consts.ACCESS_REQUEST_TABLE), parameters, values);
        }

        public static DataSet SelectAgentEmail(string ticketID)
        {
            List<string> parameters = new List<string>();
            List<string> values = new List<string>();

            parameters.Add(":id"); values.Add(ticketID);

            return _db.ExecuteQuery(string.Format("SELECT DISTINCT(EMAIL) FROM {0} INNER JOIN {1} ON {0}.ID = {1}.AGENT_ID INNER JOIN {2} ON {2}.TICKET_ID = {1}.ID WHERE {2}.TICKET_ID LIKE :id ",Consts.CONTACTS_TABLE, Consts.TICKETS_TABLE, Consts.ACCESS_REQUEST_TABLE), parameters, values);
        }

        public static DataSet SelectRef(string ticketID)
        {
            List<string> parameters = new List<string>();
            List<string> values = new List<string>();

            parameters.Add(":id"); values.Add(ticketID);

            return _db.ExecuteQuery(string.Format("SELECT DISTINCT({0}.REFERENCE) FROM {0}  WHERE {0}.ID LIKE :id", Consts.TICKETS_TABLE), parameters, values);
        }

        public static bool CloseTicket(string ticketID, string gen)
        {
            List<string> parameters = new List<string>();
            List<string> values = new List<string>();
            
            parameters.Add(":gen");values.Add(gen);
            parameters.Add(":res");values.Add(DateTime.Now.ToString());
            parameters.Add(":lastUpdate"); values.Add(DateTime.Now.ToString());
            parameters.Add(":id"); values.Add(ticketID);

            return _db.ExecuteNonQuery(string.Format("UPDATE {0} SET TICKET_STATUS = 'CLOSED', RESOLUTION = :gen, RESOLUTION_TS = :res, LAST_UPDATE_TS = :lastUpdate WHERE ID = :id", Consts.TICKETS_TABLE), parameters, values);
        }

        /* EPI_CMDB */

       

        public static DataSet GetProductPerEpiId(string EpiId)
        {
            List<string> parameters = new List<string>();
            List<string> values = new List<string>();

            parameters.Add(":id");values.Add(EpiId);

            /*
             * 
                {0} Historic
                {1} Contacts
                {2} Localisation
                {3} Brand
                {4} Model
                {5} Stock

            */

            return _db.ExecuteQuery(string.Format("select {0}.EPIID, {0}.OPERATION_DATE,{0}.TICKET_ID, {1}.LAST_NAME, {1}.FIRST_NAME,{1}.ID, {2}.LOCALISATION_ID,{3}.NAME, {4}.NAME AS MODELE ,{5}.SERIAL_NUMBER, {0}.STATUS_TO FROM {0} LEFT OUTER JOIN {1} ON {0}.CONTACT_ID = {1}.ID LEFT OUTER JOIN {2} ON {0}.ID_LOCALISATION_TO = {2}.ID LEFT OUTER JOIN {5} ON {0}.EPIID = {5}.EPIID LEFT OUTER JOIN {3} ON {5}.BRAND_ID = {3}.BRAND_ID LEFT OUTER JOIN {4} ON {5}.MODEL_ID = {4}.MODEL_ID WHERE {0}.EPIID like :id AND {0}.STATUS_TO IN ('STOCKED','INSTALLED','OBSOLETE','TRANSIT','UNDER_REPAIR','COPY_LBL') ORDER BY {0}.OPERATION_DATE DESC", Consts.HISTORIC_TABLE, Consts.CONTACTS_TABLE, Consts.LOCALISATION_TABLE, Consts.BRAND_TABLE, Consts.MODEL_TABLE, Consts.STOCK_TABLE), parameters, values);
        }

        public static DataSet SelectAllFromTicketId(string ticketID)
        {
            List<string> parameters = new List<string>();
            List<string> values = new List<string>();

            parameters.Add(":id"); values.Add(ticketID);

            return _db.ExecuteQuery(string.Format("SELECT * FROM {0} WHERE {0}.ID LIKE :id", Consts.TICKETS_TABLE), parameters, values);
        }

        public static DataSet SelectAgentIdentity(string ticketID)
        {
            List<string> parameters = new List<string>();
            List<string> values = new List<string>();

            parameters.Add(":id"); values.Add(ticketID);

            return _db.ExecuteQuery(string.Format("SELECT FIRST_NAME, LAST_NAME FROM {0} INNER JOIN {1} ON {0}.ID = {1}.AGENT_ID WHERE {1}.ID = :id ", Consts.CONTACTS_TABLE, Consts.TICKETS_TABLE), parameters, values);
        }

        public static DataSet SelectCallerIdentity(string ticketID)
        {
            List<string> parameters = new List<string>();
            List<string> values = new List<string>();

            parameters.Add(":id"); values.Add(ticketID);

            return _db.ExecuteQuery(string.Format("SELECT FIRST_NAME, LAST_NAME FROM {0} INNER JOIN {1} ON {0}.ID = {1}.CALLER_ID WHERE {1}.ID = :id ", Consts.CONTACTS_TABLE, Consts.TICKETS_TABLE), parameters, values);
        
        }

        public static DataSet SelectAllFromCommentaire(string ticketID)
        {
            List<string> parameters = new List<string>();
            List<string> values = new List<string>();

            parameters.Add(":id"); values.Add(ticketID);

            return _db.ExecuteQuery(string.Format("SELECT * FROM {0} WHERE {0}.TICKET_ID LIKE :id ORDER BY {0}.TIMESTAMP DESC", Consts.COMMENTAIRE_TABLE), parameters, values);
        }

        public static DataSet SelectContact(string id)
        {
            List<string> parameters = new List<string>();
            List<string> values = new List<string>();

            parameters.Add(":id"); values.Add(id);

            return _db.ExecuteQuery(string.Format("SELECT * FROM {0} WHERE {0}.ID LIKE :id", Consts.CONTACTS_TABLE), parameters, values);
        }

        /* EPISTOCK */

        public static DataSet SelectLocalisationId(string localisation)
        {
            List<string> parameters = new List<string>();
            List<string> values = new List<string>();

            parameters.Add(":loc"); values.Add(localisation);

            return _db.ExecuteQuery(string.Format("SELECT ID FROM {0} WHERE {0}.LOCALISATION_ID LIKE :loc", Consts.LOCALISATION_TABLE), parameters, values);
        }

        public static DataSet SelectUsernameFromUsers(string id)
        {
            List<string> parameters = new List<string>();
            List<string> values = new List<string>();

            parameters.Add(":id"); values.Add(id);

            return _db.ExecuteQuery(string.Format("SELECT USERNAME FROM {0} WHERE {0}.CONTACT_ID LIKE :id", Consts.USERS_TABLE), parameters, values);

        }

        /// <summary>
        /// Insert an unknown localisation in Database.
        /// </summary>
        /// <param name="localisation"></param>
        /// <returns></returns>
        public static bool InsertLocalisationId(string localisation, string idOp)
        {
            List<string> parameters = new List<string>();
            List<string> values = new List<string>();

            parameters.Add(":loc");values.Add(localisation);
            parameters.Add(":sign");values.Add(SelectUsernameFromUsers(idOp).Tables[0].Rows[0]["USERNAME"].ToString());

            return _db.ExecuteNonQuery(string.Format("INSERT INTO {0}(LOCALISATION_ID, ORG_ID, WING, FLOOR, STATUS, ESIGN) VALUES (:loc,0,0,0,0,:sign)", Consts.LOCALISATION_TABLE), parameters, values);
        }

        public static bool InsertInHistoric(string idOp,string statut, string epiid, string localisationId,string note = "")
        {
            List<string> parameters = new List<string>();
            List<string> values = new List<string>();

            parameters.Add(":epiid");values.Add(epiid);
            parameters.Add(":opid");values.Add(idOp);
            parameters.Add(":dateOp");values.Add(DateTime.Now.ToString());
            parameters.Add(":status");values.Add(statut);
            parameters.Add(":idLoc");values.Add(localisationId);
            parameters.Add(":note"); values.Add(note);
            parameters.Add(":sign");values.Add(SelectUsernameFromUsers(idOp).Tables[0].Rows[0]["USERNAME"].ToString());

            return _db.ExecuteNonQuery(string.Format("INSERT INTO {0}(EPIID, CONTACT_ID, OPERATION_DATE, STATUS_TO, ID_LOCALISATION_TO, TICKET_ID, NOTE, STATUS, ESIGN) VALUES (:epiid, :opid, :dateOp, :status, :idLoc, 0, :note, 0, :sign)", Consts.HISTORIC_TABLE), parameters, values);
        }

        public static bool UpdateStockStatus(string epiid, string statut)
        {
            List<string> parameters = new List<string>();
            List<string> values = new List<string>();


            parameters.Add(":statut"); values.Add(statut);
            parameters.Add(":id");values.Add(epiid);

            return _db.ExecuteNonQuery(string.Format("UPDATE {0} SET {0}.STOCK_STATUS = :statut WHERE {0}.EPIID LIKE :id",Consts.STOCK_TABLE), parameters, values);
        }

        public static DataSet GetProduct(string EpiId)
        {
            List<string> parameters = new List<string>();
            List<string> values = new List<string>();

            parameters.Add(":id"); values.Add(EpiId);

            return _db.ExecuteQuery(string.Format("SELECT * FROM {0} WHERE {0}.EPIID LIKE :id", Consts.STOCK_TABLE), parameters, values);
        }

        public static DataSet GetRespMail()
        {
            List<string> parameters = new List<string>();
            List<string> values = new List<string>();

            return _db.ExecuteQuery(string.Format("SELECT * FROM {0} WHERE {0}.ID LIKE '3'", Consts.APP_PARAM_TABLE), parameters, values);
        }

    }
}
 