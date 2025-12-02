using Microsoft.Playwright;
using System;
using System.Threading.Tasks;

namespace PlaywrightTests.PageObjects
{
    /// <summary>
    /// Base page class containing common functionality for all page objects
    /// </summary>
    public class BasePage
    {
        protected readonly IPage _page;
        protected readonly int DefaultTimeout = 30000;

        public BasePage(IPage page)
        {
            _page = page;
        }

        /// <summary>
        /// Navigate to a specific URL
        /// </summary>
        public async Task NavigateToAsync(string url)
        {
            await _page.GotoAsync(url, new PageGotoOptions { WaitUntil = WaitUntilState.NetworkIdle });
        }

        /// <summary>
        /// Click an element using selector
        /// </summary>
        public async Task ClickAsync(string selector)
        {
            await _page.Locator(selector).ClickAsync();
        }

        /// <summary>
        /// Fill text in an input field
        /// </summary>
        public async Task FillAsync(string selector, string text)
        {
            await _page.Locator(selector).FillAsync(text);
        }

        /// <summary>
        /// Get text content of an element
        /// </summary>
        public async Task<string> GetTextAsync(string selector)
        {
            var locator = _page.Locator(selector);
            return await locator.TextContentAsync() ?? string.Empty;
        }

        /// <summary>
        /// Check if element is visible
        /// </summary>
        public async Task<bool> IsVisibleAsync(string selector)
        {
            var locator = _page.Locator(selector);
            return await locator.IsVisibleAsync();
        }

        /// <summary>
        /// Wait for element to be visible
        /// </summary>
        public async Task WaitForElementAsync(string selector, int timeout = 30000)
        {
            var locator = _page.Locator(selector);
            await locator.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = timeout });
        }

        /// <summary>
        /// Get page title
        /// </summary>
        public async Task<string> GetPageTitleAsync()
        {
            return await _page.TitleAsync();
        }

        /// <summary>
        /// Take screenshot
        /// </summary>
        public async Task TakeScreenshotAsync(string fileName)
        {
            await _page.ScreenshotAsync(new PageScreenshotOptions 
            { 
                Path = $"Reports/Screenshots/{fileName}_{DateTime.Now:yyyyMMdd_HHmmss}.png",
                FullPage = true
            });
        }

        /// <summary>
        /// Select option from dropdown by value
        /// </summary>
        public async Task SelectOptionAsync(string selector, string value)
        {
            await _page.SelectOptionAsync(selector, value);
        }

        /// <summary>
        /// Check checkbox or radio button
        /// </summary>
        public async Task CheckAsync(string selector)
        {
            var locator = _page.Locator(selector);
            await locator.CheckAsync();
        }

        /// <summary>
        /// Get current URL
        /// </summary>
        public string GetCurrentUrl()
        {
            return _page.Url;
        }
    }
}
