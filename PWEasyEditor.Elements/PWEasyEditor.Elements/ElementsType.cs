using System;
using System.Xml.Serialization;

namespace PWEasyEditor.ElementsAPI
{
    public class ElementsType
    {
        [XmlAttribute]
        public string Name { get; set; }

        [XmlAttribute]
        public string Type { get; set; }

        [XmlAttribute]
        public string Encoding { get; set; }

        [XmlAttribute]
        public string SizeString { get; set; }

        [XmlAttribute]
        public int Version { get; set; }

        [XmlElement("Rel")]
        public ElementsRelation[] Relations { get; set; }

        public Type GetNormalType()
        {
            switch (Type)
            {
                case "int": return typeof(int);
                case "float": return typeof(float);
                case "string": return typeof(string);
                default: throw new ArgumentOutOfRangeException();
            }
        }
    }
}