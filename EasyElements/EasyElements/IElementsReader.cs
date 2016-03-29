using EasyElements.Configs;

namespace EasyElements
{
    public interface IElementsReader
    {
        string PathElements { get; }
        ElementsData ElementsData { get; }

        ElementsData Open();
    }
}
