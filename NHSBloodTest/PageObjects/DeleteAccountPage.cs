using SeleniumProject.Utilities;
using OpenQA.Selenium;
using System;

namespace SeleniumProject.PageObjects
{
    public class DeleteAccountPage
    {
        private readonly Helper helper;
        private readonly IWebDriver driver;
        // Locators
        private readonly By accountDeletedMessage = By.CssSelector(".title.text-center");
        private readonly By continueButton = By.CssSelector(".btn.btn-primary");

        // Constructor
        public DeleteAccountPage(IWebDriver driver,Helper helper)
        {
            if (driver == null) throw new ArgumentNullException(nameof(driver));
            if (helper == null) throw new ArgumentNullException(nameof(helper));

            this.driver = driver;
            this.helper = helper;
        }

        // Get success message text
        public string GetAccountDeletedMessage()
        {
            return helper.GetText(accountDeletedMessage);
        }

        // Wait for the success message to be visible
        public void WaitForAccountDeletedMessage()
        {
            helper.WaitForElementVisible(accountDeletedMessage);
        }

        // Verify if account deleted message is displayed and correct
        public bool IsAccountDeletedMessageDisplayed()
        {
            string message = helper.GetText(accountDeletedMessage);
            if (string.IsNullOrEmpty(message))
                return false;

            return message.Trim().Equals("ACCOUNT DELETED!", StringComparison.OrdinalIgnoreCase);
        }

        // Click Continue button
        public void ClickContinue()
        {
            helper.Click(continueButton);
        }
    }
}
