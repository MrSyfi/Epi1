using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Web;

namespace AccessApp
{
    public class RemoteDBConnection
    {
        public Database database;
        public bool Connect()
        {
            ChannelServices.RegisterChannel(new System.Runtime.Remoting.Channels.Tcp.TcpClientChannel(), false);
            database = (Database)Activator.GetObject(typeof(Database), string.Format("tcp://{0}:{1}/{2}",Consts.CONST_NETWORK_EDUC_SERVER, Consts.CONST_NETWORK_EDUC_PORT.ToString(), Consts.CONST_NETWORK_EDUC_SERVICE_NAME));
            
            return true;
        }
    }
}