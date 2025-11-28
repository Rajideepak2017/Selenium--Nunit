using SeleniumProject.Utilities;
using OpenQA.Selenium;
using System;

namespace SeleniumProject.PageObjects
{
    public class OrderConfirmationPage
    {
        private readonly Helper helper;
        private readonly IWebDriver driver;

        // Locator
        private readonly By successMessage = By.XPath("//h2[@data-qa='order-placed']/following-sibling::p");

        // Constructor
        public OrderConfirmationPage(IWebDriver driver, Helper helper)
        {
            if (driver == null) throw new ArgumentNullException(nameof(driver));
            if (helper == null) throw new ArgumentNullException(nameof(helper));

            this.driver = driver;
            this.helper = helper;
        }

        // Success message text check
        public string GetSuccessMessage()
        {
            return helper.WaitForElementVisible(successMessage).Text.Trim();
        }
    }
}
