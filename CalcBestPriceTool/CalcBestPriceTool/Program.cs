using System;
using System.Collections.Generic;

namespace CalcBestPriceTool
{
    class Program
    {
        private static readonly Course[] _courses =
        {
            new Course(1.0, 3),
            new Course(9.0, 2),
            new Course(19.9),
            new Course(55.0),
            new Course(68.0),
            new Course(79.0),
            new Course(99.0, 3),
            new Course(129.0),
            new Course(199.0),
            new Course(299.0),
        };

        static void Main(string[] args)
        {
            int totalPrice = 33;
            totalPrice++;
            var states = new int[PricesCount, totalPrice];

            InitStates(totalPrice, states);

            // 第一行特殊处理
            states[0, 0] = 1;
            if (totalPrice > _courses[0].Price)
            {
                states[0, (int)_courses[0].Price] = 1;
            }

            for (var i = 1; i < PricesCount; i++)
            {
                // 不买该课程
                for (int j = 0; j < totalPrice; j++)
                {
                    if (states[i - 1, j] == 1)
                    {
                        states[i, j] = 1;
                    }
                }

                // 购买该课程
                for (var j = 0; j < totalPrice; j++)
                {
                    var curPrice = (int)(j + _courses[i].Price);
                    if (states[i - 1, j] == 1 && totalPrice > j + _courses[i].Price)
                    {
                        states[i, curPrice] = 1;
                    }
                }
            }

            //Print(states, PricesCount, totalPrice);
            for (int i = totalPrice - 1; i > 0; i--)
            {
                if (states[PricesCount - 1, i] == 1)
                {
                    Console.WriteLine($"Max price: {i}");
                    break;
                }
            }

            int k;
            for (k = totalPrice - 1; k > 0; k--)
            {
                if (states[PricesCount - 1, k] == 1)
                {
                    break;
                }
            }

            if (k <= 0)
            {
                return;
            }

            for (int i = PricesCount - 1; i > 0; i--)
            {
                try
                {
                    if (k - _courses[i].Price >= 0 && states[i - 1, (int)(k - _courses[i].Price)] == 1)
                    {
                        k -= (int)_courses[i].Price;
                        Console.WriteLine($"Buy this price goods: {_courses[i]}");
                    }

                    if (k - _courses[i].Price >= 0 && states[i - 1, k] == 1)
                    {
                        Console.WriteLine($"No buy this price goods: {_courses[i]}");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error: {(int)(k - _courses[i].Price)}");
                }
            }

            if (k != 0)
            {
                Console.WriteLine($"Buy this price goods: {_courses[0]}");
            }
        }

        private static void InitStates(int totalPrice, int[,] states)
        {
            for (int i = 0; i < PricesCount; i++)
            {
                for (int j = 0; j < totalPrice; j++)
                {
                    states[i, j] = -1;
                }
            }
        }

        private static int PricesCount => _courses.Length;

        static void Print(int[,] val, int row, int col)
        {
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    Console.Write($"[{i},{j}]={val[i, j]}\t");
                    if (j != 0 && j % 10 == 0)
                    {
                        Console.WriteLine();
                    }
                }

                Console.WriteLine();
                Console.WriteLine();
            }
        }
    }

    class Course
    {

        public Course(double price, int count = 1)
        {
            Price = price;
            Count = count;
        }

        public double Price { get; }

        public int Count { get; }

        public override string ToString()
        {
            return $"Price: {Price}";
        }
    }
}
