using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace AccessApp
{
    public class Database
    {
        public static readonly string _connectionString = @"DATA SOURCE=HUINFORC17.epicura.lan:1521/XE;PASSWORD=epidesk;USER ID=epidesk";
        public static OracleConnection _connection;

        public Database()
        {
            _connection = new OracleConnection(_connectionString);
            OpenConnection();
        }

        public bool OpenConnection()
        {
            _connection.Open();
            return _connection.State == ConnectionState.Open ? true : false;
        }

        // lecture des demandes d'accès qui ne sont pas terminées.
        // select * from table where .. like %search% or .. like %search% and .. NOT LIKE "terminated"
        public DataSet ExecuteQuery(string query, List<string> parameters,List<string> values)
        {
            DataSet ds = new DataSet();
            OracleCommand command = new OracleCommand(query, _connection);

            for(int i = 0; i<parameters.Count; i++)
            {
                OracleParameter parameter = new OracleParameter
                {
                    ParameterName = parameters[i],
                    Value = values[i]
                };
                command.Parameters.Add(parameter);
            }
            OracleDataAdapter adapter = new OracleDataAdapter(command);
            adapter.Fill(ds);
            return ds;

        }
        
        // Modification du statut de la demande d'accès.
        // update table set .. = .. where .. like %:search% or ... like %search% .... 
        public bool ExecuteNonQuery(string query, List<string> parameters, List<string> values)
        {
            OracleCommand command = new OracleCommand(query, _connection);
            for(int i = 0; i < parameters.Count; i++)
            {
                OracleParameter parameter = new OracleParameter
                {
                    ParameterName = parameters[i],
                    Value = values[i]
                };
                command.Parameters.Add(parameter);
            }
            return command.ExecuteNonQuery() != 0;
        }

       
    }
}