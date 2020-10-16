using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Knapsack
{
    public class BruteForceCalculator : CalculatorBase
    {
        public BruteForceCalculator(Problem problem) : base(problem) { }
        protected override string AlgorithmName => "暴力破解法";
        public class Knapsack //模擬背包行為
        {
            public List<Item> Items = new List<Item>();
            public int CurrentValue = 0;
            public int AllowedWeight = 0;
            public Knapsack(int maxWeight)
            {
                AllowedWeight = maxWeight;
            }
            public void AddItem(Item item) //放入物品
            {
                Items.Add(item);
                CurrentValue += item.Value;
                AllowedWeight -= item.Weight;
            }
            public void RemoveItem(Item item) //取出物品
            {
                Items.Remove(item);
                CurrentValue -= item.Value;
                AllowedWeight += item.Weight;
            }
        }

        protected override Answer Calculate()
        {
            Knapsack bag = new Knapsack(MaxWeight);
            MaxValueItems = bag.Items.ToArray();
            Explore(bag, Items.ToList());
            return new Answer
            {
                Items = MaxValueItems,
                ExecCount = CallCount
            };
        }

        public Item[] MaxValueItems = null;
        public int MaxValue = 0;
        int CallCount = 0;

        public void Explore(Knapsack knapsack, List<Item> options)
        {
            //篩選小於背包剩餘可負重的物品一一嘗試
            //這裡直接用Where過濾，若要提高效能需再優化
            foreach (var item in options.Where(o => o.Weight <= knapsack.AllowedWeight))
            {
                CallCount++;
                knapsack.AddItem(item); //放入物品
                if (knapsack.CurrentValue > MaxValue) //若超越最高記錄就記錄
                {
                    MaxValueItems = knapsack.Items.ToArray();
                    MaxValue = knapsack.CurrentValue;
                }
                if (!Duplicatable) //若不允許物品重複，已放入背包的項目需移出選項
                    Explore(knapsack, options.Except(new Item[] { item }).ToList());
                else
                    Explore(knapsack, options.ToList());
                knapsack.RemoveItem(item);
            }
        }
    }
}
