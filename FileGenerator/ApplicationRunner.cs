using FileGenerator.LineGeneration;
using Microsoft.Extensions.Logging;

namespace FileGenerator
{
    public class ApplicationRunner(ILogger<ApplicationRunner> logger, IAppSettings appSettings, IStringGenerator stringGenerator, IFileService fileService)
    {

        public void Run()
        {
            logger.LogInformation("Application started at: {time}", DateTimeOffset.Now);

            Random random = new Random();
            var currentSize = 0;
            var desiredSizeInBytes = appSettings.FileSizeInMb * 1024 * 1024;

            while (currentSize < desiredSizeInBytes)
            {
                int numberPart = random.Next(0, 1000_000);
                var stringPart = stringGenerator.GetNewStringPart();
                var line = $"{numberPart}.{stringPart}";
                fileService.WriteLineToOutputFile(line);

                currentSize += line.Length;
            }

            logger.LogInformation("Application finished at: {time}", DateTimeOffset.Now);
        }
    }
}