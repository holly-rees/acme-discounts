using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AcmeSharedModels;
using acme_discount_engine.Models;
using System.Data;

namespace acme_discount_engine.Models
{
    public class Basket
    {
        private List<Item> items;

        private Money totalAfter2for1 = new(0.0);

        private Money runningTotal = new(0.0);
        public Basket(List<Item> items)
        {
            this.items = items;
            SortItemsAlphabetically();
        }

        public Money GetRunningTotal()
        {
            return runningTotal;
        }

        public Money GetTotalAfter2for1()
        {
            return totalAfter2for1;
        }

        public List<Item> GetItems()
        {
            return items;
        }

        public void UpdateTotalAfter2for1(decimal total)
        {
            totalAfter2for1.Reset();
            totalAfter2for1.AddMoney(total);
        }

        public void UpdateRunningTotal(decimal total)
        {
            runningTotal.Reset();
            runningTotal.AddMoney(total);
        }



        public decimal SumItems()
        {
            return (decimal)items.Sum(item => item.Price);
        }

        public void SortItemsAlphabetically()
        {
            items.Sort((x, y) => x.Name.CompareTo(y.Name));
        }


    }
}