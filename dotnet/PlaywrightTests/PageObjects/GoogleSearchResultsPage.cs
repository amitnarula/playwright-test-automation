using Microsoft.Playwright;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlaywrightTests.PageObjects
{
    /// <summary>
    /// Page Object for Google Search Results Page
    /// </summary>
    public class GoogleSearchResultsPage : BasePage
    {
        // Locators
        private const string SearchResultsSelector = "#search .g";
        private const string SearchBoxSelector = "textarea[name='q']";
        private const string ResultStatsSelector = "#result-stats";

        public GoogleSearchResultsPage(IPage page) : base(page)
        {
        }

        /// <summary>
        /// Get count of search results displayed
        /// </summary>
        public async Task<int> GetSearchResultsCountAsync()
        {
            await WaitForElementAsync(SearchResultsSelector);
            var locator = _page.Locator(SearchResultsSelector);
            return await locator.CountAsync();
        }

        /// <summary>
        /// Check if results are displayed
        /// </summary>
        public async Task<bool> AreResultsDisplayedAsync()
        {
            try
            {
                await WaitForElementAsync(SearchResultsSelector, 10000);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Get search query from search box
        /// </summary>
        public async Task<string> GetSearchQueryAsync()
        {
            var locator = _page.Locator(SearchBoxSelector);
            return await locator.InputValueAsync();
        }

        /// <summary>
        /// Get all search result titles
        /// </summary>
        public async Task<List<string>> GetSearchResultTitlesAsync()
        {
            await WaitForElementAsync(SearchResultsSelector);
            var locator = _page.Locator(SearchResultsSelector + " h3");
            var count = await locator.CountAsync();
            var titles = new List<string>();

            for (int i = 0; i < count; i++)
            {
                var title = await locator.Nth(i).TextContentAsync();
                if (!string.IsNullOrEmpty(title))
                {
                    titles.Add(title);
                }
            }

            return titles;
        }

        /// <summary>
        /// Click on a search result by index (0-based)
        /// </summary>
        public async Task ClickSearchResultAsync(int index)
        {
            var locator = _page.Locator(SearchResultsSelector);
            await locator.Nth(index).ClickAsync();
        }
    }
}
