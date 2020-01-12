using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lab3
{
    class Program
    {

        static void myAsyncCallback(IAsyncResult state)
        {

            object[] args = (object[])state.AsyncState;
            FileStream fs = args[0] as FileStream;
            byte[] data = (byte[])args[1];
            Console.WriteLine(Encoding.UTF8.GetString(data));
            fs.Close();

        }


        static void Main(string[] args)
        {
        
            byte[] buffer = new byte[255];

            FileStream fs = new FileStream("C:\\Users\\pauli\\Desktop\\Semestr_5\\IO - Inżynieria Oprogramowania\\Laboratoria\\Lab3\\Lab3\\Lab3\\tekst.txt", FileMode.Open);
            fs.BeginRead(buffer, 0, buffer.Length, myAsyncCallback, new object[] { fs, buffer });

            Thread.Sleep(1000);

        }
    }

}
