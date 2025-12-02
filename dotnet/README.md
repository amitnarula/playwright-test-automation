# Playwright Test Automation Framework - .NET Core

A reusable, robust test automation framework built with **Playwright**, **.NET Core 3.1**, and **NUnit** using the **Page Object Model (POM)** design pattern. This framework includes built-in HTML reporting via ExtentReports.

## 🚀 Features

- ✅ **Page Object Model (POM)** - Maintainable and scalable test architecture
- ✅ **Playwright Integration** - Cross-browser automation support (Chromium, Firefox, WebKit)
- ✅ **NUnit Framework** - Popular .NET testing framework with rich assertions
- ✅ **ExtentReports** - Beautiful HTML reports with screenshots and detailed logs
- ✅ **Configuration Management** - Centralized test settings via `appsettings.json`
- ✅ **Logging Utility** - Comprehensive logging for debugging
- ✅ **Parallel Test Execution** - Run tests concurrently for faster feedback
- ✅ **Screenshot on Failure** - Automatic screenshot capture for failed tests
- ✅ **Video Recording** - Optional video recording of test execution

## 📁 Project Structure

```
PlaywrightTestFramework/
├── PlaywrightTests/
│   ├── PageObjects/           # Page Object Model classes
│   │   ├── BasePage.cs        # Base page with common methods
│   │   ├── GoogleHomePage.cs  # Google home page object
│   │   └── GoogleSearchResultsPage.cs
│   ├── Tests/                 # Test classes
│   │   ├── BaseTest.cs        # Base test with setup/teardown
│   │   └── GoogleSearchTests.cs
│   ├── Utilities/             # Helper utilities
│   │   ├── ConfigurationManager.cs
│   │   ├── Logger.cs
│   │   └── FileHelper.cs
│   ├── Reports/               # Test reports and artifacts
│   │   ├── Screenshots/
│   │   ├── Videos/
│   │   └── Logs/
│   ├── appsettings.json       # Test configuration
│   └── test.runsettings       # Test run settings
└── PlaywrightTestFramework.sln
```

## 🛠️ Prerequisites

