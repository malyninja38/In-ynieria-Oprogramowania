using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lab3
{
    class Listener
    {
        private readonly TcpListener _tcpListener;
        private Client _tcpClient;
        private int number = 1;

        static void writeConsoleMessage(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public Listener(int port)
        {
            _tcpListener = new TcpListener(IPAddress.Any, port);
            _tcpListener.Start();
        }

        public void AcceptClient()
        {
            while (true)
            {
                ThreadPool.QueueUserWorkItem(Callback, new Client(_tcpListener.AcceptTcpClient(), 1024));
                writeConsoleMessage("Client connected: " + number, ConsoleColor.Red);
                number++;
            }
        }

        public void Callback(object client) {
            (client as Client).Read();
        }
    }

}
