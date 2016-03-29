using System;
using System.Collections.Generic;
using System.Data;
using EasyElements.Configs;

namespace EasyElements
{
    public class ElementsData
    {
        public short Version { get; set; }
        public DataSet Data { get; protected set; }

        internal Dictionary<ElementsList, List<byte[]>> SkipValues { get; set; }
        internal short Segmentation { get; set; }

        internal ElementsData(short version, short segmentation, DataSet data, Dictionary<ElementsList, List<byte[]>> skipValues, List<ElementsList> confListsForThisElements)
        {
            this.SkipValues = skipValues;
            this.Data = data;
            this.Version = version;
            this.Segmentation = segmentation;
        }
    }
}