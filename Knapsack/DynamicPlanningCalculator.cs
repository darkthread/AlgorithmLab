using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Knapsack
{
    public class DynamicPlanningCalculator : CalculatorBase
    {
        public DynamicPlanningCalculator(Problem problem) : base(problem) { }
        protected override string AlgorithmName => "動態規劃法";
        public class Cell
        {
            public List<Item> Items { get; set; }
            public int TotalValue { get; set; }
            public int TotalWeight { get; set; }

            //傳入物品集合建立格式
            public Cell(IEnumerable<Item> items)
            {
                Items = items.ToList();
                TotalValue = items.Sum(o => o.Value);
                TotalWeight = items.Sum(o => o.Weight);
            }
            public Cell() : this(new List<Item>()) { } //空格子
            //其他格子的內容再加上指定物品
            public Cell(Cell cell, Item item) : this(cell.Items.Concat(new Item[] { item })) { }
            //在格子放在指定物品
            public Cell(Item item) : this(new Item[] { item }) { }
        }

        protected Cell[,] Cells = null;

        protected override Answer Calculate()
        {
            var items = Items;
            var rowCount = items.Length;
            var colCount = MaxWeight;
            Cells = new Cell[rowCount, colCount];
            for (var row = 0; row < rowCount; row++)
            {
                var item = items[row]; //每一列處理一種物品
                for (var col = 0; col < colCount; col++)
                {
                    var cellMaxWeight = col + 1; //本欄的最大重量單位
                    //正上方的格子，若為第一列則為空格
                    var upperCell = row > 0 ? Cells[row - 1, col] : new Cell();
                    Cell currCell;
                    //物品超重放不進去，直接取上方格子或空格子
                    if (item.Weight > cellMaxWeight)
                        currCell = upperCell;
                    else //背包放得下
                    {
                        //先放入物品本身，如果有其他更划算的再換掉
                        currCell = new Cell(item);
                        //剩餘重量還可用來裝值錢東西
                        var weightLeft = cellMaxWeight - item.Weight;
                        if (weightLeft > 0) //若背包負重還有剩
                        {
                            
                            if (Duplicatable)
                                //允許物品重複，由同列取剩餘重量最大價值組合加上本物品
                                currCell = new Cell(Cells[row, weightLeft - 1], item);
                            else if (row > 0)
                                //每個物品只能有一個，從上一列取得剩餘負重最大價值組合加上本物品
                                currCell = new Cell(Cells[row - 1, weightLeft - 1], item);
                        }
                        //若搞了半天沒上一列值錢，取上一列
                        if (currCell.TotalValue < upperCell.TotalValue)
                            currCell = upperCell;
                    }
                    Cells[row, col] = currCell;
                }
            }
            return new Answer
            {
                Items = Cells[rowCount - 1, colCount - 1].Items, //最右下角為最高價值
                ExecCount = rowCount * colCount
            };
        }
    }
}
