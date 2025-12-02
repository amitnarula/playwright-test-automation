using Microsoft.Playwright;
using System.Threading.Tasks;

namespace PlaywrightTests.PageObjects
{
    /// <summary>
    /// Page Object for Google Home Page
    /// </summary>
    public class GoogleHomePage : BasePage
    {
        // Locators
        private const string SearchBoxSelector = "textarea[name='q']";
        private const string SearchButtonSelector = "input[name='btnK']";
        private const string LuckyButtonSelector = "input[name='btnI']";

        public GoogleHomePage(IPage page) : base(page)
        {
        }

        /// <summary>
        /// Navigate to Google home page
        /// </summary>
        public async Task NavigateAsync()
        {
            await NavigateToAsync("https://www.google.com");
        }

        /// <summary>
        /// Search for a query
        /// </summary>
        public async Task SearchForAsync(string query)
        {
            await FillAsync(SearchBoxSelector, query);
            await _page.Keyboard.PressAsync("Enter");
        }

        /// <summary>
        /// Check if search box is visible
        /// </summary>
        public async Task<bool> IsSearchBoxVisibleAsync()
        {
            return await IsVisibleAsync(SearchBoxSelector);
        }

        /// <summary>
        /// Get search box placeholder or value
        /// </summary>
        public async Task<string> GetSearchBoxValueAsync()
        {
            var locator = _page.Locator(SearchBoxSelector);
            return await locator.InputValueAsync();
        }
    }
}
