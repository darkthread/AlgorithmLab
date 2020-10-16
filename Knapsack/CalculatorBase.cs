using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Knapsack
{
    /// <summary>
    /// 背包問題計算機基底類別
    /// </summary>
    public abstract class CalculatorBase
    {

        public int MaxWeight; //背包最大負重
        public Item[] Items; //可放入物品清單
        public bool Duplicatable; //物品是否可重複

        protected abstract string AlgorithmName { get; } //演算法名稱

        protected class Answer //計算結果
        {
            public IEnumerable<Item> Items; //背包內物品清單
            public int MaxValue => Items.Sum(o => o.Value); //最高價值
            public int ExecCount; //運算次數
        }

        public CalculatorBase(Problem problem)
        {
            MaxWeight = problem.MaxWeight;
            Items = problem.Items;
            Duplicatable = problem.Duplicatable;
        }

        protected abstract Answer Calculate(); //抽象方法，實作演算邏輯

        public void Run(bool warmup = false)
        {
            var sw = new Stopwatch();
            sw.Start();
            var answer = Calculate();
            sw.Stop();
            Console.ForegroundColor = warmup ? ConsoleColor.DarkGray : ConsoleColor.Yellow;
            Console.WriteLine($"====== {this.AlgorithmName} ======");
            if (!warmup) Console.ResetColor();
            Console.WriteLine($"最高價值：{answer.MaxValue}");
            Console.WriteLine($"物品清單：{string.Join(" / ", answer.Items.Select(o => o.ToString()).ToArray())}");
            var dura = sw.ElapsedMilliseconds > 1 ?
                $"{sw.ElapsedMilliseconds:n0} ms" : $"{sw.ElapsedTicks:n0} ticks";
            Console.WriteLine($"執行時間：{dura} ( {answer.ExecCount:n0} 次 )");
        }
    }
}
