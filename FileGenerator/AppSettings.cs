using Microsoft.Extensions.Configuration;

namespace FileGenerator
{
    public class AppSettings(IConfiguration configuration) : IAppSettings
    {
        public int FileSizeInMb => configuration.GetValue<int>("FileSizeInMb");

        public int StringRepeatRate => configuration.GetValue<int>("StringRepeatRate");

        public string FileSaveLocation => GetFileDirectory("FileSaveLocation");

        public string FileName => configuration.GetValue<string>("FileName") ?? "unsorted.txt";

        private string GetFileDirectory(string key)
        {
            return (configuration.GetValue<string>(key) ??
                        System.Reflection.Assembly.GetExecutingAssembly().Location).TrimEnd('/').TrimEnd('\\');
        }
    }
}
