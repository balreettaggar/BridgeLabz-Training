using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Text.RegularExpressions;
using System.ComponentModel;
namespace advanced_c_sharp
{
    internal class RegexProblems
    {
        static internal void ValidateUsername()
        {
            string username = "Balreet_10";
            bool result = Regex.IsMatch(username, @"^[A-Za-z][A-Za-z0-9_]{4,14}$");
            Console.WriteLine(result == true);
        }

        static internal void LicensePlate()
        {
            string license = "AB214";
            bool result = Regex.IsMatch(license, @"^[A-Z]{2}[0-9]{4}$");
            Console.WriteLine(result == true);
       }

        
    }
}
