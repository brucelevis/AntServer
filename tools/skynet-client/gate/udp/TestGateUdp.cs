using Skynet.DotNetClient.Gate.UDP;
using Skynet.DotNetClient.Utils.Logger;
using Sproto;

namespace Skynet.DotNetClient
{
    public class TestGateUdp
    {
        private GateUdpClient _client;
	
        public void Run (UdpSession udpSession)
        {
            _client = new GateUdpClient(NetWorkStateCallBack);
            _client.Connect(udpSession);
        }
    
        public void Update()
        {
            _client.Update();
        }
        
        public void DisConnect()
        {
            _client.Disconnect();
        }

        private void NetWorkStateCallBack(NetWorkState state)
        {
            SkynetLogger.Info(Channel.NetDevice,"Gate Udp NetWorkStateCallBack:" + state);
            if (state != NetWorkState.Connected) return;
        
            //TODO:发送 与 gate 握手消息成功后 开启 心跳操作
            var handshakeRequset = new SpObject();
            handshakeRequset.Insert("uid", "ddddddddddd");
            _client.Request("handshake", handshakeRequset, (SpObject obj) =>
            { 
                SkynetLogger.Info(Channel.NetDevice,"udp handshake resp");
            });
        }
    }   
}
