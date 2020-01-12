using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Lab3
{
    class Client
    {
        private readonly TcpClient tcpClient;
        private readonly byte[] buffer;

        public Client(TcpClient tcpClient, int bufferSize)
        {
            this.tcpClient = tcpClient;
            buffer = new byte[bufferSize];
        }

        static void writeConsoleMessage(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }


        public void Read()
        {
            var stream = tcpClient.GetStream();
            while (true)
            {
                var readBytes = stream.Read(buffer, 0, buffer.Length);
                byte[] send = new byte[readBytes];
                if (readBytes != 0)
                {
                    Array.Copy(buffer, send, readBytes);
                    Write(send);
                    string clientInput = Encoding.UTF8.GetString(buffer, 0, readBytes).Replace("\r\n", "");
                    writeConsoleMessage("Otrzymałem wiadomość: " + clientInput, ConsoleColor.Green);
                }
            }
        }

        public void Write(byte[] sendBuffer)
        {
            tcpClient.GetStream().Write(sendBuffer, 0, sendBuffer.Length);
        }
    }
}

