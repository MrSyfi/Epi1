using EpiDESKUConnectorLib;

namespace AccessApp
{
    public class GlobalVar
    {

        public static ED_UCDBConnection _db;
        //public static Database _db;

        public static ED_UCDBConnection Instance
        {
            get
            {
                if (_db == null)
                {
                    RemoteDBConnection _rdb = new RemoteDBConnection();
                    _rdb.Connect();
                    _rdb.EDUC.EpiDESKUConnectorLib(Consts.CONST_ORACLE_CONNECTION_STRING);
                    _db = _rdb.EDUC;
                }
                return _db;
            }
        }


    }
}