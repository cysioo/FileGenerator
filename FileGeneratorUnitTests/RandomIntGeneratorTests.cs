using FileGenerator.LineGeneration.TokenGeneration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileGeneratorUnitTests
{
    public class RandomIntGeneratorTests
    {
        [Test]
        public void WhenThereAreNoParameters_ThenAPositiveNumberIsGenerated()
        {
            var sut = new RandomIntGenerator([]);

            var result = sut.Generate();
            var parsedResult = int.Parse(result);

            Assert.GreaterOrEqual(parsedResult, 0);
        }

        [Test]
        public void WhenThereIs1Parameter_ThenANumberNotLowerThenThatParameterIsGenerated()
        {
            var min = (int.MaxValue - 2).ToString();
            var sut = new RandomIntGenerator([min]);

            var result = sut.Generate();
            var parsedResult = int.Parse(result);

            Assert.GreaterOrEqual(parsedResult, 0);
        }

        [Test]
        public void WhenThereAre2Parameters_ThenANumberBetweenTheseParametersIsGenerated()
        {
            var min = 1;
            var max = 2;
            var sut = new RandomIntGenerator([min.ToString(), max.ToString()]);

            var result = sut.Generate();
            var parsedResult = int.Parse(result);

            Assert.GreaterOrEqual(parsedResult, min);
            Assert.LessOrEqual(parsedResult, max);
        }

        [Test]
        public void WhenThereAre2EqualParameters_ThenAnExceptionIsThrown()
        {
            var min = 2;
            var max = 2;

            Assert.Throws<ArgumentException>(() => new RandomIntGenerator([min.ToString(), max.ToString()]));
        }
    }
}
