using System;
using System.Diagnostics;

namespace PlaywrightTests.Utilities
{
    /// <summary>
    /// Logger utility for test execution logging
    /// </summary>
    public static class Logger
    {
        private static readonly string LogFilePath = $"Reports/Logs/TestLog_{DateTime.Now:yyyyMMdd_HHmmss}.txt";

        static Logger()
        {
            // Create log directory if it doesn't exist
            var logDir = System.IO.Path.GetDirectoryName(LogFilePath);
            if (!string.IsNullOrEmpty(logDir) && !System.IO.Directory.Exists(logDir))
            {
                System.IO.Directory.CreateDirectory(logDir);
            }
        }

        public static void Info(string message)
        {
            Log("INFO", message);
        }

        public static void Debug(string message)
        {
            Log("DEBUG", message);
        }

        public static void Warning(string message)
        {
            Log("WARNING", message);
        }

        public static void Error(string message)
        {
            Log("ERROR", message);
        }

        public static void Error(string message, Exception ex)
        {
            Log("ERROR", $"{message} - Exception: {ex.Message}\nStackTrace: {ex.StackTrace}");
        }

        private static void Log(string level, string message)
        {
            var logMessage = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{level}] {message}";
            
            // Console output
            Console.WriteLine(logMessage);
            
            // Debug output
            System.Diagnostics.Debug.WriteLine(logMessage);
            
            // File output
            try
            {
                System.IO.File.AppendAllText(LogFilePath, logMessage + Environment.NewLine);
            }
            catch
            {
                // Ignore file write errors
            }
        }
    }
}
