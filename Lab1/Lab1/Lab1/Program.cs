using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpListener serwer = new TcpListener(IPAddress.Parse("127.0.0.1"), 8080);
            serwer.Start();

            TcpClient klient = serwer.AcceptTcpClient();

            Byte[] tablica = new Byte[100];
            NetworkStream stream = klient.GetStream();

            while (true)
            {
                int readBytes = stream.Read(tablica,0,100);
                if (readBytes != 0)
                {
                    stream.Write(tablica, 0, readBytes);
                }
            }
        }
    }
}
