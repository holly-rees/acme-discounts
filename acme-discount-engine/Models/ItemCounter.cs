using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AcmeSharedModels;

namespace acme_discount_engine.Models
{
    public class ItemCounter
    {
        private string currentItem;
        private int count;

        public ItemCounter()
        {
            this.currentItem = "";
            this.count = 0;
        }

        public void ResetCounter()
        {
            count = 0;
        }

        public bool isFirstOfNewItem(Item item)
        {
            return item.Name != currentItem;
        }

        public void initialiseNewItemCount(Item item)
        {
            currentItem = item.Name;
            count = 1;
        }

        public void increaseCount()
        {
            count += 1;
        }

        public int getCount()
        {
            return count;
        }
    }
}