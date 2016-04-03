using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyGShop
{
    public class GShopItem : ICloneable
    {
        public bool Activate;
        public int ShopId { get; set; }
        public int CatIndex { get; set; }
        public int SubCatIndex { get; set; }
        public string SurfacePath { get; set; }
        public int ItemId { get; set; }
        public int ItemAmount { get; set; }
        public List<GShopSell> SaleOptions = new List<GShopSell>();
        public string Description { get; set; }
        public string Name { get; set; }
        public int GiftId { get; set; }
        public int GiftAmount { get; set; }
        public int GiftDuration { get; set; }
        public int LogPrice { get; set; }

        public object Clone()
        {
            var item = MemberwiseClone();
            ((GShopItem)item).SaleOptions = SaleOptions.Select(x => (GShopSell) x.Clone()).ToList();

            return item;

        }
    }
}
