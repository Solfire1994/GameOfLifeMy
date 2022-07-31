using System;

namespace GameOfLifeMy
{
    internal class Program
    {
        
        static void Main(string[] args)
        {
            // Создание поля

            Console.WriteLine("Введите размеры поля, сразу количество строк, затем столбцов");
            int row = int.Parse(Console.ReadLine());
            int col = int.Parse(Console.ReadLine());
            bool[,] cells = new bool[row, col];
            int ticks = 10;

            // Заполнение жизнью, ограничение на 30% живых клеток на поле на старте.

            int count = 0;
            for (int i = 0; i < row && count < row * col * 0.3; i++)
            {
                for (int j = 0; j < col && count < row * col * 0.3; j++)
                {
                    Random r = new Random();
                    if (r.Next(0, 3) != 0)
                    {
                        cells[i, j] = true;
                        count++;
                    }
                }
            }

            // Цикл на определенное количество тиков

            for (int h = 0; h <= ticks; h++)
            {
                Console.Clear();
                // Рисование поля
                string str = new string('_', col);
                Console.Write(" " + str);
                Console.WriteLine();
                for (int i = 0; i < row; i++)
                {
                    Console.Write("|");
                    for (int j = 0; j < col; j++)
                    {
                        if (cells[i, j]) Console.Write("@"); else Console.Write(".");
                    }
                    Console.Write("|");
                    Console.WriteLine();
                }
                Console.Write("|" + str + "|");

                // Подсчет соседей

                int[,] neighborsMatrix = new int[row, col];
                for (int i = 0; i < row; i++)
                {
                    for (int j = 0; j < col; j++)
                    {

                        neighborsMatrix[i, j] = GetNeighbors(i, j, row, col, cells);
                    }
                }

                // Рост коллонии

                for (int i = 0; i < row; i++)
                {
                    for (int j = 0; j < col; j++)
                    {
                        if (cells[i, j])
                        {
                            if (neighborsMatrix[i, j] < 2)
                            {
                                cells[i, j] = false;
                            }

                            if (neighborsMatrix[i, j] > 3)
                            {
                                cells[i, j] = false;
                            }
                        }
                        else
                        {
                            if (neighborsMatrix[i, j] >= 3)
                            {
                                cells[i, j] = true;
                            }
                        }
                    }
                }
                System.Threading.Thread.Sleep(100);
            }

            Console.Write("\nНаша игра началась с 30% жизни в нашем объекте, а теперь");
            System.Threading.Thread.Sleep(500);
            Console.Write(".");
            System.Threading.Thread.Sleep(500);
            Console.Write(".");
            System.Threading.Thread.Sleep(500);
            Console.Write(".");
            int endCount = 0;
            for (int i = 0; i < row; i++)
            {
                for(int j = 0; j < col; j++)
                {
                    if(cells[i,j]) endCount++;
                }
            }
            Console.Write($"\nА теперь жизнь заполняет наш мир на {100*endCount/(row*col)} %");
            System.Threading.Thread.Sleep(3000);
            if ((100 * endCount / (row * col)) <= 30)
            {
                Console.Clear();
                System.Threading.Thread.Sleep(1000);
                Console.Write("ВЫ ПРОИГРАЛИ!!!");
            }
            else
            {
                Console.Clear();
                System.Threading.Thread.Sleep(1000);
                Console.Write("ВЫ ПОБЕДИЛИ!!!");
            }
        }

        static int GetNeighbors(int x, int y, int row, int col, bool[,] cells) 
        {
            int numOfAliveNeighbors = 0;
            for (int i = x - 1; i < x + 2; i++)
            {
                for (int j = y - 1; j < y + 2; j++)
                {
                    if (!((i < 0 || j < 0) || (i >= row || j >= col)))
                    {
                        if (cells[i, j] == true) numOfAliveNeighbors++;
                    }
                }
            }
            return numOfAliveNeighbors;
        }
    }
}
