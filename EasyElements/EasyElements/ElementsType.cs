using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace EasyElements
{
    [Serializable]
    public class ElementsType
    {
        public static Dictionary<string, object> DefaultValues { get; set; } = 
            new Dictionary<string, object>
            {
                {"System.Int32", default(int) },
                {"System.Single", default(float) },
                {"System.String",string.Empty }
            };

        [XmlAttribute]
        public string Name { get; set; }

        [XmlAttribute]
        public string Caption { get; set; }

        [XmlAttribute]
        public string Type { get; set; }

        [XmlAttribute]
        public string Encoding { get; set; }

        [XmlAttribute]
        public string SizeString { get; set; }

        [XmlAttribute]
        public string isToolTip { get; set; }

        [XmlAttribute]
        public int Version { get; set; }

        [XmlElement("Rel")]
        public ElementsRelation[] Relations { get; set; }

        [XmlIgnore]
        public Type NormalType => System.Type.GetType(Type);

        [XmlIgnore]
        public object DefaultValue => DefaultValues[Type];

        public override string ToString() => Caption;

    }


}