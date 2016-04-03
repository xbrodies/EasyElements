using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyGShop
{
    public class GShopData
    {
        public TimeSpan Timestamp { get; set; }
        public Dictionary<string, Dictionary<string, List<GShopItem>>> Data { get; set; } = 
            new Dictionary<string, Dictionary<string, List<GShopItem>>>();
    }
}
