﻿using FileGenerator.LineGeneration;
using Microsoft.Extensions.Logging;

namespace FileGenerator
{
    public class ApplicationRunner(ILogger<ApplicationRunner> logger, IAppSettings appSettings, ILineGenerator lineGenerator, IFileService fileService)
    {

        public void Run()
        {
            logger.LogInformation("Application started at: {time}", DateTimeOffset.Now);

            Random random = new Random();
            var currentSize = 0;
            var desiredSizeInBytes = appSettings.FileSizeInMb * 1024 * 1024;
            lineGenerator.Initialize();

            while (currentSize < desiredSizeInBytes)
            {
                var line = lineGenerator.GenerateLine();
                fileService.WriteLineToOutputFile(line);

                currentSize += line.Length;
            }

            logger.LogInformation("Application finished at: {time}", DateTimeOffset.Now);
            logger.LogInformation("Created file: {file}", fileService.OutputFilePath);
        }
    }
}