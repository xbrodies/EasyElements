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
            if (Elements == null) throw new ArgumentNullException(nameof(Elements));
            if (Config == null) throw new ArgumentNullException(nameof(Config));

            using (var _binaryWriter = new BinaryWriter(File.OpenWrite(Path)))
            {
                _binaryWriter.Write(Elements.Version);
                _binaryWriter.Write(Elements.Segmentation);

                foreach (var list in Config.Lists.Where(x => x.Version <= Elements.Version))
                    WriteTable(list, _binaryWriter);
            }
        }

        private void WriteTable(ElementsList list, BinaryWriter _binaryWriter)
        {
            if (Elements.SkipValues.ContainsKey(list))
                foreach (var bytese in Elements.SkipValues[list])
                    _binaryWriter.Write(bytese);

            var values = Elements.Data.Tables[list.Name].Rows;

            if (list.Skip != "RAW")
                _binaryWriter.Write(values.Count);

            foreach (DataRow value in values)
                WriteRow(value, list.Types, _binaryWriter);
        }

        private void WriteRow(DataRow row, IEnumerable<ElementsType> Types, BinaryWriter _binaryWriter)
        {
            foreach (var type in Types.Where(x => x.Version <= Elements.Version))
                switch (type.Type)
                {
                    case "System.String":
                        {
                            var str = (string)row[type.Name];
                            var result = new byte[int.Parse(type.SizeString)];

                            Encoding.GetEncoding(type.Encoding).GetBytes(str).CopyTo(result, 0);

                            _binaryWriter.Write(result);

                            break;
                        }
                    case "System.Int32":
                        _binaryWriter.Write((int)row[type.Name]);
                        break;

                    case "System.Single":
                        _binaryWriter.Write((float)row[type.Name]);
                        break;
                }
        }
    }
}
