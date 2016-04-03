using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using EasyElements.Configs;

namespace EasyElements
{
    public class ElementsData
    {
        public short Version { get; set; }
        public DataSet Data { get; protected set; }
        public Config ConfigForThisElements { get; private set; }

        internal Dictionary<ElementsList, List<byte[]>> SkipValues { get; set; }
        internal short Segmentation { get; set; }

        internal ElementsData(short version, short segmentation, DataSet data, Dictionary<ElementsList, List<byte[]>> skipValues, Config configForThisElements)
        {
            this.SkipValues = skipValues;
            this.ConfigForThisElements = configForThisElements;
            this.Data = data;
            this.Version = version;
            this.Segmentation = segmentation;
        }

     //   public DataRow FindByID

        public DataRow FindByID(int ID, List<ElementsList> lists)
        {
            return lists.SelectMany(elementsList => Data.Tables[elementsList.Name].Rows.Cast<DataRow>())
                .FirstOrDefault(dataRow => dataRow["ID"].Equals(ID));
        }
    }
}