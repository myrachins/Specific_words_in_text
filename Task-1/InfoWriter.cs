using System;
using System.Collections.Generic;
using System.IO;

namespace Task_1
{
    class InfoWriter
    {
        /// <summary>
        /// This is main method for work. It just gets rougth params from cmd and does all needed work
        /// </summary>
        /// <param name="args"></param>
        public static void WriteToFile(string[] args)
        {
            try
            {
                string pathInput, pathOutput;
                string[] flags;

                ArgumentsParser.ParseArguments(args, out flags, out pathInput);
                pathOutput = InfoWriter.readFileName("Input path for output file: ");

                string[] linesInput;
                if (pathInput == null)
                {
                    Console.WriteLine();
                    Console.WriteLine("Input text in Console. To stop enter empty line");
                    linesInput = FileParser.GetAllLines();
                }            
                else
                    linesInput = FileParser.GetAllLines(pathInput);

                string[] linesOutput = (string[])linesInput.Clone();

                if (checkForExisting(flags, "-c"))
                    linesOutput = removeEmptyLines(trueWordsPlusSeparators(linesOutput));
                if (checkForExisting(flags, "-s"))
                    linesOutput = removeEmptyLines(stayOnlyOneSeparator(linesOutput));
                if (!checkForExisting(flags, "-q"))
                    File.WriteAllLines(pathOutput, linesOutput);
                if (checkForExisting(flags, "-p"))
                    outputStaticInformation(linesInput, linesOutput);
                if (checkForExisting(flags, "-l"))
                    outputLengthInformation(linesInput);
            }
            catch(Exception exc)
            {
                Console.WriteLine(exc.Message);
            }
        }

        private static string[] removeEmptyLines(string[] lines)
        {
            List<string> cleanLines = new List<string>();
            foreach (var line in lines)
                if (line != String.Empty)
                    cleanLines.Add(line);
            return cleanLines.ToArray();
        }

        private static bool checkForExisting(string[] args, string flag)
        {
            return Array.Exists(args, (arg) => arg == flag);
        }

        private static void outputStaticInformation(string[] linesInput, string[] linesOutput)
        {
            Console.WriteLine();
            Console.WriteLine("Статическая информация:");
            Console.WriteLine($"Символы: входной {symbCount(linesInput)}; выходной {symbCount(linesOutput)}");
            Console.WriteLine($"Строки: входной {linesInput.Length}; выходной {linesOutput.Length}");
            Console.WriteLine($"Слова: входной {FileParser.CountThroughText(linesInput, WordTypes.Word)}; выходной {FileParser.CountThroughText(linesOutput, WordTypes.Word)}");
            Console.WriteLine($"Произвольные слова: входной {FileParser.CountThroughText(linesInput, WordTypes.AnyWord)};");
            Console.WriteLine($"Настоящие слова: входной {FileParser.CountThroughText(linesInput, WordTypes.TrueWord)};");
            Console.WriteLine($"Заглавные слова: входной {FileParser.CountThroughText(linesInput, WordTypes.GenericWord)};");
            Console.WriteLine($"Акронимы: входной {FileParser.CountThroughText(linesInput, WordTypes.AcronimWord)};");
        }

        private static int symbCount(string[] lines)
        {
            int counter = 0;
            foreach (var line in lines)
                counter += line.Length;
            return counter;
        }

        private static void outputLengthInformation(string[] lines)
        {
            Console.WriteLine();
            Console.WriteLine("Информация о длинах:");
            List<string> maxWords = FileParser.ConditionalLengthWords(lines, WordTypes.Word);
            Console.WriteLine("Слово (длина {0}): {1}", listElementLength(maxWords), enumerateList(maxWords));
            List<string> maxAnyWords = FileParser.ConditionalLengthWords(lines, WordTypes.AnyWord);
            Console.WriteLine("Произвольное слово (длина {0}): {1}", listElementLength(maxAnyWords), enumerateList(maxAnyWords));
            List<string> maxTrueWords = FileParser.ConditionalLengthWords(lines, WordTypes.TrueWord);
            Console.WriteLine("Настоящее слово (длина {0}): {1}", listElementLength(maxTrueWords), enumerateList(maxTrueWords));
            List<string> maxGenericWords = FileParser.ConditionalLengthWords(lines, WordTypes.GenericWord);
            Console.WriteLine("Заглавное слово (длина {0}): {1}", listElementLength(maxGenericWords), enumerateList(maxGenericWords));
            List<string> maxAcronimWords = FileParser.ConditionalLengthWords(lines, WordTypes.AcronimWord);
            Console.WriteLine("Акроним (длина {0}): {1}", listElementLength(maxAcronimWords), enumerateList(maxAcronimWords));
        }

        private static int listElementLength(List<string> words)
        {
            return words.Count > 0 ? words[0].Length : 0;
        }

        private static string enumerateList(List<string> words)
        {
            words.Sort();
            string resOut = String.Empty;
            foreach (var word in words)
                resOut += $"{word}; ";
            return resOut;
        }

        private static string[] trueWordsPlusSeparators(string[] lines)
        {
            List<string> trWordsSepar = new List<string>();
            foreach(var line in lines)
            {
                string[] words = LineState.SplitLine(line);
                string[] separators = LineState.SplitSeparators(line);
                bool isSepStart = LineState.IsStartsWithSeparator(line);
                string updatedLine = String.Empty;
                for(int i = 0; i < words.Length; i++)
                {
                    int indSep = isSepStart ? i + 1 : i;
                    if(Conditions.IsTrueWord(words[i]))
                    {
                        updatedLine += words[i] + ((indSep == separators.Length) ? String.Empty : separators[indSep]);
                    }
                }
                trWordsSepar.Add(updatedLine);
            }
            return trWordsSepar.ToArray();
        }

        private static string[] stayOnlyOneSeparator(string[] lines)
        {
            List<string> updatedLines = new List<string>();
            foreach(var line in lines)
            {
                string[] words = LineState.SplitLine(line);
                updatedLines.Add(groupWordsToLineWithOneSeparator(words));
            }
            return updatedLines.ToArray();
        }

        private static string groupWordsToLineWithOneSeparator(string[] words)
        {
            string line = String.Empty;
            for (int i = 0; i < words.Length; i++)
            {
                line += words[i];
                if (i != words.Length - 1)
                    line += " ";
            }
            return line;
        }

        private static string readFileName(string str)
        {
            Console.Write(str);
            return Console.ReadLine();
        }
    }
}
