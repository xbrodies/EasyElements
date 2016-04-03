using System.Security.Cryptography.X509Certificates;
using EasyElements.Configs;

namespace EasyElements
{
    public interface IElementsReader
    {
        string PathElements { get; }
        ElementsData ElementsData { get; }

        Config Config { get; }

        ElementsData Open();
    }
}
