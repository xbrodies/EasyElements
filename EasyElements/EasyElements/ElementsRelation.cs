using System;
using System.Xml.Serialization;

namespace EasyElements
{
    [Serializable]
    public class ElementsRelation
    {
        [XmlAttribute]
        public string ListName { get; set; }

        [XmlAttribute]
        public string PropertyName { get; set; }
    }
}