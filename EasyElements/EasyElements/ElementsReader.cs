using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using EasyElements.Configs;

namespace EasyElements
{
    public class ElementsReader : IElementsReader
    {
        public string PathElements { get; }
        public ElementsData ElementsData { get; private set; }

        public Config Config { get; }
        private short version;
        private ElementsList[] _readLists;


        public ElementsReader(string pathElements, string pathToConfigs)
        {
            if (string.IsNullOrEmpty(pathToConfigs))
                throw new ArgumentException("Argument is null or empty", nameof(pathToConfigs));

            this.PathElements = pathElements;
            this.Config = new ConfigReader(pathToConfigs).Open();
        }

        public ElementsReader(string pathElements, Config config)
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config));

            this.PathElements = pathElements;
            this.Config = config;
        }

        public ElementsData Open(IEnumerable<ElementsList> lists)
        {
            _readLists = lists as ElementsList[] ?? lists.ToArray();
            return Open();
        }

        public ElementsData Open()
        {
            if (string.IsNullOrEmpty(PathElements))
                throw new ArgumentException("Argument is null or empty", nameof(PathElements));

            if (!File.Exists(PathElements))
                throw new FileNotFoundException(PathElements);

            using (var br = new BinaryReader(File.OpenRead(PathElements)))
                Read(br);

            return ElementsData;
        }

        private void Read(BinaryReader br)
        {
            version = br.ReadInt16();
            var segmentation = br.ReadInt16();
            var dataSet = new DataSet();
            var skipValues = new Dictionary<ElementsList, List<byte[]>>();
            var CurrentConfig = Config.Downgrade(version);

            foreach (var list in CurrentConfig.Lists)
            {
                if(_readLists!=null)
                  if(_readLists.All(elementsList => dataSet.Tables.Contains(elementsList.Name)))
                     break;

                if (list.Skip != "0")
                    skipValues.Add(list, ReadSkip(br, list));

                var data = ReadList(br, list);

                if (_readLists != null)
                {
                    if (_readLists.All(x => x.Name == list.Name))
                        dataSet.Tables.Add(data);
                } else dataSet.Tables.Add(data);

            }

            ElementsData = new ElementsData(version, segmentation, dataSet, skipValues, CurrentConfig);
        }

        private DataTable ReadList(BinaryReader br, ElementsList list)
        {
            var table = new DataTable(list.Name);

            foreach (var type in list.Types)
            {
                var column = new DataColumn(type.Name, type.GetNormalType());

                column.DefaultValue = column.DataType == typeof (string) ? (object) "" : 0;
                column.AllowDBNull = false;
                table.Columns.Add(column);
            }

            if (!list.Types.Any())
                return table;

            var length = br.ReadInt32();

            for (var i = 0; i < length; i++)
                table.Rows.Add(ReadItem(br, table, list.Types));

            return table;
        }

        private DataRow ReadItem(BinaryReader br, DataTable table, List<ElementsType> types)
        {
            var row = table.NewRow();
            var j = 0;

            foreach (var type in types)
            {
                switch (type.Type)
                {
                    case "int": row[j] = br.ReadInt32(); break;
                    case "float": row[j] = br.ReadSingle(); break;
                    case "string": row[j] = Encoding.GetEncoding(type.Encoding).GetString(br.ReadBytes(int.Parse(type.SizeString))).Replace(@"\0", string.Empty); break;
                    default: throw new ArgumentOutOfRangeException();
                }

                j++;
            }
            return row;
        }

        private List<byte[]> ReadSkip(BinaryReader br, ElementsList list)
        {
            var vals = new List<byte[]>();
            switch (list.Skip)
            {
                case "AUTO":
                    vals.Add(br.ReadBytes(4));
                    var count = br.ReadInt32();
                    vals.Add(BitConverter.GetBytes(count));
                    vals.Add(br.ReadBytes(count));
                    count = br.ReadInt32();
                    while (count <= 0 || count > 10000)
                    {
                        vals.Add(BitConverter.GetBytes(count));
                        count = br.ReadInt32();
                    }
                    br.BaseStream.Position -= 4;
                    break;
                case "RAW":
                    var rCount = br.ReadInt32();
                    vals.Add(BitConverter.GetBytes(rCount));
                    for (var i = 0; i < rCount; i++)
                    {
                        vals.Add(br.ReadBytes(132));
                        var count2 = br.ReadInt32();
                        vals.Add(BitConverter.GetBytes(count2));
                        for (var i2 = 0; i2 < count2; i2++)
                        {
                            vals.Add(br.ReadBytes(8));
                            var l = br.ReadInt32();
                            vals.Add(BitConverter.GetBytes(l));
                            vals.Add(br.ReadBytes(l * 2));
                            var count3 = br.ReadInt32();
                            vals.Add(BitConverter.GetBytes(count3));
                            vals.Add(br.ReadBytes(count3 * 136));
                        }
                    }
                    break;
                default:
                    vals.Add(br.ReadBytes(int.Parse(list.Skip)));
                    break;
            }
            return vals;
        }
    }
}