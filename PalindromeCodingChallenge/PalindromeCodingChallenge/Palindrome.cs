using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PalindromeCodingChallenge
{
    public static class Palindrome
    {
        public static bool IsPalindrome(this string str)
        {
            if(str == null)
            {
                return false;
            }
            else if(str == "")
            {
                return false;
            }
            else
            {
                // Extract only alphanumeric characters -- set all to upcase
                Regex r = new Regex("^[a-zA-Z0-9]*$");
                char[] chars = str.ToCharArray();
                string alphaNumStr = "";
                for(int i = 0; i < chars.Length; i++)
                {
                    string c = "" + chars[i];
                    if(r.IsMatch(c))
                    {
                        alphaNumStr += c.ToUpper();
                    }
                }

                Console.WriteLine(alphaNumStr);

                return true;
            }
        }
    }
}
