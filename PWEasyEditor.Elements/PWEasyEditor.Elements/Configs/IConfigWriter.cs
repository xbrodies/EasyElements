namespace EasyElements.Configs
{
    public interface IConfigWriter
    {
        string Path { get; }
        Config Config { get; }

        void Save();
        void Save(string Path);
        void Save(Config Config);
    }
}
