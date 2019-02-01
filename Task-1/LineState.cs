using System;
using System.Collections.Generic;

namespace Task_1
{
    static class LineState
    {
        /// <summary>
        /// This method finds count of suitable words in line
        /// </summary>
        /// <param name="line"></param>
        /// <param name="isSuitable"></param>
        /// <returns></returns>
        public static int FindCountOfWords(string line, Predicate<string> isSuitable)
        {
            string[] words = SplitLine(line);
            return countOfSuitable(words, isSuitable);
        }

        /// <summary>
        /// This method split line, using complex separator
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public static string[] SplitLine(string line)
        {
            List<string> words = new List<string>();
            bool condition = true;
            foreach (var ch in line)
            {
                if (Conditions.IsSeparator(ch))
                {
                    condition = true;
                }
                else
                {
                    if (condition)
                        words.Add(String.Empty);
                    words[words.Count - 1] += ch;
                    condition = false;
                }
            }
            return words.ToArray();
        }

        /// <summary>
        /// This method gets all separators from line
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public static string[] SplitSeparators(string line)
        {
            List<string> separators = new List<string>();
            //separators.Add(String.Empty);
            bool condition = true;
            foreach(var ch in line)
            {
                if (Conditions.IsSeparator(ch))
                {
                    if (condition)
                        separators.Add(String.Empty);
                    separators[separators.Count - 1] += ch;
                    condition = false;
                }
                else
                {
                    condition = true;
                }
            }
            return separators.ToArray();
        }

        /// <summary>
        /// This method checks is line starts with separator
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public static bool IsStartsWithSeparator(string line)
        {
            if (line == String.Empty)
                return false;
            return Conditions.IsSeparator(line[0]);
        }

        private static int countOfSuitable(string[] words, Predicate<string> isSuitable)
        {
            int counter = 0;
            foreach(var word in words)
            {
                if (isSuitable(word))
                    counter++;
            }
            return counter;
        }
    }
}
