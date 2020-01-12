using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lab3_zad7
{
    class Program
    {

        delegate int zad7delegate(object args);                         // Wskaźnik na funkcję zwracającą int i przyjmującą argumenty
                                                                        // Delegat jest typem, który bezpiecznie hermetyzuje metodę, podobną do wskaźnika funkcji w C i C++

        private static int ReadFile(object args)
        {
            FileStream fs = (args as object[])[0] as FileStream;
            byte[] buffer = (args as object[])[1] as byte[];
            return fs.Read(buffer, 0, 1024);
        }

        static void zad7() {

            zad7delegate Delegat;

            FileStream fs = File.Open("C:\\Users\\pauli\\Desktop\\Semestr_5\\IO - Inżynieria Oprogramowania\\Laboratoria\\Lab3\\Lab3\\Lab3\\tekst.txt", FileMode.Open);

            byte[] buffer = new byte[1024];

            Delegat = new zad7delegate(ReadFile);                                                     // Wywołanie funkcji ReadFile
            var asResult = Delegat.BeginInvoke(new object[] { fs, buffer }, null, null);              // Nie ma callback i argumentów do niego
            Delegat.EndInvoke(asResult);
            
            fs.Close();
            
            Console.Write(Encoding.UTF8.GetString(buffer as byte[]));
        }


        static void Main(string[] args)
        {

            zad7();

        }
    }

}
