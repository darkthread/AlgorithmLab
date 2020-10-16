using System;
using System.Collections.Generic;
using System.Linq;

namespace Knapsack
{
    class Program
    {
        static void Main(string[] args)
        { 
            Test1();
            Test2();
            Test3();
            Test4();
            Console.ReadLine();
        }

        static void Test(Problem problem)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(
                $"【問題】重量上限 = {problem.MaxWeight} / 物品種類 = {problem.Items.Length} / 允許物品重複 = {(problem.Duplicatable ? "是" : "否")}");
            Console.ResetColor();
            for (var i = 0; i < 3; i++)
            {
                var bfc = new BruteForceCalculator(problem);
                bfc.Run(i == 0); //i==0 為暖身
                var dpc = new DynamicPlanningCalculator(problem);
                dpc.Run(i == 0); //i==0 為暖身
            }
            Console.WriteLine();
        }

        static void Test1()
        {
            var problem = new Problem(4, false, new Item[]
            {
                new Item("吉他", 1, 1500),
                new Item("音響", 4, 3000),
                new Item("筆電", 3, 2000)
            });
            Test(problem);
        }

        static void Test2()
        {
            //REF: https://openhome.cc/Gossip/AlgorithmGossip/KnapsackProblem.htm
            var problem = new Problem(8, true, new Item[]
            {
                new Item("李子", 4, 4500),
                new Item("蘋果", 5, 5700),
                new Item("橘子", 2, 2250),
                new Item("草莓", 1, 1100),
                new Item("甜瓜", 6, 6700)
            });
            Test(problem);
        }

        static void Test3()
        {
            //REF: https://magiclen.org/dynamic-programming-0-1-knapsack-problem/
            var wu = 50;
            var problem = new Problem(1000 / wu, false, new Item[] {
                new Item("石英錶", 200/wu, 1200),
                new Item("手機", 500/wu, 1500),
                new Item("畫", 300/wu, 1500),
                new Item("平板電腦", 850/wu, 4000),
            });
            Test(problem);
        }

        static void Test4()
        {
            var rnd = new Random();
            var randomItems = Enumerable.Range(1, 20)
                .Select(o => new Item("物品", rnd.Next(100) + 1, rnd.Next(400) * 10 + 10))
                .ToArray();
            var problem = new Problem(randomItems.Sum(o => o.Weight) / 4, false, randomItems);
            Test(problem);
        }


    }
}
