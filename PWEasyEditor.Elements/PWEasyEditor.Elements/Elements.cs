using System;
using System.Collections.Generic;
using System.Data;

namespace PWEasyEditor.ElementsAPI
{
    public class Elements
    {
        internal Elements(short version,short segmentation, DataSet values, Dictionary<ElementsList, List<byte[]>> skipValues)
        {
            SkipValues = skipValues;
            Values = values;
            Version = version;
            Segmentation = segmentation;
        }

        public short Version { get; protected set; }
        public short Segmentation { get; protected set; }
        public DataSet Values { get; protected set; }
        public Dictionary<ElementsList, List<byte[]>> SkipValues { get; set; }
    }
}