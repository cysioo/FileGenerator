using Microsoft.Extensions.Configuration;

namespace FileGenerator
{
    public class AppSettings(IConfiguration configuration) : IAppSettings
    {
        public int FileSizeInMb => configuration.GetValue<int>("FileSizeInMb");

        public int? RepeatRate => configuration.GetValue<int?>("RepeatRate");

        public string FileSaveLocation => GetFileDirectory("FileSaveLocation");

        public string FileName => configuration.GetValue<string>("FileName") ?? "generated.txt";

        public string? LineTemplate => configuration.GetValue<string>("LineTemplate");

        private string GetFileDirectory(string key)
        {
            return (configuration.GetValue<string>(key) ??
                        System.Reflection.Assembly.GetExecutingAssembly().Location).TrimEnd('/').TrimEnd('\\');
        }
    }
}
