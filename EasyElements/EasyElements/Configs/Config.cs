using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Xml.Serialization;

namespace EasyElements.Configs
{
    public class Config
    {
        [XmlElement("List")]
        public List<ElementsList> Lists { get; set; } = new List<ElementsList>();

        [XmlAttribute]
        public int ConfigVersion { get; set; }

        public ElementsList GetElementsListByDataTable(DataTable table)
        {
            return Lists.First(x => x.Name == table.TableName);
        }
    }
}