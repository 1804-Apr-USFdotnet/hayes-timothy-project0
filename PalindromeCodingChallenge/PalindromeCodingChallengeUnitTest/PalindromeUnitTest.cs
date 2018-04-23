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

            // Palindromes
            string caseInsensitive = "Racecar";
            string punctuation = "never Odd, or Even.";
            string numbers = "121";
            string singleChar = "A";

            // Non-Palindromes
            string nullStr = null;
            string emptyString = "";
            string failed = "1A2";

            // Act
            bool a1 = caseInsensitive.IsPalindrome();
            bool e1 = true;

            bool a2 = punctuation.IsPalindrome();
            bool e2 = true;

            bool a3 = numbers.IsPalindrome();
            bool e3 = true;

            bool a4 = singleChar.IsPalindrome();
            bool e4 = true;

            bool a5 = nullStr.IsPalindrome();
            bool e5 = false;

            bool a6 = emptyString.IsPalindrome();
            bool e6 = false;

            bool a7 = failed.IsPalindrome();
            bool e7 = false;

            // Assert
            Assert.AreEqual(a1, e1);
            Assert.AreEqual(a2, e2);
            Assert.AreEqual(a3, e3);
            Assert.AreEqual(a4, e4);
            Assert.AreEqual(a5, e5);
            Assert.AreEqual(a6, e6);
            Assert.AreEqual(a7, e7);

        }
    }
}
