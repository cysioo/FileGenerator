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

            var result = _sut.SplitStringIntoParts(lineTemplate);

            Assert.That(result[0].Text, Is.EqualTo("{{sequence}}"));
        }

        [Test]
        public void WhenTemplateContainsTokenInTheMiddle_ThenResultContainsTokenInTheMiddle()
        {
            var lineTemplate = "{{sequence}}text before {{words:3}} text after";
            
            var result = _sut.SplitStringIntoParts(lineTemplate);

            Assert.IsTrue(result.Count == 4);
            Assert.That(result[2].Text, Is.EqualTo("{{words:3}}"));
        }

        [Test]
        public void WhenTemplateContainsTextInTheFront_ThenThe1stResultItemIsTheText()
        {
            var lineTemplate = "text before {{words:3}} text after";

            var result = _sut.SplitStringIntoParts(lineTemplate);

            Assert.That(result[0].Text, Is.EqualTo("text before "));
        }

        [Test]
        public void WhenTemplateContainsTextInTheEnd_ThenTheLastResultItemIsTheText()
        {
            var lineTemplate = "text before {{words:3}} text after";

            var result = _sut.SplitStringIntoParts(lineTemplate);

            Assert.That(result[0].Text, Is.EqualTo("text before "));
        }
    }
}
