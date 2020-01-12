using System;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Threading;

namespace Lab2
{
    class Program
    {
        static void Main(string[] args)
        {
            Listener listener = new Listener(8080);
            listener.AcceptClient();
        }
    }
}