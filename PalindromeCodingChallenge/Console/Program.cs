﻿using PalindromeCodingChallenge;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string caseInsensitive = "never Odd, or Even.";
            bool a1 = caseInsensitive.IsPalindrome();
            Console.WriteLine(a1);
        }
    }
}
