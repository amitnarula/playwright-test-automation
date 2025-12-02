# Quick Start Guide

## Step 1: Install Playwright Browsers

After building the project for the first time, you need to install Playwright browsers.

### On Windows PowerShell:
```powershell
cd PlaywrightTests
.\bin\Debug\netcoreapp3.1\playwright.ps1 install
```

If you get an execution policy error, run:
```powershell
Set-ExecutionPolicy -ExecutionPolicy RemoteSigned -Scope CurrentUser
```

## Step 2: Build the Project

```bash
dotnet build
```

## Step 3: Run Your First Test

```bash
dotnet test
```

## Step 4: View Test Report

After test execution, open the HTML report:
```
Reports/TestReport.html
```

## Common Commands

### Run specific test category:
```bash
dotnet test --filter TestCategory=Smoke
```

### Run single test:
```bash
dotnet test --filter "FullyQualifiedName~Test01_VerifyGoogleHomePageLoads"
```

### Run in headed mode:
Edit `appsettings.json` and set `"Headless": false`

### Enable verbose logging:
```bash
dotnet test --logger "console;verbosity=detailed"
```

## Troubleshooting

### "Playwright not found" error
Install browsers: `.\bin\Debug\netcoreapp3.1\playwright.ps1 install`

### Tests are slow
- Set `"SlowMo": 0` in appsettings.json
- Run tests in headless mode

### Screenshots not working
Ensure `Reports/Screenshots` directory exists with write permissions

## Next Steps

1. Review the sample tests in `Tests/GoogleSearchTests.cs`
2. Create your own page objects in `PageObjects/`
3. Add your test cases in `Tests/`
4. Customize settings in `appsettings.json`
5. Check the HTML report after each run

Happy Testing! 🚀
