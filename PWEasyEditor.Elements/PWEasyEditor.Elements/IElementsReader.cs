using PWEasyEditor.Elements.Configs;

namespace PWEasyEditor.Elements
{
    public interface IElementsReader
    {
        string PathElements { get; }
        Elements Elements { get; }
        bool IsCompleted { get; }

        int ListCount { get; }
        int ListReading { get; }

        Config Config { get; }

        Elements Open();
    }
}
