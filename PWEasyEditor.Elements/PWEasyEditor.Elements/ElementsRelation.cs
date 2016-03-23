using System.Xml.Serialization;

namespace PWEasyEditor.Elements
{
    public class ElementsRelation
    {
        [XmlAttribute]
        public string ListName { get; set; }

        [XmlAttribute]
        public string PropertyName { get; set; }
    }
}