using EpiDESKUConnectorLib;

namespace AccessApp
{
    public class DBConnection
    {
        private static ED_UCDBConnection _db = null;
        private static readonly object padlock = new object();

        //Singleton
        public static ED_UCDBConnection Instance
        {
            get
            {
                lock (padlock)
                {
                    if (_db == null)
                    {
                        RemoteDBConnection _rdb = new RemoteDBConnection();
                        //Connexion à l'objet distant.
                        _rdb.Connect();
                        //Définit la chaine de connexion de la base de données
                        _rdb.EDUC.EpiDESKUConnectorLib(Consts.CONST_ORACLE_CONNECTION_STRING);
                        //Instancie ED_UCDBConnection grâce à l'objet récupéré
                        _db = _rdb.EDUC;
                    }
                    return _db;
                }
            }
        }
    }
}