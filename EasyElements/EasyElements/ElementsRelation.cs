using System.Xml.Serialization;

namespace EasyElements
{
    public class ElementsRelation
    {
        [XmlAttribute]
        public string ListName { get; set; }

        [XmlAttribute]
        public string PropertyName { get; set; }
    }
}