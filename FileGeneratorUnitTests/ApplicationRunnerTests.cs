using FileGenerator;
using Microsoft.Extensions.Logging;
using Moq;

namespace FileGeneratorUnitTests
{
    public class ApplicationRunnerTests
    {
        const string StringPart = "string part";
        private ApplicationRunner _sut;
        private Mock<IFileService> _fileService;

        [SetUp]
        public void Setup()
        {
            var logger = new Mock<ILogger<ApplicationRunner>>();
            var appSettings = new Mock<IAppSettings>();
            appSettings.Setup(x => x.FileSizeInMb).Returns(1);
            var stringGenerator = new Mock<IStringGenerator>();
            stringGenerator.Setup(x => x.GetNewStringPart()).Returns(StringPart);
            _fileService = new Mock<IFileService>();
            _sut = new ApplicationRunner(logger.Object, appSettings.Object, stringGenerator.Object, _fileService.Object);

            _sut.Run();
        }

        [Test]
        public void WhenTheAppIsRunning_ThenLinesWithStringsFromStringGeneratorAreWrittenToOutputFile()
        {
            _fileService.Verify(x => x.WriteLineToOutputFile(It.Is<string>(s => s.EndsWith(StringPart))), Times.AtLeastOnce);
        }
    }
}