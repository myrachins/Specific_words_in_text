using System;
using System.Collections.Generic;
using System.IO;

namespace Task_1
{
    static class FileParser
    {
        /// <summary>
        /// This method calculates the number of entering some type of word in text
        /// </summary>
        /// <param name="lines"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static int CountThroughText(string[] lines, WordTypes type)
        {
            Predicate<string> condition = getConditionFromWordType(type);
            int countInText = 0;
            foreach (var line in lines)
                countInText += LineState.FindCountOfWords(line, condition);
            return countInText;
        }

        /// <summary>
        /// This method finds all longest suitable words in lines 
        /// </summary>
        /// <param name="lines"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static List<string> ConditionalLengthWords(string[] lines, WordTypes type)
        {
            Predicate<string> condition = getConditionFromWordType(type);
            List<string> maxLengthWords = new List<string>();
            int maxLength = -1;
            foreach(var line in lines)
            {
                string[] words = LineState.SplitLine(line);
                foreach(var word in words)
                {
                    if (!condition(word))
                        continue;
                    else if (word.Length > maxLength)
                    {
                        maxLength = word.Length;
                        maxLengthWords.Clear();
                        maxLengthWords.Add(word);
                    }
                    else if (word.Length == maxLength)
                        maxLengthWords.Add(word);
                }
            }
            return maxLengthWords;
        }

        private static Predicate<string> getConditionFromWordType(WordTypes type)
        {
            Predicate<string> condition = null;
            switch (type)
            {
                case WordTypes.Word: condition = Conditions.IsWord; break;
                case WordTypes.AnyWord: condition = Conditions.IsAnyWord; break;
                case WordTypes.TrueWord: condition = Conditions.IsTrueWord; break;
                case WordTypes.GenericWord: condition = Conditions.IsGenericWord; break;
                case WordTypes.AcronimWord: condition = Conditions.IsAcronimWord; break;
            }
            return condition;
        }

        /// <summary>
        /// This method reads all file into string array
        /// </summary>
        /// <param name="pathToFile"></param>
        /// <returns></returns>
        public static string[] GetAllLines(string pathToFile)
        {
            try
            {
                string[] allLines = File.ReadAllLines(pathToFile);
                return allLines;
            }
            catch
            {
                throw new ArgumentException("CANNOT OPEN");
            }
        }

        /// <summary>
        /// This method reads lines from console, while user hasn't entered empty line
        /// </summary>
        /// <returns></returns>
        public static string[] GetAllLines()
        {
            List<string> allLines = new List<string>();
            string currentLine;
            do
            {
                currentLine = Console.ReadLine();
                if (currentLine != String.Empty)
                    allLines.Add(currentLine);
            }
            while (currentLine != String.Empty);
            return allLines.ToArray();
        }
    }
}
