namespace FileGenerator
{
    public interface IFileService
    {
        string[] Words { get; }

        void Dispose();
        void WriteLineToOutputFile(string line);
    }
}