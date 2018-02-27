using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using EpiDESKUConnectorLib;

namespace AccessApp
{
    public class DAL
    {



        /* GESTION EPIACCESS */

        public static DataSet SelectFromSearchRequest(string search)
        {

            // %search% in request for the LIKE Condition
            if (search.ToUpper() != "OP_READY")
                search = "%" + search.ToUpper() + "%";
            else
                // By doing this, we only take the OP_READY (and not OP_READYF) REQUESTS.
                search = "%" + search.ToUpper();


            return GlobalVar.Instance.ExecuteQuery(string.Format("SELECT ID, LAST_NAME, FIRST_NAME , USERNAME , PHONE_NBR , SERVICE  , AR_STATUS , RESP_EMAIL, TICKET_ID FROM {0} WHERE ((UPPER(LAST_NAME) LIKE '{1}' OR UPPER(FIRST_NAME) LIKE '{1}' OR UPPER(USERNAME) LIKE '{1}' OR UPPER(SERVICE) LIKE '{1}'  OR UPPER(AR_STATUS) LIKE '{1}' OR UPPER(TICKET_ID) LIKE '{1}') AND (AR_STATUS NOT LIKE 'CLOSED' AND AR_STATUS NOT LIKE 'REFUSED' AND AR_STATUS NOT LIKE 'ERROR' AND AR_STATUS NOT LIKE 'UNKNOWN' AND AR_STATUS NOT LIKE 'APPROVED')) ORDER BY ID DESC", Consts.ACCESS_REQUEST_TABLE, search));
        }

        public static bool UpdateRequestStatus(string id, string status)
        {

            ArrayList parameters = new ArrayList();
            ArrayList values = new ArrayList();

            parameters.Add(":status"); values.Add(status);
   

            return GlobalVar.Instance.ExecuteNonQuery(string.Format("UPDATE {0} SET AR_STATUS = :status WHERE ID = {1}", Consts.ACCESS_REQUEST_TABLE, id), parameters, values);
        }

        public static bool UpdateRespEmail(string id, string email)
        {
            ArrayList parameters = new ArrayList();
            ArrayList values = new ArrayList();

            parameters.Add(":email"); values.Add(email);

            return GlobalVar.Instance.ExecuteNonQuery(string.Format("UPDATE {0} SET RESP_EMAIL = :email WHERE ID = {1}", Consts.ACCESS_REQUEST_TABLE, id), parameters, values);
        }

        /* EPIACCESS_CLOSE */

        public static DataSet SelectOPReadyFromSearchRequest(string search)
        {
            search = "%" + search.ToUpper() + "%";

            return GlobalVar.Instance.ExecuteQuery(string.Format("SELECT ID, LAST_NAME, FIRST_NAME , USERNAME , PHONE_NBR , SERVICE  , AR_STATUS , RESP_EMAIL, TICKET_ID FROM {0} WHERE ((UPPER(LAST_NAME) LIKE '{1}' OR UPPER(FIRST_NAME) LIKE '{1}' OR UPPER(USERNAME) LIKE '{1}' OR UPPER(SERVICE) LIKE '{1}'  OR UPPER(AR_STATUS) LIKE '{1}' OR UPPER(TICKET_ID) LIKE '{1}') AND (AR_STATUS NOT LIKE 'CLOSED' AND AR_STATUS NOT LIKE 'REFUSED' AND AR_STATUS LIKE 'OP_READY')) ORDER BY ID DESC", Consts.ACCESS_REQUEST_TABLE, search));
        }

        public static DataSet SelectAgentEmail(string ticketID)
        {
            ArrayList parameters = new ArrayList();
            ArrayList values = new ArrayList();

            parameters.Add(":id"); values.Add(ticketID);

            return GlobalVar.Instance.ExecuteQuery(string.Format("SELECT DISTINCT(EMAIL) FROM {0} INNER JOIN {1} ON {0}.ID = {1}.AGENT_ID INNER JOIN {2} ON {2}.TICKET_ID = {1}.ID WHERE {2}.TICKET_ID LIKE '{3}' ", Consts.CONTACTS_TABLE, Consts.TICKETS_TABLE, Consts.ACCESS_REQUEST_TABLE, ticketID));
        }

