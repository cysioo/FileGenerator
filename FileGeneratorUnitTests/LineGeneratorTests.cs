using FileGenerator.LineGeneration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileGeneratorUnitTests
{
    internal class LineGeneratorTests
    {
        private LineGenerator _sut = new LineGenerator();

        [Test]
        public void WhenTemplateContainsTokenInTheFront_ThenThe1stResultItemIsTheToken()
        {
            var lineTemplate = "{{sequence}}text before {{words:3}} text after";

            var result = _sut.SplitLineTemplateIntoParts(lineTemplate);

            Assert.That(result[0].Text, Is.EqualTo("{{sequence}}"));
            Assert.That(result[0].TokenType, Is.EqualTo(TokenType.Sequence));
        }

        [Test]
        public void WhenTemplateContainsTokenInTheMiddle_ThenResultContainsTokenInTheMiddle()
        {
            var lineTemplate = "{{sequence}}text before {{words:3}} text after";
            
            var result = _sut.SplitLineTemplateIntoParts(lineTemplate);

            Assert.IsTrue(result.Count == 4);
            Assert.That(result[2].Text, Is.EqualTo("{{words:3}}"));
            Assert.That(result[2].TokenType, Is.EqualTo(TokenType.Words));
        }

        [Test]
        public void WhenTemplateContainsTextInTheFront_ThenThe1stResultItemIsTheText()
        {
            var lineTemplate = "text before {{words:3}} text after";

            var result = _sut.SplitLineTemplateIntoParts(lineTemplate);

            Assert.That(result[0].Text, Is.EqualTo("text before "));
            Assert.That(result[0].TokenType, Is.EqualTo(TokenType.Text));
        }

        [Test]
        public void WhenTemplateContainsTextInTheEnd_ThenTheLastResultItemIsTheText()
        {
            var lineTemplate = "text before {{words:3}} text after";

            var result = _sut.SplitLineTemplateIntoParts(lineTemplate);

            Assert.That(result[0].Text, Is.EqualTo("text before "));
            Assert.That(result[0].TokenType, Is.EqualTo(TokenType.Text));
        }

        [Test]
        public void GivenLineTemplateWithSequence_WhenGettingTokenType_ThenSequenceIsReturned()
        {
            var lineTemplate = "{{sequence}}";

            var result = _sut.GetTokenType(lineTemplate);

            Assert.That(result, Is.EqualTo(TokenType.Sequence));
        }

        [Test]
        public void GivenLineTemplateWithSequenceWithDelimiter_WhenGettingTokenType_ThenSequenceIsReturned()
        {
            var lineTemplate = "{{sequence:1}}";

            var result = _sut.GetTokenType(lineTemplate);

            Assert.That(result, Is.EqualTo(TokenType.Sequence));
        }

        [Test]
        public void GivenLineTemplateWithText_WhenGettingTokenType_ThenSequenceIsReturned()
        {
            var lineTemplate = "{{sequence:1";      // this is text not a token because it doesn't have corresponding closing braces

            var result = _sut.GetTokenType(lineTemplate);

            Assert.That(result, Is.EqualTo(TokenType.Text));
        }

        [Test]
        public void GivenLineTemplateWithUndefinedToken_WhenGettingTokenType_ThenUnknownIsReturned()
        {
            var lineTemplate = "{{not defined}}";     

            var result = _sut.GetTokenType(lineTemplate);

            Assert.That(result, Is.EqualTo(TokenType.Unknown));
        }
    }
}
