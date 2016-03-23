using System;
using System.IO;
using System.Xml.Serialization;
using PWEasyEditor.Elements.Configs;

namespace PWEasyEditor.Elements.Configs
{
    public class ConfigReader : IConfigReader
    {
        public string ConfigPath { get; private set; }
        public Config Config { get; private set; }

        public ConfigReader(string configPath)
        {
            this.ConfigPath = configPath;
        }

        public Config Open()
        {
            return Open(ConfigPath);
        }

        public Config Open(string Path)
        {
            if (String.IsNullOrEmpty(Path))
                throw new ArgumentException("Неверный формат пути к файлу конфигураций", nameof(Path));

            if (!File.Exists(Path))
                throw new FileNotFoundException("Файл конфигураций не найден", Path);

           return Read(Path);
        }

        private Config Read(string Path)
        {
            Config = new Config();

            var serializer = new XmlSerializer(Config.GetType());

            using (var fs = new FileStream(Path, FileMode.OpenOrCreate))
                Config = (Config) serializer.Deserialize(fs);

            return Config;
        }
    }
}