        public static DataSet SelectRef(string ticketID)
        {

            return GlobalVar.Instance.ExecuteQuery(string.Format("SELECT DISTINCT({0}.REFERENCE) FROM {0}  WHERE {0}.ID LIKE '{1}'", Consts.TICKETS_TABLE, ticketID));
        }

        public static bool CloseTicket(string ticketID, string gen)
        {
            ArrayList parameters = new ArrayList();
            ArrayList values = new ArrayList();

            parameters.Add(":gen"); values.Add(gen);
            parameters.Add(":res"); values.Add(DateTime.Now.ToString());
            parameters.Add(":lastUpdate"); values.Add(DateTime.Now.ToString());

            return GlobalVar.Instance.ExecuteNonQuery(string.Format("UPDATE {0} SET TICKET_STATUS = 'CLOSED', RESOLUTION = :gen, RESOLUTION_TS = :res, LAST_UPDATE_TS = :lastUpdate WHERE ID = {1}", Consts.TICKETS_TABLE, ticketID), parameters, values);
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

            return GlobalVar.Instance.ExecuteQuery(string.Format("select {0}.EPIID, {0}.OPERATION_DATE,{0}.TICKET_ID, {1}.LAST_NAME, {1}.FIRST_NAME,{1}.ID, {2}.LOCALISATION_ID,{3}.NAME, {4}.NAME AS MODELE ,{5}.SERIAL_NUMBER, {0}.STATUS_TO FROM {0} LEFT OUTER JOIN {1} ON {0}.CONTACT_ID = {1}.ID LEFT OUTER JOIN {2} ON {0}.ID_LOCALISATION_TO = {2}.ID LEFT OUTER JOIN {5} ON {0}.EPIID = {5}.EPIID LEFT OUTER JOIN {3} ON {5}.BRAND_ID = {3}.BRAND_ID LEFT OUTER JOIN {4} ON {5}.MODEL_ID = {4}.MODEL_ID WHERE {0}.EPIID like '{6}' AND {0}.STATUS_TO IN ('STOCKED','INSTALLED','OBSOLETE','TRANSIT','UNDER_REPAIR','COPY_LBL') ORDER BY {0}.OPERATION_DATE DESC", Consts.HISTORIC_TABLE, Consts.CONTACTS_TABLE, Consts.LOCALISATION_TABLE, Consts.BRAND_TABLE, Consts.MODEL_TABLE, Consts.STOCK_TABLE, EpiId));
        }

        public static DataSet SelectAllFromTicketId(string ticketID)
        {

            return GlobalVar.Instance.ExecuteQuery(string.Format("SELECT * FROM {0} WHERE {0}.ID LIKE '{1}'", Consts.TICKETS_TABLE, ticketID));
        }

        public static DataSet SelectAgentIdentity(string ticketID)
        {


            return GlobalVar.Instance.ExecuteQuery(string.Format("SELECT FIRST_NAME, LAST_NAME FROM {0} INNER JOIN {1} ON {0}.ID = {1}.AGENT_ID WHERE {1}.ID = '{2}' ", Consts.CONTACTS_TABLE, Consts.TICKETS_TABLE, ticketID));
        }

        public static DataSet SelectCallerIdentity(string ticketID)
        {


            return GlobalVar.Instance.ExecuteQuery(string.Format("SELECT FIRST_NAME, LAST_NAME FROM {0} INNER JOIN {1} ON {0}.ID = {1}.CALLER_ID WHERE {1}.ID = {2} ", Consts.CONTACTS_TABLE, Consts.TICKETS_TABLE, ticketID));

        }

        public static DataSet SelectAllFromCommentaire(string ticketID)
        {



            return GlobalVar.Instance.ExecuteQuery(string.Format("SELECT * FROM {0} WHERE {0}.TICKET_ID LIKE '{1}' ORDER BY {0}.TIMESTAMP DESC", Consts.COMMENTAIRE_TABLE, ticketID));
        }

