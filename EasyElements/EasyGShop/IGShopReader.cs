using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyGShop
{
    public interface IGShopReader
    {
        string PathToGShop { get; }
        GShopData Open();
    }
}
