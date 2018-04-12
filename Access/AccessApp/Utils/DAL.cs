using System;
using System.Collections;
using System.Data;

namespace AccessApp
{
    public class DAL
    {

        /* Generic */


        /* GESTION EPIACCESS */

        public static DataSet SelectFromSearchRequest(string search)
        {
            if (search != "ALL")
            {
                // %search% in request for the LIKE Condition
                if (search.ToUpper() != "OP_READY")
                    search = "%" + search.ToUpper() + "%";
                else
                    // By doing this, we only take the OP_READY (and not OP_READYF) REQUESTS.
                    search = "%" + search.ToUpper();


                return DBConnection.Instance.ExecuteQuery(string.Format("SELECT ID, LAST_NAME, FIRST_NAME , USERNAME , PHONE_NBR , SERVICE  , AR_STATUS , RESP_EMAIL, TICKET_ID FROM {0} WHERE ((UPPER(LAST_NAME) LIKE :search OR UPPER(FIRST_NAME) LIKE :search OR UPPER(USERNAME) LIKE :search OR UPPER(SERVICE) LIKE :search OR UPPER(AR_STATUS) LIKE :search OR UPPER(TICKET_ID) LIKE :search) AND (AR_STATUS NOT LIKE 'CLOSED' AND AR_STATUS NOT LIKE 'REFUSED' AND AR_STATUS NOT LIKE 'ERROR' AND AR_STATUS NOT LIKE 'UNKNOWN' AND AR_STATUS NOT LIKE 'APPROVED') AND STATUS = 0) ORDER BY ID DESC", Consts.ACCESS_REQUEST_TABLE), search, ":search");
            } else
            {
                return DBConnection.Instance.ExecuteQuery(string.Format("SELECT ID, LAST_NAME, FIRST_NAME , USERNAME , PHONE_NBR , SERVICE  , AR_STATUS , RESP_EMAIL, TICKET_ID FROM {0} WHERE AR_STATUS NOT LIKE 'CLOSED' AND AR_STATUS NOT LIKE 'REFUSED' AND AR_STATUS NOT LIKE 'ERROR' AND AR_STATUS NOT LIKE 'UNKNOWN' AND AR_STATUS NOT LIKE 'APPROVED' AND STATUS = 0 ORDER BY ID DESC", Consts.ACCESS_REQUEST_TABLE));

            }
        }

        public static bool UpdateRequestStatus(string id, string status)
        {

            ArrayList parameters = new ArrayList();
            ArrayList values = new ArrayList();

            parameters.Add(":status"); values.Add(status);
   

            return DBConnection.Instance.ExecuteNonQuery(string.Format("UPDATE {0} SET AR_STATUS = :status WHERE ID = {1}", Consts.ACCESS_REQUEST_TABLE, id), values, parameters);
        }

        public static bool UpdateRespEmail(string id, string email)
        {
            ArrayList parameters = new ArrayList();
            ArrayList values = new ArrayList();

            parameters.Add(":email"); values.Add(email);

            return DBConnection.Instance.ExecuteNonQuery(string.Format("UPDATE {0} SET RESP_EMAIL = :email WHERE ID = {1}", Consts.ACCESS_REQUEST_TABLE, id), values, parameters);
        }

        /* EPIACCESS_CLOSE */

        public static DataSet SelectOPReadyFromSearchRequest(string search)
        {
            if (search != "ALL")
            {
                search = "%" + search.ToUpper() + "%";

                return DBConnection.Instance.ExecuteQuery(string.Format("SELECT ID, LAST_NAME, FIRST_NAME , USERNAME , PHONE_NBR , SERVICE  , AR_STATUS , RESP_EMAIL, TICKET_ID FROM {0} WHERE ((UPPER(LAST_NAME) LIKE :search OR UPPER(FIRST_NAME) LIKE :search OR UPPER(USERNAME) LIKE :search OR UPPER(SERVICE) LIKE :search  OR UPPER(AR_STATUS) LIKE :search OR UPPER(TICKET_ID) LIKE :search) AND (AR_STATUS NOT LIKE 'CLOSED' AND AR_STATUS NOT LIKE 'REFUSED' AND AR_STATUS LIKE 'OP_READY') AND STATUS = 0) ORDER BY ID DESC", Consts.ACCESS_REQUEST_TABLE), search, ":search");
            } else
            {
                return DBConnection.Instance.ExecuteQuery(string.Format("SELECT ID, LAST_NAME, FIRST_NAME , USERNAME , PHONE_NBR , SERVICE  , AR_STATUS , RESP_EMAIL, TICKET_ID FROM {0} WHERE AR_STATUS NOT LIKE 'CLOSED' AND AR_STATUS NOT LIKE 'REFUSED' AND AR_STATUS LIKE 'OP_READY' AND STATUS = 0 ORDER BY ID DESC", Consts.ACCESS_REQUEST_TABLE));
            }
        }

