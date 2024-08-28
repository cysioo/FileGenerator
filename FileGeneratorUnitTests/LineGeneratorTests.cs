using FileGenerator;
using FileGenerator.LineGeneration;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileGeneratorUnitTests
{
    internal class LineGeneratorTests
    {
        private LineGenerator _sut;
        private Mock<IAppSettings> _appSettings;

        [SetUp]
        public void Setup()
        {
            _appSettings = new Mock<IAppSettings>();
            var fileService = new Mock<IFileService>();

            _sut = new LineGenerator(_appSettings.Object, fileService.Object);
        }

        [Test]
        public void WhenTemplateContainsTokenInTheFront_ThenThe1stResultItemIsTheToken()
        {
            var lineTemplate = "{{sequence}}text before {{words:3}} text after";
            _appSettings.Setup(x => x.LineTemplate).Returns(lineTemplate);

            var result = _sut.SplitLineTemplateIntoParts(lineTemplate);

            Assert.That(result[0].Text, Is.EqualTo("{{sequence}}"));
            Assert.That(result[0].TokenType, Is.EqualTo(TokenType.Sequence));
        }

        [Test]
        public void WhenTemplateContainsTokenInTheMiddle_ThenResultContainsTokenInTheMiddle()
        {
            var lineTemplate = "{{sequence}}text before {{words:3}} text after";
            _appSettings.Setup(x => x.LineTemplate).Returns(lineTemplate);

            var result = _sut.SplitLineTemplateIntoParts(lineTemplate);

            Assert.IsTrue(result.Count == 4);
            Assert.That(result[2].Text, Is.EqualTo("{{words:3}}"));
            Assert.That(result[2].TokenType, Is.EqualTo(TokenType.Words));
        }

        [Test]
        public void WhenTemplateContainsTextInTheFront_ThenThe1stResultItemIsTheText()
        {
            var lineTemplate = "text before {{words:3}} text after";
            _appSettings.Setup(x => x.LineTemplate).Returns(lineTemplate);

            var result = _sut.SplitLineTemplateIntoParts(lineTemplate);

            Assert.That(result[0].Text, Is.EqualTo("text before "));
            Assert.That(result[0].TokenType, Is.EqualTo(TokenType.Text));
        }

        [Test]
        public void WhenTemplateContainsTextInTheEnd_ThenTheLastResultItemIsTheText()
        {
            var lineTemplate = "text before {{words:3}} text after";
            _appSettings.Setup(x => x.LineTemplate).Returns(lineTemplate);

            var result = _sut.SplitLineTemplateIntoParts(lineTemplate);

            Assert.That(result[0].Text, Is.EqualTo("text before "));
            Assert.That(result[0].TokenType, Is.EqualTo(TokenType.Text));
        }

        [Test]
        public void GivenLineTemplateWithSequence_WhenGettingTokenType_ThenSequenceIsReturned()
        {
            var lineTemplate = "{{sequence}}";
            _appSettings.Setup(x => x.LineTemplate).Returns(lineTemplate);

            var result = _sut.GetTokenType(lineTemplate);

            Assert.That(result, Is.EqualTo(TokenType.Sequence));
        }

        [Test]
        public void GivenLineTemplateWithSequenceWithDelimiter_WhenGettingTokenType_ThenSequenceIsReturned()
        {
            var lineTemplate = "{{sequence:1}}";
            _appSettings.Setup(x => x.LineTemplate).Returns(lineTemplate);

            var result = _sut.GetTokenType(lineTemplate);

            Assert.That(result, Is.EqualTo(TokenType.Sequence));
        }

        [Test]
        public void GivenLineTemplateWithText_WhenGettingTokenType_ThenSequenceIsReturned()
        {
            var lineTemplate = "{{sequence:1";      // this is text not a token because it doesn't have corresponding closing braces
            _appSettings.Setup(x => x.LineTemplate).Returns(lineTemplate);

            var result = _sut.GetTokenType(lineTemplate);

            Assert.That(result, Is.EqualTo(TokenType.Text));
        }

        [Test]
        public void GivenLineTemplateWithUndefinedToken_WhenGettingTokenType_ThenUnknownIsReturned()
        {
            var lineTemplate = "{{not defined}}";
            _appSettings.Setup(x => x.LineTemplate).Returns(lineTemplate);

            var result = _sut.GetTokenType(lineTemplate);

            Assert.That(result, Is.EqualTo(TokenType.Unknown));
        }

        [Test]
        public void GivenTokenWith2Parameters_WhenGettingTokenParameters_Then2ParametersAreReturned()
        {
            const string param1 = "1";
            const string param2 = "param 2";
            var lineTemplate = "{{token:" + param1 + ":" + param2 + "}}";
            _appSettings.Setup(x => x.LineTemplate).Returns(lineTemplate);

            var result = _sut.GetTokenParameters(lineTemplate);

            Assert.That(result.Length, Is.EqualTo(2));
            Assert.That(result[0], Is.EqualTo(param1));
            Assert.That(result[1], Is.EqualTo(param2));
        }

        [Test]
        public void GivenTokenWithoutParameters_WhenGettingTokenParameters_ThenEmptyArrayIsReturned()
        {
            var lineTemplate = "{{token}}";
            _appSettings.Setup(x => x.LineTemplate).Returns(lineTemplate);

            var result = _sut.GetTokenParameters(lineTemplate);

            Assert.That(result.Length, Is.EqualTo(0));
        }

        [Test]
        public void GivenTokenWithDelimiterButNoParameters_WhenGettingTokenParameters_ThenEmptyArrayIsReturned()
        {
            var lineTemplate = "{{token:}}";
            _appSettings.Setup(x => x.LineTemplate).Returns(lineTemplate);

            var result = _sut.GetTokenParameters(lineTemplate);

            Assert.That(result.Length, Is.EqualTo(0));
        }
    }
}
