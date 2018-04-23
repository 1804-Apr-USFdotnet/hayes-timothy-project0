using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PalindromeCodingChallenge;

namespace PalindromeCodingChallengeUnitTest
{
    [TestClass]
    public class PalindromeUnitTest
    {
        [TestMethod]
        public void TestIsPalindrome()
        {
            // Arrange
            string caseInsensitive = "Racecar";

            // Act
            bool a1 = caseInsensitive.IsPalindrome();
            bool e1 = true;

            // Assert
            Assert.AreEqual(a1, e1);

        }
    }
}
