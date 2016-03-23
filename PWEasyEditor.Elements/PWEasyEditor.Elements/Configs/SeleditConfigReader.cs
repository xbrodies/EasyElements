using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PWEasyEditor.ElementsAPI.Configs
{
    public class SeleditConfigReader
    {
        public string Path { get; protected set; }
        public Config Config { get; protected set; }
        public int Version { get; set; }
        private int acc_id;
        private int NpcTalkList;

        public SeleditConfigReader(string path, int version)
        {
            Path = path;
            Version = version;
        }

        public Config Open()
        {
            return Open(Path);
        }

        public Config Open(string Path)
        {
            if (string.IsNullOrEmpty(Path))
                throw new ArgumentException(nameof(Path));

            InitRead(Path);

            return Config;
        }

        private void InitRead(string path)
        {
            if (!File.Exists(Path))
                throw new FileNotFoundException(Path);

            Config = new Config();
            ReadFile(File.ReadAllLines(path));
        }

        private void ReadFile(IReadOnlyList<string> Lines)
        {
            var count = int.Parse(Lines[0]);
            NpcTalkList = int.Parse(Lines[1]);

            var line = 2;

            for (int i = 0; i < count; i++)
            {
                while (Lines[line] == "")
                    line++;

                ReadList(Lines[line], Lines[line + 1], Lines[line + 2], Lines[line + 3]);
                line += 4;
                acc_id++;
            }

        }

        private void ReadList(string Name, string Skip, string Values, string Types)
        {
            var list = new ElementsList
            {
                Name = Name,
                Skip = Skip ?? null,
                Version = Version
            };

            var values = Values.Split(';');
            var types = Types.Split(';');

            for (var i = 0; i < values.Length && i < types.Length; i++)
            {
                var type = new ElementsType
                {
                    Name = values[i],
                    Version = Version
                };

                var parseType = types[i].Split(':');

                switch (parseType[0])
                {
                    case "int32": type.Type = "int"; break;
                    case "wstring":
                        type.Type = "string";
                        type.Encoding = "Unicode";
                        break;
                    case "string":
                        type.Type = "string";
                        type.Encoding = "936";
                        break;
                    case "float":
                        type.Type = "float";
                        break;
                }


                if (parseType.Length > 1)
                    type.SizeString = parseType[1];

                list.Types.Add(type);
            }

            Config.Lists.Add(list);
        }

        
    }
}
