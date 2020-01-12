using System;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Threading;

namespace Lab3_zad7_serwer
{

    class Listener
    {
        delegate void clientservicedelegate(object client);

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
                //  ThreadPool.QueueUserWorkItem(ClientService, new Client(_tcpListener.AcceptTcpClient(), 1024));
                clientservicedelegate del = new clientservicedelegate(ClientService);
                writeConsoleMessage("Client connected: " + number, ConsoleColor.Red);
                del.BeginInvoke(new Client(_tcpListener.AcceptTcpClient(), 1024), null, null);
                number++;
            }
        }

        public void ClientService(object client)
        {
            (client as Client).Read();
        }
    }

}
