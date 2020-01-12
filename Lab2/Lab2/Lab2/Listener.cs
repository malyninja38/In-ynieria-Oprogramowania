using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    class Listener
    {
        private readonly TcpListener _tcpListener;
        private Client _tcpClient;

        public Listener(int port)
        {
            _tcpListener = new TcpListener(IPAddress.Any, port);
            _tcpListener.Start();
        }

        public void AcceptClient()
        {
            _tcpClient = new Client(_tcpListener.AcceptTcpClient(), 1024);
        }
    }

}
