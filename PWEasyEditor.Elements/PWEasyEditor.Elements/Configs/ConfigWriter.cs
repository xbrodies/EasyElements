using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace PWEasyEditor.ElementsAPI.Configs
{
    public class ConfigWriter
    {
        private readonly string path;
        private readonly Config config;

        public ConfigWriter(string Path, Config Config)
        {
            this.path = Path;
            this.config = Config;
        }

        public void Save() => Save(path, config);
        public void Save(string Path) => Save(Path, config);
        public void Save(Config Config) => Save(path, Config);

        public void Save(string Path, Config Config)
        {
            if (String.IsNullOrEmpty(Path))
                throw new ArgumentException("Неверный формат пути к файлу конфигураций", nameof(Path));

            if (Config == null)
                throw new ArgumentNullException(nameof(Config));

            var serializer = new XmlSerializer(Config.GetType());

            using (var fs = new FileStream(Path, FileMode.OpenOrCreate))
                serializer.Serialize(fs, Config);
        }
    }
}