- **.NET Core 3.1 SDK** (or later) - [Download](https://dotnet.microsoft.com/download)
- **Visual Studio 2019/2022** or **VS Code**
- **Git** (optional)

## 📦 Installation

### 1. Clone or Download the Project

```bash
git clone <repository-url>
cd PlaywrightTestFramework
```

### 2. Restore NuGet Packages

```bash
dotnet restore
```

### 3. Install Playwright Browsers

```bash
cd PlaywrightTests
pwsh bin/Debug/netcoreapp3.1/playwright.ps1 install
```

Or on Windows with PowerShell:
```powershell
.\bin\Debug\netcoreapp3.1\playwright.ps1 install
```

### 4. Build the Solution

```bash
dotnet build
```

## ▶️ Running Tests

### Run All Tests

```bash
dotnet test
```

### Run Tests with Specific Category

```bash
dotnet test --filter TestCategory=Smoke
dotnet test --filter TestCategory=Search
```

### Run Tests with Settings File

```bash
dotnet test --settings:PlaywrightTests/test.runsettings
```

### Run Tests in Visual Studio

1. Open `PlaywrightTestFramework.sln` in Visual Studio
2. Build the solution (Ctrl+Shift+B)
3. Open Test Explorer (Test → Test Explorer)
4. Click "Run All" or select specific tests

## 📊 Test Reports

### ExtentReports HTML Report

After test execution, an HTML report is generated at:
```
PlaywrightTests/Reports/TestReport.html
```

Open this file in a browser to view:
- Test execution summary
- Pass/Fail status for each test
- Screenshots for failed tests
- Detailed logs and timestamps
- System information

### NUnit XML Results

Test results are also available in NUnit XML format in the `TestResults` folder.

## ⚙️ Configuration

### appsettings.json

Customize test behavior by editing `PlaywrightTests/appsettings.json`:

```json
{
  "TestSettings": {
    "Browser": "chromium",           // chromium, firefox, webkit
    "Headless": true,                // true for headless, false to see browser
    "SlowMo": 50,                    // Slow down operations (ms)
    "Timeout": 30000,                // Default timeout (ms)
    "BaseUrl": "https://www.google.com",
    "ScreenshotOnFailure": true,
    "VideoRecording": true,
    "ViewportWidth": 1920,
    "ViewportHeight": 1080
  }
}
```

## 📝 Writing Tests

### Example Test Class

```csharp
using NUnit.Framework;
using PlaywrightTests.PageObjects;
using PlaywrightTests.Tests;

[TestFixture]
public class MyTests : BaseTest
{
    private MyPage _myPage;

    [SetUp]
    public new async Task Setup()
    {
        await base.Setup();
        _myPage = new MyPage(_page);
    }

    [Test]
    [Category("Smoke")]
    public async Task MyTest()
    {
        LogInfo("Starting test");
        await _myPage.NavigateAsync();
        
        // Your test logic here
        
        LogPass("Test completed successfully");
    }
}
```

### Example Page Object

```csharp
using Microsoft.Playwright;
using PlaywrightTests.PageObjects;

public class MyPage : BasePage
{
    private const string ButtonSelector = "#myButton";

    public MyPage(IPage page) : base(page) { }

    public async Task ClickButtonAsync()
    {
        await ClickAsync(ButtonSelector);
    }
}
```

## 🎯 Key Components

### BasePage.cs
Provides reusable methods for all page objects:
- `NavigateToAsync()` - Navigate to URL
- `ClickAsync()` - Click element
- `FillAsync()` - Fill input field
- `GetTextAsync()` - Get element text
- `IsVisibleAsync()` - Check element visibility
- `WaitForElementAsync()` - Wait for element
- `TakeScreenshotAsync()` - Capture screenshot

### BaseTest.cs
Handles test lifecycle:
- Browser initialization
- Page setup
- ExtentReports integration
- Screenshot on failure
- Video recording
- Cleanup and reporting

### ConfigurationManager
Centralized access to test settings from `appsettings.json`

### Logger
Console and file logging with different log levels (Info, Debug, Warning, Error)

## 🔧 Customization

### Change Browser

In `BaseTest.cs`, modify the browser launch:

```csharp
_browser = await _playwright.Firefox.LaunchAsync(...);  // Firefox
_browser = await _playwright.Webkit.LaunchAsync(...);   // WebKit
```

### Run Tests in Headed Mode

Set `Headless = false` in the launch options:

```csharp
_browser = await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
{
    Headless = false,
    SlowMo = 100
});
```

### Parallel Test Execution

Tests are configured for parallel execution. Adjust in test class:

```csharp
[Parallelizable(ParallelScope.All)]     // Run all tests in parallel
[Parallelizable(ParallelScope.Children)] // Run child tests in parallel
```

## 📌 Sample Tests Included

The framework includes 6 sample tests demonstrating:
1. ✅ Page load verification
2. ✅ Search functionality
3. ✅ Search query preservation
4. ✅ Result title extraction
5. ✅ Page title verification
6. ✅ URL navigation verification

## 🐛 Troubleshooting

### Playwright Browsers Not Installed

```bash
pwsh bin/Debug/netcoreapp3.1/playwright.ps1 install
```

### Tests Timing Out

Increase timeout in `appsettings.json` or in specific test:

```csharp
await WaitForElementAsync(selector, timeout: 60000);
```

### Screenshots Not Captured

Ensure the `Reports/Screenshots` directory exists and has write permissions.

## 📚 Additional Resources

- [Playwright for .NET Documentation](https://playwright.dev/dotnet/)
- [NUnit Documentation](https://docs.nunit.org/)
- [ExtentReports Documentation](https://www.extentreports.com/)

## 🤝 Contributing

Feel free to fork this project and customize it for your needs. Add new page objects, utilities, or enhance the reporting!

## 📄 License

This project is open source and available for educational and commercial use.

## 👤 Author

Created as a reusable test automation framework template.

---

**Happy Testing! 🎉**
