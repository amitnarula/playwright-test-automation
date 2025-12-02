using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using Microsoft.Playwright;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using System;
using System.IO;
using System.Threading.Tasks;

namespace PlaywrightTests.Tests
{
    /// <summary>
    /// Base test class for all test classes
    /// Handles browser setup, teardown, and reporting
    /// </summary>
    public class BaseTest
    {
        protected IPlaywright? _playwright;
        protected IBrowser? _browser;
        protected IPage? _page;
        protected IBrowserContext? _context;

        // ExtentReports instance
        protected static ExtentReports? _extent;
        protected ExtentTest? _test;

        private static readonly string ReportPath = Path.Combine(Directory.GetCurrentDirectory(), "Reports", "TestReport.html");
        private static readonly string ScreenshotPath = Path.Combine(Directory.GetCurrentDirectory(), "Reports", "Screenshots");

        /// <summary>
        /// OneTime setup - runs once before all tests in the test class
        /// </summary>
        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            // Create directories if they don't exist
            Directory.CreateDirectory(Path.GetDirectoryName(ReportPath)!);
            Directory.CreateDirectory(ScreenshotPath);

            // Initialize ExtentReports
            var htmlReporter = new ExtentSparkReporter(ReportPath);
            htmlReporter.Config.DocumentTitle = "Playwright Test Automation Report";
            htmlReporter.Config.ReportName = "Test Execution Report";
            htmlReporter.Config.Theme = AventStack.ExtentReports.Reporter.Config.Theme.Dark;

            _extent = new ExtentReports();
            _extent.AttachReporter(htmlReporter);
            _extent.AddSystemInfo("Environment", "QA");
            _extent.AddSystemInfo("User", Environment.UserName);
            _extent.AddSystemInfo("OS", Environment.OSVersion.ToString());
        }

        /// <summary>
        /// Setup method - runs before each test
        /// </summary>
        [SetUp]
        public async Task Setup()
        {
            // Create test node in report
            _test = _extent?.CreateTest(TestContext.CurrentContext.Test.Name);

            // Initialize Playwright
            _playwright = await Playwright.CreateAsync();

            // Launch browser (you can configure browser type, headless mode, etc.)
            _browser = await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false, // Set to false to see browser UI
                SlowMo = 50 // Slow down operations by 50ms for better visibility
            });

            // Create browser context
            _context = await _browser.NewContextAsync(new BrowserNewContextOptions
            {
                //ViewportSize = new ViewportSize { Width = 1920, Height = 1080 },
                RecordVideoDir = Path.Combine(Directory.GetCurrentDirectory(), "Reports", "Videos")
            });

            // Create new page
            _page = await _context.NewPageAsync();
        }

        /// <summary>
        /// Teardown method - runs after each test
        /// </summary>
        [TearDown]
        public async Task Teardown()
        {
            var outcome = TestContext.CurrentContext.Result.Outcome.Status;
            var errorMessage = TestContext.CurrentContext.Result.Message;
            var stackTrace = TestContext.CurrentContext.Result.StackTrace;

            try
            {
                // Take screenshot on failure
                if (outcome == TestStatus.Failed && _page != null)
                {
                    var screenshotFileName = $"{TestContext.CurrentContext.Test.Name}_{DateTime.Now:yyyyMMdd_HHmmss}.png";
                    var screenshotPath = Path.Combine(ScreenshotPath, screenshotFileName);
                    
                    await _page.ScreenshotAsync(new PageScreenshotOptions
                    {
                        Path = screenshotPath,
                        FullPage = true
                    });

                    // Add screenshot to report
                    _test?.Fail(errorMessage, MediaEntityBuilder.CreateScreenCaptureFromPath(screenshotPath).Build());
                    _test?.Log(Status.Fail, $"Stack Trace: {stackTrace}");
                }
                else if (outcome == TestStatus.Passed)
                {
                    _test?.Pass("Test passed successfully");
                }
                else if (outcome == TestStatus.Skipped)
                {
                    _test?.Skip("Test was skipped");
                }
            }
            catch (Exception ex)
            {
                _test?.Warning($"Error in teardown: {ex.Message}");
            }
            finally
            {
                // Close browser resources
                if (_page != null)
                {
                    await _page.CloseAsync();
                }

                if (_context != null)
                {
                    await _context.CloseAsync();
                }

                if (_browser != null)
                {
                    await _browser.CloseAsync();
                }

                if (_playwright != null)
                {
                    _playwright.Dispose();
                }
            }
        }

        /// <summary>
        /// OneTime teardown - runs once after all tests in the test class
        /// </summary>
        [OneTimeTearDown]
        public void OneTimeTeardown()
        {
            // Flush ExtentReports
            _extent?.Flush();
        }

        /// <summary>
        /// Log information to the test report
        /// </summary>
        protected void LogInfo(string message)
        {
            _test?.Info(message);
            TestContext.WriteLine(message);
        }

        /// <summary>
        /// Log a pass message to the test report
        /// </summary>
        protected void LogPass(string message)
        {
            _test?.Pass(message);
            TestContext.WriteLine($"PASS: {message}");
        }

        /// <summary>
        /// Log a fail message to the test report
        /// </summary>
        protected void LogFail(string message)
        {
            _test?.Fail(message);
            TestContext.WriteLine($"FAIL: {message}");
        }
    }
}
