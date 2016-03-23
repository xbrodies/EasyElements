using System.Xml.Serialization;

namespace PWEasyEditor.ElementsAPI
{
    public class ElementsRelation
    {
        [XmlAttribute]
        public string ListName { get; set; }

        [XmlAttribute]
        public string PropertyName { get; set; }
    }
}