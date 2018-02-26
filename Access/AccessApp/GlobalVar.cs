using EpiDESKUConnectorLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AccessApp
{
    public class GlobalVar
    {

        public static ED_UCDBConnection _db;

        public static void Init()
        {
            RemoteDBConnection _rdb = new RemoteDBConnection();
            _rdb.Connect();
            _rdb.EDUC.EpiDESKUConnectorLib(Consts.CONST_ORACLE_CONNECTION_STRING);
            _db = _rdb.EDUC;
        }
    }
}