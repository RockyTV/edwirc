using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace edwirc
{
    public class Logger
    {
        private static string logsDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs");
        private static string logFile = string.Format("edwirc {0}.log", DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss"));

        public static void Write(string message, params object[] args)
        {
            string output = string.Format("[{0}] {1}", DateTime.Now.ToString("HH:mm:ss"), message);
            try
            {
                File.AppendAllText(logFile, output);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error writing to log file!");
                Console.WriteLine(e.ToString());
            }
        }
    }
}
