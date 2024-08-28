namespace FileGenerator
{
    public interface IFileService
    {
        string[] Words { get; }
        string OutputFilePath { get; }

        void Dispose();
        void WriteLineToOutputFile(string line);
    }
}