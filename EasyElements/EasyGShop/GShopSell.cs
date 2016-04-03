using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyGShop
{
    public class GShopSell : ICloneable
    {
        public int price { get; set; }
        public int expire_date { get; set; }
        public int duration { get; set; }
        public int start_date { get; set; }
        public int control_type { get; set; }
        public int day { get; set; }
        public int status { get; set; }
        public int flags { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
