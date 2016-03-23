using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PWEasyEditor.Elements.Configs
{
    public interface IConfigReader
    {
        string ConfigPath { get; }
        Config Config { get; }

        Config Open();
        Config Open(string Path);
    }
}
