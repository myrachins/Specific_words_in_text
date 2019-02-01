using System;
using System.Collections.Generic;
using System.IO;

namespace Task_1
{
    static class ArgumentsParser
    {
        /// <summary>
        /// This method gets all needed information from cmd arguments
        /// </summary>
        /// <param name="args"></param>
        /// <param name="flags"></param>
        /// <param name="fileInput"></param>
        public static void ParseArguments(string[] args, out string[] flags, out string fileInput)
        {
            List<string> lstFlags = new List<string>();
            bool condition = false;
            fileInput = null;
            foreach(var arg in args)
            {
                if (arg.StartsWith("-"))
                    lstFlags.Add(arg);
                else
                {
                    if (condition)
                        throw new ArgumentException("TOO MANY FILES");
                    fileInput = arg;
                    condition = true;
                }
            }
            flags = lstFlags.ToArray();
            checkArgs(flags);
        }

        private static void checkArgs(string[] args)
        {
            if (Array.Exists(args, (arg) => arg != "-q" && arg != "-s" &&
            arg != "-c" && arg != "-p" && arg != "-l"))
                throw new ArgumentException("INVALID FLAG");
            if (Array.Exists(args, (arg) => (arg == "-s" || arg == "-c")) &&
                Array.Exists(args, (arg) => arg == "-q"))
                throw new ArgumentException("CONFLICTING FLAGS");
        }
    }
}
