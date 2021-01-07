using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Michaelsoft.ContentManager.Common.Extensions
{
    public static class StringHelper
    {

        private static Random Random = new Random();

        public static string Capitalize(this string s)
        {
            if (string.IsNullOrEmpty(s)) return string.Empty;
            return char.ToUpper(s[0]) + s.Substring(1);
        }

        public static string ToUrlFriendly(this string s)
        {
            if (string.IsNullOrEmpty(s)) return string.Empty;
            return Regex.Replace(s.ToLower(), "[^a-z0-9\\s\\-]+", "").Replace(" ", "-");
        }

        public static string Sha1(this string s)
        {
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(s));
                var sb = new StringBuilder(hash.Length * 2);
                foreach (byte b in hash)
                {
                    sb.Append(b.ToString("X2"));
                }

                return sb.ToString().Substring(0, 40);
            }
        }

        public static bool IsNullOrEmpty(this string s)
        {
            return s == null || s.Trim().Equals("");
        }

        public static string RandomString(int length,
                                          string symbols = "",
                                          bool uppercase = true,
                                          bool lowercase = true,
                                          bool numbers = true)
        {
            if (symbols.IsNullOrEmpty() && !uppercase && !lowercase && !numbers)
                throw new Exception("Cannot generate a random string with these arguments.");

            var groups = new List<string>();

            if (uppercase)
                groups.Add("ABCDEFGHIJKLMNOPQRSTUVWXYZ");

            if (lowercase)
                groups.Add("abcdefghijklmnopqrstuvwxyz");

            if (numbers)
                groups.Add("0123456789");

            groups.Add(symbols);

            var sb = new StringBuilder();
            for (var i = 0; i < length; i++)
            {
                var group = groups[i % groups.Count];
                if (group.IsNullOrEmpty()) group = groups[0];
                sb.Append(new string(Enumerable.Repeat(group, 1)
                                               .Select(s => s[Random.Next(s.Length)]).ToArray()));
            }

            var tempResult = sb.ToString();
            return new string(Enumerable.Repeat(tempResult, length)
                                        .Select(s => s[Random.Next(s.Length)]).ToArray());
        }

    }
}