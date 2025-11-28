using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.ObjectModel;

namespace SeleniumProject.Utilities
{
    public class Helper
    {
        private IWebDriver driver;
        private WebDriverWait wait;
        private ReadOnlyCollection<IWebElement> elements;

        public Helper(IWebDriver driver, int timeoutInSeconds = 10)
        {
            ArgumentNullException.ThrowIfNull(driver);
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
        }

        // Wait and click using IWebElement
        public void WaitAndClick(IWebElement element, int timeoutInSeconds = 10)
        {
            WebDriverWait waitClick = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
            waitClick.Until(ExpectedConditions.ElementToBeClickable(element));
            element.Click();
        }

        // Wait until element is clickable
        public void Wait(IWebElement element, int timeoutInSeconds = 10)
        {
            WebDriverWait waitElement = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
            waitElement.Until(ExpectedConditions.ElementToBeClickable(element));
        }

        // Scroll a specific element into view
        public void ScrollToElement(IWebElement element)
        {
            if (element == null)
                throw new NullReferenceException("Element is null. Check locator or page load.");

            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView({block:'center'});", element);
        }

        // Scroll down by 500 pixels 
        public void ScrollDown(int pixels = 500)
        {
            ((IJavaScriptExecutor)driver).ExecuteScript($"window.scrollBy(0, {pixels});");
        }

        // Wait until element located by By is visible
        public IWebElement WaitForElementVisible(By locator, int timeoutInSeconds = 10)
        {
            WebDriverWait waitElement = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
            return waitElement.Until(ExpectedConditions.ElementIsVisible(locator));
        }

        // Click element 
        public void Click(By locator)
        {
            var element = WaitForElementVisible(locator);
            element.Click();
        }

        // Send text to element 
        public void SendKeys(By locator, string text)
        {
            var element = WaitForElementVisible(locator);
            element.Clear();
            element.SendKeys(text);
        }

        // Get text of element 
        public string GetText(By locator)
        {
            return WaitForElementVisible(locator).Text.Trim();
        }

        //findElemnts
        public IList<IWebElement> FindElements(By locator)
        {
            return wait.Until(driver =>
            {
                var elements = driver.FindElements(locator);
                if (elements.Count > 0)
                {
                    return elements;
                }
                else
                {
                    return null;
                }
            });
        
        }

        //Clear and send keys
        public void ClearAndSendKeys(By locator, string text)
        {
            var element = WaitForElementVisible(locator);
            element.Clear();
            element.SendKeys(text);
        }
    }
}
