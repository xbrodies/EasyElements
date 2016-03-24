using System;
using System.Collections.Generic;
using System.Data;

namespace PWEasyEditor.Elements
{
    public class ElementsData
    {
        public short Version { get; set; }
        internal short Segmentation { get; set; }
        public DataSet Data { get; protected set; }
        internal Dictionary<ElementsList, List<byte[]>> SkipValues { get; set; }

        internal ElementsData(short version, short segmentation, DataSet data, Dictionary<ElementsList, List<byte[]>> skipValues)
        {
            SkipValues = skipValues;
            Data = data;
            Version = version;
            Segmentation = segmentation;
        }
    }
}