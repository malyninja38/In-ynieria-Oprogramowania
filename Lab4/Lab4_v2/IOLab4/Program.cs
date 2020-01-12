using System;
using System.IO;
using System.Text;
using System.Drawing;

namespace IOLab4
{
    class MainClass
    {
        byte[][] obrazek;                              
        byte[][] filtr;                                         // macierz 3x3, zawiera 1 i 2


        MainClass(int szerokosc, int wysokosc)                       // konstruktor
        {
            obrazek = new byte[szerokosc][];

            for(int i = 0; i < szerokosc; i++)
            {
                obrazek[i] = new byte[wysokosc];                   // ładujemy do niego zera
            }

            filtr = new byte[3][];

            for (int i = 0; i < 3; i++)
            {
                filtr[i] = new byte[3];                     // też ładujemy zera
            }
        }


        byte[][] Workbench(byte[][] img)                                  // tworzy drugą macierz, która dodaje zera naokoło zdjęcia
        {
            byte[][] workbench = new byte[img.Length + 2][];
           
            for (int i = 0; i < workbench.Length; i++)
            {
                workbench[i] = new byte[img[0].Length + 2];                   // tworzymy macierz o dwa większą od zdjęcia (na długość) - robi obwódkę z zer
            }
            
            for(int y = 0; y < workbench[0].Length; y++)
            {
                for (int x = 0; x < img.Length; x++)
                {
                    if(x == 0 || y == 0 || x == workbench.Length-1 || y == workbench[0].Length-1)             // jeśli piksel jest skrajny to ustawiamy go na zero
                    {
                        workbench[x][y] = 0;
                    }
                    else
                    {
                        workbench[x][y] = img[x - 1][y - 1];                                                  // jeśli nie to przepisujemy ze zdjęcia
                    }
                }
            }

            return workbench;
        }


        void Przetwarzanie_obrazka()                                                        // piksel razy wartość sąsiadujących pikseli
        {
            byte[][] workbench = Workbench(obrazek);

            for (int y = 1; y < workbench[0].Length-1; y++)
            {
                for (int x = 1; x < workbench.Length-1; x++)
                {
                    int temp =0;
                    byte K = 0;

                    for(int i = 0; i < 9; i++)
                    {
                        temp += workbench[x - 1 + i % 3][y - 1 + (i / 3)] * filtr[i % 3][i / 3];                      // magia filtra - sumuje się wszystkie 9 pikseli dookoła
                        K += filtr[i % 3][i / 3];                                                                         // K - suma wszystkich wartości w filtrze - wg wiki
                    }

                    if (K == 0) K = 1;
                    obrazek[x-1][y-1] = (byte)(temp/K);                                                     // wpisujemy wartość w pikselu
                }
            }
        }


        public override string ToString()                                             // w przypadku wypisywania klasy wyświetla jako tablicę a nie bajty
        {
            StringBuilder temp = new StringBuilder();
            
            for(int y = 0; y < obrazek[0].Length; y++)
            {
                temp.Append("[");
                
                for(int x = 0; x < obrazek.Length; x++)
                {
                    temp.Append(" ").Append(obrazek[x][y]).Append(" ");
                
                }
                temp.Append("]\n");
            }

            return temp.ToString();
        }


        public void Zaladuj_filtr(byte[][] _filtr)                                                 // Ładujemy filtr kopiując to co przekażemy do wewnętrznego filtra, przekazujemy stworzony w mainie filtr
        {
            if (_filtr.Length != 3 || _filtr[0].Length != 3) return;
            
            for(int i = 0; i < 3; i++)
            {
                for(int j = 0; j < 3; j++)
                {
                    filtr[i][j] = _filtr[i][j];
                }
            }
        }


        public void Zaladuj_obrazek(string path)                                                 // wczytujemy obrazek z pliku i przekazujemy tutaj
        {
            Image img = Image.FromFile(path);
            
            if(img.Width < obrazek.Length || img.Height < obrazek[0].Length)
            {
                return;
            }
            
            byte[] array;
            
            using (MemoryStream mem = new MemoryStream())
            {
                img.Save(mem, System.Drawing.Imaging.ImageFormat.Png);
                array = mem.ToArray();
            }
            
            for(int y = 0; y < obrazek[0].Length; y++)
            {
                for(int x = 0; x < obrazek.Length; x++)
                {
                    obrazek[x][y] = array[y*img.Width+x];
                }
            }
        }


        public static void Main(string[] args)
        {
            MainClass main = new MainClass(100,100);
            
            byte[][] filtr =                                                           // tworzenie filtra
            new byte[][]{
                new byte[]{2, 1, 1},
                new byte[]{1, 2, 1},
                new byte[]{1, 1, 2},
            };
            
            main.Zaladuj_obrazek("C:\\Users\\pauli\\Desktop\\Semestr_5\\IO - Inżynieria Oprogramowania\\Laboratoria\\Lab4\\Lab4_v2\\IOLab4\\bin\\Debug\\dog.png");
            main.Zaladuj_filtr(filtr);
           
            
            Console.WriteLine("Wartość pikseli przed filtrowaniem");
            Console.WriteLine(main);                                                     // Wykona "To string"
            
            main.Przetwarzanie_obrazka();
            
            Console.WriteLine("Wartość pikseli po filtrowaniu");
            Console.WriteLine(main);
        }
    }
}
