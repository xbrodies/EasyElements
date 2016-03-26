using System.Collections.Generic;
using System.Xml.Serialization;

namespace EasyElements.Configs
{
    public class Config
    {
        [XmlElement("List")]
        public List<ElementsList> Lists { get; set; } = new List<ElementsList>();

        [XmlAttribute]
        public int ConfigVersion { get; set; }
    }
}