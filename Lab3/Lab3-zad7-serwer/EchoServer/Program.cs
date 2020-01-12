using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3_zad7_serwer
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
