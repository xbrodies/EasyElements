using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace PWEasyEditor.Elements.Configs
{
    public class ConfigWriter : IConfigWriter
    {
        public string Path { get; }
        public Config Config { get; }

        public ConfigWriter(string Path, Config Config)
        {
            this.Path = Path;
            this.Config = Config;
        }

        public void Save() => Save(Path, Config);
        public void Save(string Path) => Save(Path, Config);
        public void Save(Config Config) => Save(Path, Config);

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