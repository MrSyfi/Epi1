using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace EpiDESKMUI
{
    class ED_Categories : ED_Core
    {
        
        public Info Information;

        public struct Info
        {
            public long ID;
            public string Name;
            public string Description;
            public long Parent_ID;
            public CategoryType CategType;
        }
        public enum CategoryType {MAIN, SUB, UNDEFINED }

        // Récupérer le nombre de catégories
        public static long GetTotalCategories(CategoryType CategType = CategoryType.MAIN)
        {
            string _Table;

            if (CategType == CategoryType.MAIN) _Table = ED_Constants.CONST_DATABASE_CATEGORIES;
            else _Table = ED_Constants.CONST_DATABASE_CATEGORIES_SUB;

            return (long)ED_GlobalVars.DBConnection.ExecuteQuery("SELECT CAST(COUNT(*) AS NUMBER(10)) FROM " + _Table + " WHERE STATUS = " + ED_Constants.CONST_DB_FIELDS_ACTIVE_STATUS).Tables[0].Rows[0][0];
        }

        // Récupérer les catégories d'une organisation -> Organisation ?
        public static DataSet GetCategories(long OrganisationID)
        {
            return ED_GlobalVars.DBConnection.ExecuteQuery(string.Format("SELECT ID,NAME FROM {0} WHERE ORG_ID = {1} AND STATUS = {2} ORDER BY NAME ASC", ED_Constants.CONST_DATABASE_CATEGORIES, OrganisationID,ED_Constants.CONST_DB_FIELDS_ACTIVE_STATUS));
        }

        // Récupérer les sous-catégories d'une catégorie.
        public static DataSet GetSubCategories(long ParentID)
        {
            return ED_GlobalVars.DBConnection.ExecuteQuery(string.Format("SELECT ID,NAME FROM {0} WHERE PARENT_ID = {1} AND STATUS = {2} ORDER BY NAME ASC", ED_Constants.CONST_DATABASE_CATEGORIES_SUB, ParentID, ED_Constants.CONST_DB_FIELDS_ACTIVE_STATUS));
        }

        // Récupérer le nom d'une catégorie par son ID.
        public string GetName(long CategoryID, CategoryType CategType = CategoryType.MAIN)
        {
            DataSet _Ds = new DataSet();
            string _Table;

            if (CategType == CategoryType.MAIN) _Table = ED_Constants.CONST_DATABASE_CATEGORIES;
            else _Table = ED_Constants.CONST_DATABASE_CATEGORIES_SUB;
                
            _Ds = _DBConnection.ExecuteQuery("SELECT NAME FROM " + _Table + " WHERE ID = " + CategoryID);

            if (_Ds.Tables[0].Rows.Count > 0)
            {
                return _Ds.Tables[0].Rows[0][0].ToString();
            }
            else return ED_Constants.CONST_EPIDESK_NO_VALUE;
        }

        // Récupérer le parent d'une catégorie.
        public string GetParentName(long ParentCategoryID, CategoryType CategType = CategoryType.MAIN)
        {
            DataSet _Ds = new DataSet();
            string _Table;

            if (CategType == CategoryType.MAIN) _Table = ED_Constants.CONST_DATABASE_ORGANIZATIONS;
            else _Table = ED_Constants.CONST_DATABASE_CATEGORIES;

            _Ds = _DBConnection.ExecuteQuery("SELECT NAME FROM " + _Table + " WHERE ID = " + ParentCategoryID);

            if (_Ds.Tables[0].Rows.Count > 0)
            {
                return _Ds.Tables[0].Rows[0][0].ToString();
            }
            else return ED_Constants.CONST_EPIDESK_NO_VALUE;
        }

        // Récupérer la description d'une catégorie.
        public string GetDescription(long CategoryID, CategoryType CategType = CategoryType.MAIN)
        {
            DataSet _Ds = new DataSet();
            string _Table;

            if (CategType == CategoryType.MAIN) _Table = ED_Constants.CONST_DATABASE_CATEGORIES;
            else _Table = ED_Constants.CONST_DATABASE_CATEGORIES_SUB;

            _Ds = _DBConnection.ExecuteQuery("SELECT DESCRIPTION FROM " + _Table + " WHERE ID = " + CategoryID);

            if (_Ds.Tables[0].Rows.Count > 0)
            {
                return _Ds.Tables[0].Rows[0][0].ToString();
            }
            else return ED_Constants.CONST_EPIDESK_NO_VALUE;
        }

        public void SetCategory(long CategoryID, CategoryType CategType = CategoryType.MAIN)
        {
            DataSet _ds = new DataSet();
            string _Table;

            if (CategType == CategoryType.MAIN) _Table = ED_Constants.CONST_DATABASE_CATEGORIES;
            else _Table = ED_Constants.CONST_DATABASE_CATEGORIES_SUB;


            _ds = _DBConnection.ExecuteQuery("SELECT * FROM " + _Table + " WHERE ID = " + CategoryID);
            if (_ds.Tables[0].Rows.Count > 0)
            {
                Information.ID = CategoryID;
                Information.Name = _ds.Tables[0].Rows[0][1].ToString();
                Information.Description = _ds.Tables[0].Rows[0][2].ToString();
                Information.Parent_ID = (long)_ds.Tables[0].Rows[0][3];
                Information.CategType = CategType;
            }
            else
            {
                Information.Name = ED_Constants.CONST_EPIDESK_NO_VALUE;
                Information.Description = ED_Constants.CONST_EPIDESK_NO_VALUE;
                Information.Parent_ID = 0;
                Information.CategType = CategoryType.UNDEFINED;
            }
        }


        /*
         * 
         * CRUD
         * 
         */
        // Ajouter une catégorie.
        public override string Add()
        {
            //EDCore
            base.Add();
            string _Table;

            if (Information.CategType == CategoryType.MAIN) _Table = ED_Constants.CONST_DATABASE_CATEGORIES;
            else _Table = ED_Constants.CONST_DATABASE_CATEGORIES_SUB;

            try
            {
                System.Collections.ArrayList Parameters = new System.Collections.ArrayList();
                System.Collections.ArrayList Values = new System.Collections.ArrayList();

                Parameters.Add(":Name"); Values.Add(Information.Name);
                Parameters.Add(":Description"); Values.Add(Information.Description);
                Parameters.Add(":ParentID"); Values.Add(Information.Parent_ID);
                Parameters.Add(":Status"); Values.Add(ED_Constants.CONST_DB_FIELDS_ACTIVE_STATUS);
                Parameters.Add(":ESIGN"); Values.Add(ED_GlobalVars.EDUsers.Information.FullUsername);

                // CF ED_DBConnection
                _DBConnection.ExecuteNonQuery("INSERT INTO " + _Table + " VALUES (null,:Name,:Description,:ParentID,:Status,:ESIGN)", Values, Parameters);

                return ED_Constants.CONST_EPIDESK_SUCCESS_MESSAGE;
            }
            catch (Exception e)
            {
                return e.Message.ToString();
            }
        }

        // Modifier une catégorie
        public override string Update()
        {
            // cf. ED_Core
            base.Update();
            string _Table;
            string _Field;

            if (Information.CategType == CategoryType.MAIN) _Table = ED_Constants.CONST_DATABASE_CATEGORIES;
            else _Table = ED_Constants.CONST_DATABASE_CATEGORIES_SUB;
            
            try
            {
                System.Collections.ArrayList Parameters = new System.Collections.ArrayList();
                System.Collections.ArrayList Values = new System.Collections.ArrayList();

                Parameters.Add(":Name"); Values.Add(Information.Name);
                Parameters.Add(":Description"); Values.Add(Information.Description);
                Parameters.Add(":ParentID"); Values.Add(Information.Parent_ID);

                if (Information.CategType == CategoryType.MAIN) _Field = "ORG_ID";
                else _Field = "PARENT_ID";
                
                _DBConnection.ExecuteNonQuery("UPDATE " + _Table + " SET NAME = :Name, DESCRIPTION = :Description, " + _Field + " = :ParentID WHERE ID = " + Information.ID,Values,Parameters);
                
                return ED_Constants.CONST_EPIDESK_SUCCESS_MESSAGE;
            }
            catch (Exception e)
            {
                return e.Message.ToString();
            }
        }

        // Delete a category
        public override string Delete()
        {
            // ED_Core
            base.Delete();
            
            string _Table;

            if (Information.CategType == CategoryType.MAIN) _Table = ED_Constants.CONST_DATABASE_CATEGORIES;
            else _Table = ED_Constants.CONST_DATABASE_CATEGORIES_SUB;
            
            try
            {
                // Sets the statut of the (sub)category to deleted.
                _DBConnection.ExecuteNonQuery("UPDATE " + _Table + " SET STATUS = " + ED_Constants.CONST_DB_FIELDS_DELETED_STATUS + " WHERE ID = " + Information.ID);
                return ED_Constants.CONST_EPIDESK_SUCCESS_MESSAGE;
            }
            catch (Exception e)
            {
                ED_GlobalVars.EDSecurity.Log(this.ToString(), ED_Security.Action.QUERY, e.Message.ToString(), ED_Security.Status.ERROR);
                return e.Message.ToString();
            }
        }

        // Get categories according to a search by name.
        public DataSet GetCategories(string Search, CategoryType CategType = CategoryType.MAIN)
        {
            string _TableA;
            string _TableB;
            string _Field;

            if (CategType == CategoryType.MAIN)
            {
                _TableA = ED_Constants.CONST_DATABASE_CATEGORIES;
                _TableB = ED_Constants.CONST_DATABASE_ORGANIZATIONS;
                _Field = ED_Constants.CONST_DATABASE_CATEGORIES_PARENT_FIELD;
            }
            else
            {
                _TableA = ED_Constants.CONST_DATABASE_CATEGORIES_SUB;
                _TableB = ED_Constants.CONST_DATABASE_CATEGORIES;
                _Field = ED_Constants.CONST_DATABASE_CATEGORIES_SUB_PARENT_FIELD;
            }

            return _DBConnection.ExecuteQuery("SELECT A.ID, A.NAME AS \"NOM\", A.DESCRIPTION, B.NAME AS \"PARENT\" FROM " + _TableA + " A, " + _TableB + " B WHERE (UPPER(A.NAME) LIKE :Search OR UPPER(A.DESCRIPTION) LIKE :Search OR UPPER(B.NAME) LIKE :Search) AND A.STATUS = 0 AND A." + _Field + " = B.ID ORDER BY A.NAME ASC", "%" + Search.ToUpper() + "%", ":Search");
        }

        // Get Parent of a category (an organization) or of a sub-category(acategory) by name
        public DataSet GetParents(string Search, CategoryType CategType = CategoryType.MAIN)
        {
            string _Table;

            if (CategType == CategoryType.MAIN) _Table = ED_Constants.CONST_DATABASE_ORGANIZATIONS;
            else _Table = ED_Constants.CONST_DATABASE_CATEGORIES;

            return _DBConnection.ExecuteQuery("SELECT ID, NAME AS \"NOM\" FROM " + _Table + " WHERE UPPER(NAME) LIKE :Search  AND STATUS = " + ED_Constants.CONST_DB_FIELDS_ACTIVE_STATUS + " ORDER BY NAME ASC", "%" + Search.ToUpper() + "%", ":Search");
        }
    }
}
