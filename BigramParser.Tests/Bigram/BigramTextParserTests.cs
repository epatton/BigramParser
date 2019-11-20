using NUnit.Framework;
using BigramParser.Core.Parsers;
using System.Collections.Generic;
using BigramParser.Tests.Helpers;

namespace BigramParser.Tests.Bigram
{
    public class BigramTextParserTests
    {
        [TestCase("test\tcase")]
        [TestCase("test\t case")]
        [TestCase("test \tcase")]
        [TestCase("test \t case")]
        [TestCase("\ttest case")]
        [TestCase("test case\t")]
        [TestCase("\ttest case\t")]
        [TestCase("test\t\t\t\t\t\t\t\t\t\tcase")]
        [TestCase("\t\t\t\t\ttest\t\t\t\t\t\t\t\t\t\tcase\t\t\t\t\t")]
        public void Parse_InputContainsTabs_ParsesSuccessfully(string input)
        {
            // Arrange
            var parser = new BigramTextParser(input);
            var expectedResult = new Dictionary<string, int>()
            {
                { "test case", 1 }
            };

            // Act
            var result = parser.Parse();

            // Assert
            Assert.IsTrue(EqualityChecks.
                DictionariesAreEqual(result, expectedResult));
        }

        [TestCase("test case")]
        [TestCase("test  case")]
        [TestCase("test  case")]
        [TestCase("test   case")]
        [TestCase(" test case")]
        [TestCase("test case ")]
        [TestCase(" test case ")]
        [TestCase("test          case")]
        [TestCase("     test          case     ")]
        public void Parse_InputContainsExtraSpaces_ParsesSuccessfully(string input)
        {
            // Arrange
            var parser = new BigramTextParser(input);
            var expectedResult = new Dictionary<string, int>()
            {
                { "test case", 1 }
            };

            // Act
            var result = parser.Parse();

            // Assert
            Assert.IsTrue(EqualityChecks.
                DictionariesAreEqual(result, expectedResult));
        }

        [TestCase("test\t case")]
        [TestCase("test\t  case")]
        [TestCase("test \t case")]
        [TestCase("test \t  case")]
        [TestCase("\t test case")]
        [TestCase("test case\t ")]
        [TestCase("\t test case\t ")]
        [TestCase("test\t \t \t \t \t \t \t \t \t \t case")]
        [TestCase("\t \t \t \t \t test\t \t \t \t \t \t \t \t \t \t case\t \t \t \t \t ")]
        public void Parse_InputContainsTabsAndExtraSpaces_ParsesSuccessfully(string input)
        {
            // Arrange
            var parser = new BigramTextParser(input);
            var expectedResult = new Dictionary<string, int>()
            {
                { "test case", 1 }
            };

            // Act
            var result = parser.Parse();

            // Assert
            Assert.IsTrue(EqualityChecks.
                DictionariesAreEqual(result, expectedResult));
        }

        [TestCase("test\ncase")]
        [TestCase("test\n case")]
        [TestCase("test \ncase")]
        [TestCase("test \n case")]
        [TestCase("\ntest case")]
        [TestCase("test case\n")]
        [TestCase("\ntest case\n")]
        [TestCase("test\n\n\n\n\n\n\n\n\n\ncase")]
        [TestCase("\n\n\n\n\ntest\n\n\n\n\n\n\n\n\n\ncase\n\n\n\n\n")]
        [TestCase("test\r\ncase")]
        [TestCase("test\r\n case")]
        [TestCase("test \r\ncase")]
        [TestCase("test \r\n case")]
        [TestCase("\r\ntest case")]
        [TestCase("test case\r\n")]
        [TestCase("\r\ntest case\r\n")]
        [TestCase("test\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\ncase")]
        [TestCase("\r\n\r\n\r\n\r\n\r\ntest\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\ncase\r\n\r\n\r\n\r\n\r\n")]
        [TestCase("test\rcase")]
        [TestCase("test\r case")]
        [TestCase("test \rcase")]
        [TestCase("test \r case")]
        [TestCase("\rtest case")]
        [TestCase("test case\r")]
        [TestCase("\rtest case\r")]
        [TestCase("test\r\r\r\r\r\r\r\r\r\rcase")]
        [TestCase("\r\r\r\r\rtest\r\r\r\r\r\r\r\r\r\rcase\r\r\r\r\r")]
        public void Parse_InputContainsNewLines_ParsesSuccessfully(string input)
        {
            // Arrange
            var parser = new BigramTextParser(input);
            var expectedResult = new Dictionary<string, int>()
            {
                { "test case", 1 }
            };

            // Act
            var result = parser.Parse();

            // Assert
            Assert.IsTrue(EqualityChecks.
                DictionariesAreEqual(result, expectedResult));
        }

        [TestCase("Test case! Pretty neat, huh?")]
        [TestCase("Test case. Pretty neat, huh?")]
        [TestCase("Test case: Pretty neat; huh?")]
        [TestCase("Test case!Pretty neat,huh?")]
        [TestCase("Test case.Pretty neat,huh?")]
        [TestCase("Test case:Pretty neat;huh?")]
        public void Parse_InputContainsPunctuations_ParsesSuccessfully(string input)
        {
            // Arrange
            var parser = new BigramTextParser(input);
            var expectedResult = new Dictionary<string, int>()
            {
                { "test case", 1 },
                { "case pretty", 1 },
                { "pretty neat", 1 },
                { "neat huh", 1 }
            };

            // Act
            var result = parser.Parse();

            // Assert
            Assert.IsTrue(EqualityChecks.
                DictionariesAreEqual(result, expectedResult));
        }

        [TestCase("Test @`~#$%^&*()_-+=[{]}\\|<>/case")]
        public void Parse_InputContainsSymbols_ParsesSuccessfully(string input)
        {
            // Arrange
            var parser = new BigramTextParser(input);
            var expectedResult = new Dictionary<string, int>()
            {
                { "test case", 1 }
            };

            // Act
            var result = parser.Parse();

            // Assert
            Assert.IsTrue(EqualityChecks.
                DictionariesAreEqual(result, expectedResult));
        }

        [TestCase("I haven't and can't, Mr. O'Reilly.")]
        [TestCase("'I' haven't' and' can't,''' Mr'. O'Reilly'.")]
        public void Parse_InputContainsContractions_ParsesSuccessfully(string input)
        {
            // Arrange
            var parser = new BigramTextParser(input);
            var expectedResult = new Dictionary<string, int>()
            {
                { "i haven't", 1 },
                { "haven't and", 1 },
                { "and can't", 1 },
                { "can't mr", 1 },
                { "mr o'reilly", 1 }
            };

            // Act
            var result = parser.Parse();

            // Assert
            Assert.IsTrue(EqualityChecks.
                DictionariesAreEqual(result, expectedResult));
        }
    }
}