        public static DataSet SelectAgentEmail(string ticketID)
        {
            return DBConnection.Instance.ExecuteQuery(string.Format("SELECT DISTINCT(EMAIL) FROM {0} INNER JOIN {1} ON {0}.ID = {1}.AGENT_ID INNER JOIN {2} ON {2}.TICKET_ID = {1}.ID WHERE {2}.TICKET_ID LIKE '{3}' ", Consts.CONTACTS_TABLE, Consts.TICKETS_TABLE, Consts.ACCESS_REQUEST_TABLE, ticketID));
        }

        public static DataSet SelectRef(string ticketID)
        {
            return DBConnection.Instance.ExecuteQuery(string.Format("SELECT DISTINCT({0}.REFERENCE) FROM {0}  WHERE {0}.ID LIKE '{1}'", Consts.TICKETS_TABLE, ticketID));
        }

        public static bool CloseTicket(string ticketID, string gen)
        {
            ArrayList parameters = new ArrayList();
            ArrayList values = new ArrayList();

            parameters.Add(":gen"); values.Add(gen);

            return DBConnection.Instance.ExecuteNonQuery(string.Format("UPDATE {0} SET TICKET_STATUS = 'CLOSED', RESOLUTION = :gen, RESOLUTION_TS = SYSDATE, LAST_UPDATE_TS = SYSDATE WHERE ID = {1}", Consts.TICKETS_TABLE, ticketID), values, parameters);
        }

