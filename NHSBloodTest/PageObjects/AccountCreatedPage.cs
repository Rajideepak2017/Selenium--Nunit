using NHSBloodTest.Utilities;
using OpenQA.Selenium;

namespace NHSBloodTest.PageObjects
{
    public class AccountCreatedPage
    {
        private readonly IWebDriver driver;
        private readonly Helper helper;


        // Locators
        private readonly By accountCreatedMessage = By.CssSelector("h2[data-qa='account-created']");
        private readonly By continueButton = By.CssSelector("a[data-qa='continue-button']");

        public AccountCreatedPage(IWebDriver driver,Helper helper)
        {
            if (driver == null) throw new ArgumentNullException(nameof(driver));
            if (helper == null) throw new ArgumentNullException(nameof(helper));

            this.driver = driver; 
            this.helper = helper;
        }

        // Verify account created message is displayed and correct
        public bool IsAccountCreatedMessageDisplayed()
        {
            string message = helper.GetText(accountCreatedMessage);
            return message.Equals("ACCOUNT CREATED!", StringComparison.OrdinalIgnoreCase);
        }

        // Wait explicitly for the message 
        public void WaitForAccountCreatedMessage()
        {
            helper.WaitForElementVisible(accountCreatedMessage);
        }

        // Click Continue button
        public void ClickContinue()
        {
            helper.Click(continueButton);
        }
    }
}
