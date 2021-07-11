using System;

namespace CalcBestPriceTool
{
    class Program
    {
        private static double[] _prices = { 1.0, 9.0, 19.9, 55.0, 68.0, 79.0, 99.0, 129.0, 199.0, 299.0 };

        static void Main(string[] args)
        {
            int totalPrice = 33;
            totalPrice++;
            var states = new int[_prices.Length, totalPrice];

            // 第一行特殊处理
            states[0, 0] = 1;
            if (totalPrice > _prices[0])
            {
                states[0, (int)_prices[0]] = 1;
            }

            for (var i = 1; i < _prices.Length; i++)
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
                    var curPrice = (int)(j + _prices[i]);
                    if (states[i - 1, j] == 1 && totalPrice > j + _prices[i])
                    {
                        states[i, curPrice] = 1;
                    }
                }
            }

            //Print(states, _prices.Length, totalPrice);
            for (int i = totalPrice - 1; i > 0; i--)
            {
                if (states[_prices.Length - 1, i] == 1)
                {
                    Console.WriteLine($"Max price: {i}");
                    break;
                }
            }

            int k;
            for (k = totalPrice - 1; k > 0; k--)
            {
                if (states[_prices.Length - 1, k] == 1)
                {
                    break;
                }
            }

            if (k <= 0)
            {
                return;
            }

            for (int i = _prices.Length - 1; i > 0; i--)
            {
                try
                {
                    if (k - _prices[i] >= 0 && states[i - 1, (int)(k - _prices[i])] == 1)
                    {
                        k -= (int)_prices[i];
                        Console.WriteLine($"Buy this price goods: {_prices[i]}");
                    }

                    if (k - _prices[i] >= 0 && states[i - 1, k] == 1)
                    {
                        Console.WriteLine($"No buy this price goods: {_prices[i]}");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error: {(int)(k - _prices[i])}");
                }
            }

            if (k != 0)
            {
                Console.WriteLine($"Buy this price goods: {_prices[0]}");
            }
        }

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
}
