using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyElements.Configs;

namespace EasyElements
{
    public class ElementsWriter : IElementsWriter
    {
        public string Path { get; set; }
        public ElementsData Elements { get; set; }
        public Config Config { get; set; }
        private BinaryWriter _binaryWriter;

        public ElementsWriter(string path, ElementsData elements, Config config)
        {
            Path = path;
            Elements = elements;
            Config = config;
        }

        public ElementsWriter(IElementsReader elementsReader)
        {
            Path = elementsReader.PathElements;
            Elements = elementsReader.ElementsData;
            Config = elementsReader.Config;
        }

        public void Save()
        {
            if (String.IsNullOrEmpty(Path)) throw new ArgumentException("Argument is null or empty", nameof(Path));
            if (Elements == null) throw new ArgumentNullException(nameof(Elements));
            if (Config == null) throw new ArgumentNullException(nameof(Config));

            _binaryWriter = new BinaryWriter(File.OpenWrite(Path));

            _binaryWriter.Write(Elements.Version);
            _binaryWriter.Write(Elements.Segmentation);

            foreach (var list in Config.Lists.Where(x => x.Version <= Elements.Version))
                WriteTable(list);

        }

        private void WriteTable(ElementsList list)
        {
            if (Elements.SkipValues.ContainsKey(list))
                foreach (var bytese in Elements.SkipValues[list])
                    _binaryWriter.Write(bytese);

            var values = Elements.Data.Tables[list.Name].Rows;

            if (list.Skip != "RAW")
                _binaryWriter.Write(values.Count);

            foreach (DataRow value in values)
                WriteRow(value, list.Types);
        }

        private void WriteRow(DataRow row, IEnumerable<ElementsType> Types)
        {
            foreach (var type in Types.Where(x => x.Version <= Elements.Version))
                switch (type.Type)
                {
                    case "string":
                        {
                            var str = (string)row[type.Name];
                            var result = new byte[int.Parse(type.SizeString)];

                            Encoding.GetEncoding(type.Encoding).GetBytes(str).CopyTo(result, 0);

                            _binaryWriter.Write(result);

                            break;
                        }
                    case "int":
                        _binaryWriter.Write((int)row[type.Name]);
                        break;

                    case "float":
                        _binaryWriter.Write((float)row[type.Name]);
                        break;
                }
        }
    }
}
