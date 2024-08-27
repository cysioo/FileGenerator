namespace FileGenerator
{
    public interface IAppSettings
    {
        string FileName { get; }
        string FileSaveLocation { get; }
        int FileSizeInMb { get; }
        int? RepeatRate { get; }
        string? LineTemplate { get; }
    }
}