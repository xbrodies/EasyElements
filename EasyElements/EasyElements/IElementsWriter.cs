using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using EasyElements.Configs;

namespace EasyElements
{
    public interface IElementsWriter
    {
        string Path { get; set; }
        ElementsData Elements { get; set; }
        Config Config { get; set; }
        
        void Save();
    }
}
