using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace EasyGShop
{
    public class GShopCategory
    {
        public string Name { get; set; }
        public List<GShopItem> Items { get; set; }

        public GShopCategory(string name)
        {
            Name = name;
            Items = new List<GShopItem>();
        }

        public GShopItem this[int itemIndex]
        {
            get { return Items[itemIndex]; }
            set { Items[itemIndex] = value; }
        }
    }
}
