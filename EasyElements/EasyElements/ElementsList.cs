using System.Collections.Generic;
using System.Xml.Serialization;

namespace EasyElements
{
    public class ElementsList
    {
        [XmlAttribute]
        public string Name { get; set; }

        [XmlAttribute]
        public string Caption { get; set; }

        [XmlAttribute]
        public int Version { get; set; }

        [XmlAttribute]
        public string Skip { get; set; }

        [XmlAttribute]
        public string ListType { get; set; }

        [XmlElement("Type")]
        public List<ElementsType> Types { get; set; }


    }
}