        public static DataSet SelectContact(string id)
        {


            return GlobalVar.Instance.ExecuteQuery(string.Format("SELECT * FROM {0} WHERE {0}.ID LIKE '{1}'", Consts.CONTACTS_TABLE, id));
        }

        /* EPISTOCK */

        public static DataSet SelectLocalisationId(string localisation)
        {

            return GlobalVar.Instance.ExecuteQuery(string.Format("SELECT ID FROM {0} WHERE {0}.LOCALISATION_ID LIKE '{1}'", Consts.LOCALISATION_TABLE, localisation));
        }

        public static DataSet SelectUsernameFromUsers(string id)
        {

            return GlobalVar.Instance.ExecuteQuery(string.Format("SELECT USERNAME FROM {0} WHERE {0}.CONTACT_ID LIKE '{1}'", Consts.USERS_TABLE, id));

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

            return GlobalVar.Instance.ExecuteNonQuery(string.Format("INSERT INTO {0}(LOCALISATION_ID, ORG_ID, WING, FLOOR, STATUS, ESIGN) VALUES (:loc,0,0,0,0,:sign)", Consts.LOCALISATION_TABLE), parameters, values);
        }

        public static bool InsertInHistoric(string idOp, string statut, string epiid, string localisationId, string note = "")
        {
            ArrayList parameters = new ArrayList();
            ArrayList values = new ArrayList();

            parameters.Add(":epiid"); values.Add(epiid);
            parameters.Add(":opid"); values.Add(idOp);
            parameters.Add(":dateOp"); values.Add(DateTime.Now.ToString());
            parameters.Add(":status"); values.Add(statut);
            parameters.Add(":idLoc"); values.Add(localisationId);
            parameters.Add(":note"); values.Add(note);
            parameters.Add(":sign"); values.Add(SelectUsernameFromUsers(idOp).Tables[0].Rows[0]["USERNAME"].ToString());

            return GlobalVar.Instance.ExecuteNonQuery(string.Format("INSERT INTO {0}(EPIID, CONTACT_ID, OPERATION_DATE, STATUS_TO, ID_LOCALISATION_TO, TICKET_ID, NOTE, STATUS, ESIGN) VALUES (:epiid, :opid, :dateOp, :status, :idLoc, 0, :note, 0, :sign)", Consts.HISTORIC_TABLE), parameters, values);
        }

        public static bool UpdateStockStatus(string epiid, string statut)
        {
            ArrayList parameters = new ArrayList();
            ArrayList values = new ArrayList();


            parameters.Add(":statut"); values.Add(statut);
            parameters.Add(":id"); values.Add(epiid);

            return GlobalVar.Instance.ExecuteNonQuery(string.Format("UPDATE {0} SET {0}.STOCK_STATUS = :statut WHERE {0}.EPIID LIKE :id", Consts.STOCK_TABLE), parameters, values);
        }

        public static DataSet GetProduct(string EpiId)
        {
            ArrayList parameters = new ArrayList();
            ArrayList values = new ArrayList();

            parameters.Add(":id"); values.Add(EpiId);

            return GlobalVar.Instance.ExecuteQuery(string.Format("SELECT * FROM {0} WHERE {0}.EPIID LIKE '{1}'", Consts.STOCK_TABLE, EpiId));
        }

        public static DataSet GetRespMail()
        {
            return GlobalVar.Instance.ExecuteQuery(string.Format("SELECT * FROM {0} WHERE {0}.ID LIKE '3'", Consts.APP_PARAM_TABLE));
        }

        /* EPILABEL */
        public static DataSet SelectAllSites()
        {
            return GlobalVar.Instance.ExecuteQuery(string.Format("SELECT ABBREVIATION FROM {0}", Consts.SITES_TABLE));
        }

        public static DataSet SelectPrinterIP(string siteAbb)
        {
            string site = siteAbb + "_TAG_PRN_IP";

            return GlobalVar.Instance.ExecuteQuery(string.Format("SELECT * FROM {0} WHERE VARIABLE LIKE '{1}'", Consts.APP_PARAM_TABLE, site));
        }

    }
}
