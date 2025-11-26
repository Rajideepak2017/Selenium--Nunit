using NHSBloodTest.Utilities;
using OpenQA.Selenium;
using System;

namespace NHSBloodTest.PageObjects
{
    public class LoginPage
    {
        private readonly IWebDriver driver;
        private readonly Helper helper;

        // Locators
        private readonly By nameInput = By.CssSelector("input[data-qa='signup-name']");
        private readonly By emailInput = By.CssSelector("input[data-qa='signup-email']");
        private readonly By signupButton = By.CssSelector("button[data-qa='signup-button']");
        private readonly By signupFormContainer = By.CssSelector("div.signup-form");

        // Constructor
        public LoginPage(IWebDriver driver,Helper helper)
        {
            if (driver == null) throw new ArgumentNullException(nameof(driver));
            if (helper == null) throw new ArgumentNullException(nameof(helper));

            this.driver = driver;
            this.helper = helper;
        }

        // Actions / Methods
        public void SignupNewUser(string name, string email)
        {
            helper.SendKeys(nameInput, name);
            helper.SendKeys(emailInput, email);
            helper.Click(signupButton);
        }

        public bool IsSignupFormDisplayed()
        {
            return helper.WaitForElementVisible(signupFormContainer).Displayed;
        }
    }
}
