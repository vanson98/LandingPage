using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace LadingPage.Common.Utility
{
    public static class StringExtension
    {
        public static string GetSeoName(this string str)
        {
            var seName = str?.RemoveSign4VietnameseString().Trim().Replace(" ", "-").ToLower();
            Regex rgx = new Regex("[^a-zA-Z0-9-]");
            if (!string.IsNullOrEmpty(seName))
            {
                seName = rgx.Replace(seName, "");
            }
            return seName;
        }
        public static string RemoveSign4VietnameseString(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return str;
            }
            var regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            var temp = str.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, string.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }
    }
}
