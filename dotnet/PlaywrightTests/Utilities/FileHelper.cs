using System;
using System.IO;

namespace PlaywrightTests.Utilities
{
    /// <summary>
    /// Helper class for file and directory operations
    /// </summary>
    public static class FileHelper
    {
        /// <summary>
        /// Create a directory if it doesn't exist
        /// </summary>
        public static void CreateDirectoryIfNotExists(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        /// <summary>
        /// Delete a directory and all its contents
        /// </summary>
        public static void DeleteDirectory(string path)
        {
            if (Directory.Exists(path))
            {
                Directory.Delete(path, recursive: true);
            }
        }

        /// <summary>
        /// Get a unique file name with timestamp
        /// </summary>
        public static string GetTimestampedFileName(string baseFileName, string extension)
        {
            var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            return $"{baseFileName}_{timestamp}.{extension}";
        }

        /// <summary>
        /// Clean old files from a directory
        /// </summary>
        public static void CleanOldFiles(string directoryPath, int daysToKeep)
        {
            if (!Directory.Exists(directoryPath))
                return;

            var files = Directory.GetFiles(directoryPath);
            var cutoffDate = DateTime.Now.AddDays(-daysToKeep);

            foreach (var file in files)
            {
                var fileInfo = new FileInfo(file);
                if (fileInfo.CreationTime < cutoffDate)
                {
                    try
                    {
                        File.Delete(file);
                        Logger.Info($"Deleted old file: {file}");
                    }
                    catch (Exception ex)
                    {
                        Logger.Error($"Failed to delete file: {file}", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Get file size in human-readable format
        /// </summary>
        public static string GetFileSizeFormatted(string filePath)
        {
            if (!File.Exists(filePath))
                return "File not found";

            var fileInfo = new FileInfo(filePath);
            var bytes = fileInfo.Length;

            string[] sizes = { "B", "KB", "MB", "GB", "TB" };
            double len = bytes;
            int order = 0;

            while (len >= 1024 && order < sizes.Length - 1)
            {
                order++;
                len = len / 1024;
            }

            return $"{len:0.##} {sizes[order]}";
        }
    }
}
