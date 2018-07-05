using System;
using Quobject.SocketIoClientDotNet.Client;

namespace IEX.Api
{
    public class IexMarketDataSubscriber
    {
        private Socket _socket;

        public IexMarketDataSubscriber()
        {
        }

        public void Subscribe(string url, string action, string topics, Action<object> subscribeAction)
        {
            _socket = IO.Socket(url);
            _socket.On(Socket.EVENT_CONNECT, () =>
            {
                _socket.Emit(action, topics);

            });
            _socket.On(Socket.EVENT_MESSAGE, (data) =>
            {
                subscribeAction(data);
            });
        }
    }
}
