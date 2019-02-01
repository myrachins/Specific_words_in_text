using System;
using System.Collections.Generic;

namespace Task_1
{
    static class Conditions
    {
        /// <summary>
        /// This method checks word with input condition
        /// </summary>
        /// <param name="word"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        private static bool isSuitable(string word, Predicate<char> condition)
        {
            foreach (var ch in word)
            {
                if (!condition(ch))
                    return false;
            }
            return true;
        }

        public static bool IsWord(string word)
        {
            // We always recieve only word
            return true;
        }

        public static bool IsAnyWord(string word)
        {
            Predicate<char> condition = (ch) => (ch >= 'a' && ch <= 'z') || (ch >= 'A' && ch <= 'Z') || (ch >= '0' && ch <= '9');
            return isSuitable(word, condition);
        }

        public static bool IsTrueWord(string word)
        {
            Predicate<char> condition = (ch) => (ch >= 'a' && ch <= 'z') || (ch >= 'A' && ch <= 'Z');
            return isSuitable(word, condition);
        }

        public static bool IsAcronimWord(string word)
        {
            Predicate<char> condition = (ch) => (ch >= 'A' && ch <= 'Z');
            return isSuitable(word, condition);
        }

        private static bool isLittleWord(string word)
        {
            Predicate<char> condition = (ch) => (ch >= 'a' && ch <= 'z');
            return isSuitable(word, condition);
        }

        public static bool IsGenericWord(string word)
        {
            string first = word.Substring(0, 1);
            string second = word.Substring(1);
            return IsAcronimWord(first) && isLittleWord(second);
        }

        public static bool IsSeparator(char ch)
        {
            return ch == ' ' || ch == '\n' || ch == '\t';
        }
    }
}
