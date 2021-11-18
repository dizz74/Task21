using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Task21
{
    class Program
    {
        public static Object locker = new Object();
        const int Size = 10;
        static char[,] area = new char[Size, Size];
        static void Main(string[] args)
        {
            for (int i = 0; i < Size; i++)
                for (int j = 0; j < Size; j++)
                    area[i, j] = '—';
            PrintArea();

            Thread tWorker1 = new Thread(new ThreadStart(Worker1));
            Thread tWorker2 = new Thread(new ThreadStart(Worker2));
            tWorker1.Start();
            tWorker2.Start();
            Console.ReadKey();

        }


        static bool Work(int x, int y, bool firstWorker)
        {
            lock (locker)
            {
                if (area[y, x] == '—')
                {
                    area[y, x] = firstWorker ? '#' : '@';
                    PrintArea();
                    return true;
                }
                else return false;
            }
        }

        static void Worker1()
        {
            int myX = 0;
            int myY = 0;
            while (true)
            {
                if (myY >= Size)
                {
                    Console.WriteLine("1:Насяльника, я сделял");
                    break;//end work
                }
                if (myX == Size) { myX = 0; myY++; }
                bool free = Work(myX, myY, true);
                myX++;
                if (!free) { myX = 0; myY++; }
                Thread.Sleep(170);
            }
        }

        static void Worker2()
        {
            int myX = Size - 1;
            int myY = Size - 1;
            while (true)
            {
                if (myX < 0)
                {
                    Console.WriteLine("2:Насяльника, я сделял");
                    break;//end work
                }
                if (myY == -1) { myY = Size - 1; myX--; }
                bool free = Work(myX, myY, false);
                myY--;
                if (!free) { myY = Size - 1; myX--; }
                Thread.Sleep(170);
            }
        }

        static void PrintArea()
        {
            Console.Clear();
            Console.WriteLine();
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    Console.Write(area[i, j]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
