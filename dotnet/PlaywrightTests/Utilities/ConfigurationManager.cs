using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace PlaywrightTests.Utilities
{
    /// <summary>
    /// Configuration manager to read test settings from appsettings.json
    /// </summary>
    public static class ConfigurationManager
    {
        private static IConfiguration? _configuration;

        static ConfigurationManager()
        {
            Initialize();
        }

        private static void Initialize()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            _configuration = builder.Build();
        }

        public static string GetBrowser()
        {
            return _configuration?["TestSettings:Browser"] ?? "chromium";
        }

        public static bool IsHeadless()
        {
            return bool.Parse(_configuration?["TestSettings:Headless"] ?? "true");
        }

        public static int GetSlowMo()
        {
            return int.Parse(_configuration?["TestSettings:SlowMo"] ?? "50");
        }

        public static int GetTimeout()
        {
            return int.Parse(_configuration?["TestSettings:Timeout"] ?? "30000");
        }

        public static string GetBaseUrl()
        {
            return _configuration?["TestSettings:BaseUrl"] ?? "https://www.google.com";
        }

        public static bool ScreenshotOnFailure()
        {
            return bool.Parse(_configuration?["TestSettings:ScreenshotOnFailure"] ?? "true");
        }

        public static bool VideoRecording()
        {
            return bool.Parse(_configuration?["TestSettings:VideoRecording"] ?? "true");
        }

        public static int GetViewportWidth()
        {
            return int.Parse(_configuration?["TestSettings:ViewportWidth"] ?? "1920");
        }

        public static int GetViewportHeight()
        {
            return int.Parse(_configuration?["TestSettings:ViewportHeight"] ?? "1080");
        }

        public static string GetReportPath()
        {
            return _configuration?["ReportSettings:ReportPath"] ?? "Reports/TestReport.html";
        }

        public static string GetReportTitle()
        {
            return _configuration?["ReportSettings:ReportTitle"] ?? "Playwright Test Automation Report";
        }

        public static string GetReportName()
        {
            return _configuration?["ReportSettings:ReportName"] ?? "Test Execution Report";
        }

        public static string GetReportTheme()
        {
            return _configuration?["ReportSettings:Theme"] ?? "Dark";
        }
    }
}
