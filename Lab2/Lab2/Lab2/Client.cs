using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Lab2
{
    class Client
    {
        private readonly TcpClient tcpClient;
        private readonly byte[] buffer;

        public Client(TcpClient tcpClient, int bufferSize)
        {
            this.tcpClient = tcpClient;
            buffer = new byte[bufferSize];
            Read();
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
                }
            }
        }

        public void Write(byte[] sendBuffer)
        {
            tcpClient.GetStream().Write(sendBuffer, 0, sendBuffer.Length);
        }
    }
}

