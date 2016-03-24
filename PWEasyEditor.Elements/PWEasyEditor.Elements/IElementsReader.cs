using PWEasyEditor.Elements.Configs;

namespace PWEasyEditor.Elements
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
