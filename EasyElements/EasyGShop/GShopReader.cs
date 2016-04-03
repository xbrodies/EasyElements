using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyGShop
{
    public class GShopReader : IGShopReader
    {
        public string PathToGShop { get; set; }
        public GShopData GShop { get; private set; }

        public GShopReader(string pathToGShop)
        {
            if (String.IsNullOrEmpty(pathToGShop))
                throw new ArgumentException("Argument is null or empty", nameof(pathToGShop));

            PathToGShop = pathToGShop;
        }

        public GShopData Open()
        {
            var stopwatch = Stopwatch.StartNew();

            GShop = new GShopData();
            using (var binaryReader = new BinaryReader(File.OpenRead(PathToGShop)))
            {
                GShop.Timestamp = TimeSpan.FromSeconds(binaryReader.ReadInt32());
                var gShopItem = ReadItems(binaryReader, binaryReader.ReadInt32()).ToArray();

                var mCat = new Dictionary<string, Dictionary<string, List<GShopItem>>>();
                for (int i = 0; i < 8; i++)
                {
                    var sCat = new Dictionary<string, List<GShopItem>>();
                    string name = Encoding.Unicode.GetString(binaryReader.ReadBytes(128)).Replace("\0", string.Empty);
                    int count = binaryReader.ReadInt32();

                    for (int j = 0; j < count; j++)
                        sCat.Add(Encoding.Unicode.GetString(binaryReader.ReadBytes(128)).Replace("\0", string.Empty), 
                            gShopItem.Where(x=>x.CatIndex==i && x.SubCatIndex==j).ToList());

                    mCat.Add(name, sCat);
                }

                GShop.Data = mCat;

                stopwatch.Stop();
                Debug.Print($"Open the GShop.data in {stopwatch.Elapsed} second");
            }

            return GShop;
        }

        private IEnumerable<GShopItem> ReadItems(BinaryReader binaryReader, int count)
        {
            for (int i = 0; i < count; i++)
                yield return ReadItem(binaryReader);
        } 

        private GShopItem ReadItem(BinaryReader binaryReader)
        {
            var result = new GShopItem
            {
                ShopId = binaryReader.ReadInt32(),
                CatIndex = binaryReader.ReadInt32(),
                SubCatIndex = binaryReader.ReadInt32(),
                SurfacePath = Encoding.GetEncoding(936).GetString(binaryReader.ReadBytes(128)).Replace("\0", string.Empty),
                ItemId = binaryReader.ReadInt32(),
                ItemAmount = binaryReader.ReadInt32(),
                SaleOptions = ReadSales(binaryReader).ToList(),
                Description = Encoding.Unicode.GetString(binaryReader.ReadBytes(1024)).Replace("\0", string.Empty),
                Name = Encoding.Unicode.GetString(binaryReader.ReadBytes(64)).Replace("\0", string.Empty),
                GiftId = binaryReader.ReadInt32(),
                GiftAmount = binaryReader.ReadInt32(),
                GiftDuration = binaryReader.ReadInt32(),
                LogPrice = binaryReader.ReadInt32()
            };

            return result;
        }

        private IEnumerable<GShopSell> ReadSales(BinaryReader binaryReader)
        {
            for (var i = 0; i < 4; i++)
                yield return ReadSell(binaryReader);
        }

        private GShopSell ReadSell(BinaryReader binaryReader)
        {
            var gShopSell = new GShopSell
            {
                price = binaryReader.ReadInt32(),
                expire_date = binaryReader.ReadInt32(),
                duration = binaryReader.ReadInt32(),
                start_date = binaryReader.ReadInt32(),
                control_type = binaryReader.ReadInt32(),
                day = binaryReader.ReadInt32(),
                status = binaryReader.ReadInt32(),
                flags = binaryReader.ReadInt32(),
            };

            return gShopSell;
        }
    }
}
