using SeleniumProject.Utilities;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;

namespace SeleniumProject.PageObjects
{
    public class NavigationPage
    {
        private readonly Helper helper;
        private readonly IWebDriver driver;

        // Locators
        private readonly By menuLinks = By.XPath("//ul[@class='nav navbar-nav']//a");
        private readonly By loggedLink = By.XPath("//a[i[@class='fa fa-user']]");

        // Constructor
        public NavigationPage(IWebDriver driver,Helper helper)
        {
            if (driver == null) throw new ArgumentNullException(nameof(driver));
            if (helper == null) throw new ArgumentNullException(nameof(helper));

            this.driver = driver;
            this.helper = helper;
        }

        //Get text for user login 
        public string GetLoggedInText()
        {
            return helper.WaitForElementVisible(loggedLink).Text.Trim();
        }
        //Loop for selecting menu items dynamically
        public void ClickMenuItem(string menuText)
        {
            driver.Navigate().Refresh();
            var items = helper.FindElements(menuLinks);

            foreach (var item in items)
            {
                if (item != null && item.Text.Trim().Contains(menuText))
                {
                    item.Click();
                    break;
                }
            }
        }
    }
}