        public static bool InsertInTicketLog(long TicketID, string From, string Action, string To)
        {
            ArrayList Parameters = new ArrayList();
            ArrayList Values = new ArrayList();

            Parameters.Add(":TicketID"); Values.Add(TicketID);
            Parameters.Add(":FromS"); Values.Add(From);
            Parameters.Add(":ActionT"); Values.Add(Action.ToString());
            Parameters.Add(":ToS"); Values.Add(To);
            Parameters.Add(":ESIGN"); Values.Add(Environment.UserDomainName + @"\" + Environment.UserName);
            return DBConnection.Instance.ExecuteNonQuery(string.Format("INSERT INTO {0} VALUES (null,:TicketID,SYSDATE,:FromS,:ActionT,:ToS,{1},:ESIGN)", Consts.TICKETS_LOG_TABLE, Consts.CONST_DB_FIELDS_ACTIVE_STATUS), Values, Parameters);
        }

        /* EPI_CMDB */



        public static DataSet GetProductPerEpiId(string EpiId)
        {

            /*
             * 
                {0} Historic
                {1} Contacts
                {2} Localisation
                {3} Brand
                {4} Model
                {5} Stock
            */

            return DBConnection.Instance.ExecuteQuery(string.Format("select {0}.EPIID, {0}.OPERATION_DATE,{0}.TICKET_ID, {1}.LAST_NAME, {1}.FIRST_NAME,{1}.ID, {2}.LOCALISATION_ID,{3}.NAME, {4}.NAME AS MODELE ,{5}.SERIAL_NUMBER, {0}.STATUS_TO FROM {0} LEFT OUTER JOIN {1} ON {0}.CONTACT_ID = {1}.ID LEFT OUTER JOIN {2} ON {0}.ID_LOCALISATION_TO = {2}.ID LEFT OUTER JOIN {5} ON {0}.EPIID = {5}.EPIID LEFT OUTER JOIN {3} ON {5}.BRAND_ID = {3}.BRAND_ID LEFT OUTER JOIN {4} ON {5}.MODEL_ID = {4}.MODEL_ID WHERE {0}.EPIID like :id AND {0}.STATUS_TO IN ('STOCKED','INSTALLED','OBSOLETE','TRANSIT','UNDER_REPAIR','COPY_LBL') AND {4}.STATUS = 0 AND {5}.STATUS = 0 ORDER BY {0}.OPERATION_DATE DESC", Consts.HISTORIC_TABLE, Consts.CONTACTS_TABLE, Consts.LOCALISATION_TABLE, Consts.BRAND_TABLE, Consts.MODEL_TABLE, Consts.STOCK_TABLE),EpiId,":id");
        }

        public static DataSet SelectAllFromTicketId(string ticketID)
        {

            return DBConnection.Instance.ExecuteQuery(string.Format("SELECT * FROM {0} WHERE {0}.ID LIKE '{1}'", Consts.TICKETS_TABLE, ticketID));
        }

        public static DataSet SelectAgentIdentity(string ticketID)
        {


            return DBConnection.Instance.ExecuteQuery(string.Format("SELECT FIRST_NAME, LAST_NAME FROM {0} INNER JOIN {1} ON {0}.ID = {1}.AGENT_ID WHERE {1}.ID = '{2}' ", Consts.CONTACTS_TABLE, Consts.TICKETS_TABLE, ticketID));
        }

        public static DataSet SelectCallerIdentity(string ticketID)
        {


            return DBConnection.Instance.ExecuteQuery(string.Format("SELECT FIRST_NAME, LAST_NAME FROM {0} INNER JOIN {1} ON {0}.ID = {1}.CALLER_ID WHERE {1}.ID = {2} ", Consts.CONTACTS_TABLE, Consts.TICKETS_TABLE, ticketID));

        }

        public static DataSet SelectAllFromCommentaire(string ticketID)
        {



            return DBConnection.Instance.ExecuteQuery(string.Format("SELECT * FROM {0} WHERE {0}.TICKET_ID LIKE '{1}' ORDER BY {0}.TIMESTAMP DESC", Consts.COMMENTAIRE_TABLE, ticketID));
        }

        public static DataSet SelectContact(string id)
        {


            return DBConnection.Instance.ExecuteQuery(string.Format("SELECT * FROM {0} WHERE {0}.ID LIKE '{1}'", Consts.CONTACTS_TABLE, id));
        }

        /* EPISTOCK */

        public static DataSet SelectLocalisationId(string localisation)
        {

            return DBConnection.Instance.ExecuteQuery(string.Format("SELECT ID FROM {0} WHERE {0}.LOCALISATION_ID LIKE :loc", Consts.LOCALISATION_TABLE),localisation,":loc");
        }

        public static DataSet SelectUsernameFromUsers(string id)
        {
            

            return DBConnection.Instance.ExecuteQuery(string.Format("SELECT USERNAME FROM {0} WHERE {0}.CONTACT_ID LIKE :id AND {0}.STATUS = 0", Consts.USERS_TABLE),id,":id");

        }

        /// <summary>
        /// Insert an unknown localisation in Database.
        /// </summary>
        /// <param name="localisation"></param>
        /// <returns></returns>
        public static bool InsertLocalisationId(string localisation, string idOp)
        {
            ArrayList parameters = new ArrayList();
            ArrayList values = new ArrayList();

            parameters.Add(":loc"); values.Add(localisation);
            parameters.Add(":sign"); values.Add(SelectUsernameFromUsers(idOp).Tables[0].Rows[0]["USERNAME"].ToString());

            return DBConnection.Instance.ExecuteNonQuery(string.Format("INSERT INTO {0}(LOCALISATION_ID, ORG_ID, WING, FLOOR, STATUS, ESIGN) VALUES (:loc,0,0,0,0,:sign)", Consts.LOCALISATION_TABLE), values, parameters);
        }

        public static bool InsertInHistoric(string idOp, string statut, string epiid, string localisationId, string ticketId="0", string note = "")
        {
            ArrayList parameters = new ArrayList();
            ArrayList values = new ArrayList();
            parameters.Add(":tId");values.Add(ticketId);
            parameters.Add(":note"); values.Add(note);


            return DBConnection.Instance.ExecuteNonQuery(string.Format("INSERT INTO {0}(EPIID, CONTACT_ID, OPERATION_DATE, STATUS_TO, ID_LOCALISATION_TO, TICKET_ID, NOTE, STATUS, ESIGN) VALUES ({1}, {2}, SYSDATE, '{3}', {4}, :tId, :note , 0, '{6}')", Consts.HISTORIC_TABLE, epiid, idOp, statut, localisationId,ticketId,SelectUsernameFromUsers(idOp).Tables[0].Rows[0]["USERNAME"].ToString()), values, parameters);

        }

        public static bool InsertTicketsComment(string ticketId, string contactId, string text, string idOp)
        {
            ArrayList parameters = new ArrayList();
            ArrayList values = new ArrayList();

            parameters.Add(":ticketid"); values.Add(ticketId);
            parameters.Add(":contactId"); values.Add(contactId);
            parameters.Add(":log"); values.Add(text);

            if (idOp == "1")
            {
                parameters.Add(":sign"); values.Add("SYSTEM");
            }
            else
            {
                parameters.Add(":sign"); values.Add(SelectUsernameFromUsers(idOp).Tables[0].Rows[0]["USERNAME"].ToString());
            }

            return DBConnection.Instance.ExecuteNonQuery(string.Format("INSERT INTO {0}(TICKET_ID, CONTACT_ID, LOG, TIMESTAMP, STATUS, ESIGN) VALUES (:ticketid, :contactId, :log, SYSDATE, 0, :sign) ", Consts.TICKETS_COMMENTS), values, parameters);
        }

        public static bool UpdateStockStatus(string epiid, string statut)
        {
            ArrayList parameters = new ArrayList();
            ArrayList values = new ArrayList();


            parameters.Add(":statut"); values.Add(statut);
            parameters.Add(":id"); values.Add(epiid);

            return DBConnection.Instance.ExecuteNonQuery(string.Format("UPDATE {0} SET {0}.STOCK_STATUS = :statut WHERE {0}.EPIID LIKE :id", Consts.STOCK_TABLE), values, parameters);
        }

        public static DataSet GetProduct(string EpiId)
        {
            return DBConnection.Instance.ExecuteQuery(string.Format("SELECT * FROM {0} WHERE {0}.EPIID LIKE :id AND {0}.STATUS = 0", Consts.STOCK_TABLE),EpiId,":id");
        }

        public static DataSet GetRespMail()
        {
            return DBConnection.Instance.ExecuteQuery(string.Format("SELECT * FROM {0} WHERE {0}.ID LIKE '3'", Consts.APP_PARAM_TABLE));
        }

        /* EPILABEL */
        public static DataSet SelectAllSites()
        {
            return DBConnection.Instance.ExecuteQuery(string.Format("SELECT ABBREVIATION FROM {0} WHERE {0}.STATUS = 0", Consts.SITES_TABLE));
        }

        public static DataSet SelectPrinterIP(string siteAbb)
        {
            string site = siteAbb + "_TAG_PRN_IP";

            return DBConnection.Instance.ExecuteQuery(string.Format("SELECT * FROM {0} WHERE VARIABLE LIKE '{1}'", Consts.APP_PARAM_TABLE, site));
        }

        /* LinkObject */

            public static DataSet SelectFromTicketObjects(string ticketId)
        {

            return DBConnection.Instance.ExecuteQuery(string.Format("SELECT * FROM {0} WHERE TICKET_ID = :ticket AND TYPE LIKE 'DEVICE'", Consts.TICKET_OBJECTS_TABLE),ticketId, ":ticket");
        }

        public static bool InsertInTicketObject(string ticket_id, string type, string sValue, string idOp)
        {
            ArrayList parameters = new ArrayList();
            ArrayList values = new ArrayList();


            parameters.Add(":ticket"); values.Add(ticket_id);
            parameters.Add(":type"); values.Add(type);
            parameters.Add(":value"); values.Add(sValue);
            parameters.Add(":ESIGN");values.Add(SelectUsernameFromUsers(idOp).Tables[0].Rows[0]["USERNAME"].ToString());

            return DBConnection.Instance.ExecuteNonQuery(string.Format("INSERT INTO {0} VALUES (null, :ticket, :type, :value, null, 0, :ESIGN)",Consts.TICKET_OBJECTS_TABLE),values, parameters);
        }

        /* Log On */
        public static DataSet SelectCredentialsFromAppParam()
        {
            return DBConnection.Instance.ExecuteQuery(string.Format("SELECT * FROM {0} WHERE ID = 22 OR ID = 23",Consts.APP_PARAM_TABLE));
        }

    }
}
