using System;
using Newtonsoft.Json;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace FavoritePizzaToppings
{
    class Program
    {
        static void Main(string[] args)
        {
            var pizzas = JsonConvert.DeserializeObject<List<Pizza>>(File.ReadAllText(@"./pizzas.json"));

            var toppingCombinations = pizzas.Select(p => string.Join(",", p.Toppings.OrderBy(t => t)));

            var countOfCombos = new Dictionary<string, int>();

            foreach (var combo in toppingCombinations)
            {
                if (!countOfCombos.ContainsKey(combo))
                    countOfCombos.Add(combo, 1);
                else
                    countOfCombos[combo] += 1;
            }

            var top20 = countOfCombos.OrderByDescending(combo => combo.Value).Take(20);

            foreach (var (combination, count) in top20)
            {
                Console.WriteLine($"The topping combination of {combination} was ordered {count} times");
            }

        }
    }
}
