using System;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using EpiDESKUConnectorLib;

namespace AccessApp
{
    public class RemoteDBConnection
    {
        public ED_UCDBConnection EDUC;
        public TcpChannel ClientChannel;
        public bool Connect()
        {
            try
            {
                ClientChannel = new TcpChannel();
                ChannelServices.RegisterChannel(ClientChannel, false);
                EDUC = (ED_UCDBConnection)Activator.GetObject(typeof(ED_UCDBConnection), string.Format("tcp://{0}:{1}/{2}", Consts.CONST_NETWORK_EDUC_SERVER, Consts.CONST_NETWORK_EDUC_PORT.ToString(), Consts.CONST_NETWORK_EDUC_SERVICE_NAME));

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Disconnect()
        {
            ClientChannel.StopListening(null);
            ChannelServices.UnregisterChannel(ClientChannel);
            ClientChannel = null;
            return true;
        }
    }
}