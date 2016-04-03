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

        /// <summary>
        /// Возращает файл конфигурации с листами и типами до указанной версии elements.data. Но не изменяет текущий файл конфигураций.
        /// </summary>
        /// <param name="version">Версия elements.data</param>
        /// <returns>Файл конфигурации указанной версии</returns>
        public Config Downgrade(short version)
        {
            var lists = Lists.Where(x => x.Version <= version).ToList();
            foreach (var elementsList in lists)
                elementsList.Types = elementsList.Types.Where(x => x.Version <= version).ToList();

            return new Config { ConfigVersion = version, Lists = lists };
        }
    }
}