using System;
using System.Collections.Generic;

namespace CalcBestPriceTool
{
    class Program
    {
        private static readonly Course[] _courses =
        {
            new Course(1.0),
            new Course(9.0, 2),
            new Course(19.9),
            new Course(55.0,10),
            new Course(68.0,2),
            new Course(79.0,20),
            new Course(99.0, 13),
            new Course(129.0),
            new Course(199.0),
            new Course(299.0),
        };

        static void Main(string[] args)
        {
            int totalPrice = 117;
            totalPrice++;
            var states = new int[PricesCount, totalPrice];

            InitStates(totalPrice, states);

            // 第一行特殊处理
            CalcFirstCourseScheme(states, totalPrice);

            CalcBestPriceScheme(totalPrice, states);

            //Print(states, PricesCount, totalPrice);

            PrintMaxBuyPrice(totalPrice, states);

            PrintBuyScheme(totalPrice, states);
        }

        private static void CalcBestPriceScheme(int totalPrice, int[,] states)
        {
            for (var i = 1; i < PricesCount; i++)
            {
                // 不买该课程
                for (int j = 0; j < totalPrice; j++)
                {
                    if (states[i - 1, j] >= 0)
                    {
                        states[i, j] = 0;
                    }
                }

                // 购买该课程
                for (var j = 0; j < totalPrice; j++)
                {
                    var count = _courses[i].Count;
                    while (count > 0)
                    {
                        var courseTotalPrice = (int)_courses[i].Price * count;
                        var curPrice = j + courseTotalPrice;
                        if (states[i - 1, j] >= 0 && totalPrice > curPrice)
                        {
                            states[i, curPrice] = count;
                        }

                        count--;
                    }
                }
            }
        }

        private static void CalcFirstCourseScheme(int[,] states, int totalPrice)
        {
            states[0, 0] = 0;
            var count = _courses[0].Count;
            while (count > 0)
            {
                var courseTotalPrice = (int)_courses[0].Price * count;
                if (totalPrice > courseTotalPrice)
                {
                    states[0, courseTotalPrice] = count;
                }

                count--;
            }
        }

        private static void PrintBuyScheme(int totalPrice, int[,] states)
        {
            int k;
            for (k = totalPrice - 1; k > 0; k--)
            {
                if (states[PricesCount - 1, k] >= 0)
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
                var count = _courses[i].Count;
                while (count > 0)
                {
                    var courseTotalPrice = (int)_courses[i].Price * count;
                    try
                    {
                        if (k - courseTotalPrice >= 0 && states[i - 1, (int)(k - courseTotalPrice)] >= 0)
                        {
                            k -= courseTotalPrice;
                            Console.WriteLine($"Buy this price goods: {_courses[i]}, count: {count}");
                        }

                        if (k - courseTotalPrice >= 0 && states[i - 1, k] >= 0)
                        {
                            Console.WriteLine($"No buy this price goods: {_courses[i]}");
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Error: {courseTotalPrice}");
                    }

                    count--;
                }
            }

            if (k != 0)
            {
                Console.WriteLine($"Buy this price goods: {_courses[0]}, count: {k / _courses[0].Price}");
            }
        }

        private static void PrintMaxBuyPrice(int totalPrice, int[,] states)
        {
            for (int i = totalPrice - 1; i > 0; i--)
            {
                if (states[PricesCount - 1, i] >= 0)
                {
                    Console.WriteLine($"Max price: {i}");
                    break;
                }
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
