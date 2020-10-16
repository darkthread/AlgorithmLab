using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Knapsack
{
    public class Item
    {
        public string Name { get; set; }
        public int Weight { get; set; }
        public int Value { get; set; }

        public Item(string name, int weight, int value)
        {
            Name = name;
            Weight = weight;
            Value = value;
        }

        public override string ToString()
        {
            return $"{Name} (W:{Weight} / V:${Value})";
        }
    }

    public class Problem
    {
        public Item[] Items;

        public int MaxWeight;

        public bool Duplicatable;

        public Problem(int maxWeight, bool allowPural, IEnumerable<Item> items)
        {
            Items = items.ToArray();
            Duplicatable = allowPural;
            MaxWeight = maxWeight;
        }
    }
}
