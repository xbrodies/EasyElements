using EasyElements.Configs;

namespace EasyElements
{
    public interface IElementsReader
    {
        string PathElements { get; }
        ElementsData ElementsData { get; }
        bool IsCompleted { get; }

        Config Config { get; }

        ElementsData Open();
    }
}
