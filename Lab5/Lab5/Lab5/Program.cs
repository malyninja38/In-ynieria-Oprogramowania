using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lab5
{
    class Program
    {
        static async Task serverTask()            // Task - typ zadania, typ szablonowy, może przyjąć szablon Task<int> - od razy definiujemy wyjście. bez typu oczekujemy że nic nie zwróci
        {

            TcpListener server = new TcpListener(IPAddress.Any, 8080);
            server.Start();

            while (true)
            {
                TcpClient client = await server.AcceptTcpClientAsync();                             //await - oczekuje na wykonanie operacji asynchroniczenj
                byte[] buffer = new byte[1024];

                _ = client.GetStream().ReadAsync(buffer, 0, buffer.Length).ContinueWith(            // _ = dzięki temu nie podkreśla na zielono, bo nie oczekujemy na task
                   async (t) =>                                                                     // async - founkcja po tym słowie będzie wykonywać się asynchronicznie
                    {

                        int i = t.Result;

                        while (true)
                        {
                            await client.GetStream().WriteAsync(buffer, 0, i);                      // await to to samo co _= (prawie, ale tak)
                            Console.WriteLine(Encoding.ASCII.GetString(buffer, 0, i));
                            try
                            {
                                i = await client.GetStream().ReadAsync(buffer, 0, buffer.Length);            // dzięki temu, że try i catch, to serwer wie, że klient się rozłączył
                            }
                            catch (Exception x) 
                            {
                                Console.WriteLine("Disconnected");
                                return;
                            }
                             
                        }
                    });
            }
        }

        static async Task clientTask(string message)
        {
            TcpClient client = new TcpClient();
            await client.ConnectAsync("localhost", 8080);
            byte[] buffer;
            
                for (int i = 0; i < 5; i++)
                {
                    buffer = Encoding.ASCII.GetBytes(message);
                    await client.GetStream().WriteAsync(buffer, 0, message.Length);
                    Thread.Sleep(10);
                }
           
            client.Close();
        }


        static void Main(string[] args)
        {
            Task t = serverTask();                                                                  // uruchomienie i przechwycienie do zmiennej t tego zadania                                                                              // oczekiwanie na wykonania zadania 

            Console.WriteLine(t.Status);

            Task aaa = clientTask("AAA");
            Task bbb = clientTask("BBB");
            Task ccc = clientTask("CCC");

            t.Wait();
            aaa.Wait();
            bbb.Wait();
            ccc.Wait();
        }
    }
}
