using FileGenerator;
using FileGenerator.LineGeneration.TokenGeneration;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileGeneratorUnitTests
{
    public class WordsGeneratorTests
    {
        private string[] _words = ["w1", "w2"];

        [Test]
        public void WhenThereAreNoParameters_ThenBetween1And10WordsAreGenerated()
        {
            var appSettings = new Mock<IAppSettings>();
            var fileService = new Mock<IFileService>();
            fileService.Setup(x => x.Words).Returns(_words);
            var sut = new WordsGenerator(appSettings.Object, fileService.Object, []);

            var result = sut.Generate();
            var words = result.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            Assert.GreaterOrEqual(words.Length, 1);
            Assert.LessOrEqual(words.Length, 10);
        }

        [Test]
        public void WhenThereIs1Parameter_ThenAtLeastThatNumberOfWordsAndMax10WordsIsGenerated()
        {
            var appSettings = new Mock<IAppSettings>();
            var fileService = new Mock<IFileService>();
            fileService.Setup(x => x.Words).Returns(_words);
            var sut = new WordsGenerator(appSettings.Object, fileService.Object, ["10"]);

            var result = sut.Generate();
            var words = result.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            Assert.That(words.Length, Is.EqualTo(10));
        }

        [Test]
        public void WhenThereAre2Parameters_ThenANumberBetweenTheseParametersIsGenerated()
        {
            var appSettings = new Mock<IAppSettings>();
            var fileService = new Mock<IFileService>();
            fileService.Setup(x => x.Words).Returns(_words);
            var sut = new WordsGenerator(appSettings.Object, fileService.Object, ["2", "2"]);

            var result = sut.Generate();
            var words = result.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            Assert.That(words.Length, Is.EqualTo(2));
        }

        [Test]
        public void WhenThereParameter1IsGreaterThenParameter2_ThenAnExceptionIsThrown()
        {
            var appSettings = new Mock<IAppSettings>();
            var fileService = new Mock<IFileService>();
            fileService.Setup(x => x.Words).Returns(_words);

            Assert.Throws<ArgumentException>(() => new WordsGenerator(appSettings.Object, fileService.Object, ["2", "1"]));
        }

        [Test]
        public void WhenTheMinNumberOfWordsIsLowerThen1_ThenAnExceptionIsThrown()
        {
            var appSettings = new Mock<IAppSettings>();
            var fileService = new Mock<IFileService>();
            fileService.Setup(x => x.Words).Returns(_words);

            Assert.Throws<ArgumentException>(() => new WordsGenerator(appSettings.Object, fileService.Object, ["0"]));
        }
    }
}
