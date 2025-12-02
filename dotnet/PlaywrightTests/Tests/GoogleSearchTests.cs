using NUnit.Framework;
using PlaywrightTests.PageObjects;
using System.Linq;
using System.Threading.Tasks;

namespace PlaywrightTests.Tests
{
    /// <summary>
    /// Sample test class demonstrating the test framework usage
    /// </summary>
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    public class GoogleSearchTests : BaseTest
    {
        private GoogleHomePage? _googleHomePage;
        private GoogleSearchResultsPage? _googleSearchResultsPage;

        [SetUp]
        public new async Task Setup()
        {
            await base.Setup();
            _googleHomePage = new GoogleHomePage(_page!);
            _googleSearchResultsPage = new GoogleSearchResultsPage(_page!);
        }

        [Test]
        [Category("Smoke")]
        [Description("Verify Google home page loads successfully")]
        public async Task Test01_VerifyGoogleHomePageLoads()
        {
            LogInfo("Navigating to Google home page");
            await _googleHomePage!.NavigateAsync();

            LogInfo("Verifying search box is visible");
            var isSearchBoxVisible = await _googleHomePage.IsSearchBoxVisibleAsync();
            Assert.IsTrue(isSearchBoxVisible, "Search box should be visible on Google home page");

            LogPass("Google home page loaded successfully");
        }

        [Test]
        [Category("Smoke")]
        [Category("Search")]
        [Description("Verify search functionality returns results")]
        public async Task Test02_VerifySearchFunctionality()
        {
            var searchQuery = "Playwright automation";

            LogInfo($"Navigating to Google home page");
            await _googleHomePage!.NavigateAsync();

            LogInfo($"Searching for: {searchQuery}");
            await _googleHomePage.SearchForAsync(searchQuery);

            LogInfo("Waiting for search results");
            await Task.Delay(2000); // Wait for results to load

            LogInfo("Verifying search results are displayed");
            var areResultsDisplayed = await _googleSearchResultsPage!.AreResultsDisplayedAsync();
            Assert.IsTrue(areResultsDisplayed, "Search results should be displayed");

            var resultsCount = await _googleSearchResultsPage.GetSearchResultsCountAsync();
            LogInfo($"Number of search results: {resultsCount}");
            Assert.Greater(resultsCount, 0, "At least one search result should be displayed");

            LogPass($"Search functionality working correctly. Found {resultsCount} results");
        }

        [Test]
        [Category("Search")]
        [Description("Verify search query is preserved in search box")]
        public async Task Test03_VerifySearchQueryPreserved()
        {
            var searchQuery = "Microsoft Playwright";

            LogInfo("Navigating to Google home page");
            await _googleHomePage!.NavigateAsync();

            LogInfo($"Searching for: {searchQuery}");
            await _googleHomePage.SearchForAsync(searchQuery);

            await Task.Delay(2000);

            LogInfo("Verifying search query in search box");
            var displayedQuery = await _googleSearchResultsPage!.GetSearchQueryAsync();
            Assert.AreEqual(searchQuery, displayedQuery, "Search query should be preserved in search box");

            LogPass("Search query correctly preserved");
        }

        [Test]
        [Category("Search")]
        [Description("Verify search results contain relevant titles")]
        public async Task Test04_VerifySearchResultTitles()
        {
            var searchQuery = "NUnit testing framework";

            LogInfo("Navigating to Google home page");
            await _googleHomePage!.NavigateAsync();

            LogInfo($"Searching for: {searchQuery}");
            await _googleHomePage.SearchForAsync(searchQuery);

            await Task.Delay(2000);

            LogInfo("Getting search result titles");
            var titles = await _googleSearchResultsPage!.GetSearchResultTitlesAsync();
            Assert.IsNotEmpty(titles, "Search results should have titles");

            LogInfo($"Found {titles.Count} result titles");
            foreach (var title in titles.Take(5))
            {
                LogInfo($"Result title: {title}");
            }

            LogPass($"Successfully retrieved {titles.Count} search result titles");
        }

        [Test]
        [Category("UI")]
        [Description("Verify page title on Google home page")]
        public async Task Test05_VerifyPageTitle()
        {
            LogInfo("Navigating to Google home page");
            await _googleHomePage!.NavigateAsync();

            LogInfo("Getting page title");
            var title = await _googleHomePage.GetPageTitleAsync();
            LogInfo($"Page title: {title}");

            Assert.IsTrue(title.Contains("Google"), "Page title should contain 'Google'");

            LogPass("Page title verification successful");
        }

        [Test]
        [Category("Smoke")]
        [Description("Verify URL after navigation")]
        public async Task Test06_VerifyUrlAfterNavigation()
        {
            LogInfo("Navigating to Google home page");
            await _googleHomePage!.NavigateAsync();

            LogInfo("Getting current URL");
            var currentUrl = _googleHomePage.GetCurrentUrl();
            LogInfo($"Current URL: {currentUrl}");

            Assert.IsTrue(currentUrl.Contains("google.com"), "URL should contain google.com");

            LogPass("URL verification successful");
        }
    }
}
