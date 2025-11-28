using SeleniumProject.Utilities;
using OpenQA.Selenium;
using System;

namespace SeleniumProject.PageObjects
{
    public class HomePage
    {
        private readonly Helper helper;
        private readonly IWebDriver driver;

        // Locators
        private readonly By signupLoginLink = By.LinkText("Signup / Login");
        private readonly By featuresItemsTitle = By.XPath("//h2[text()='Features Items']");
        private readonly By firstProductContainer = By.XPath("(//div[@class='product-image-wrapper'])[1]");
        private readonly By firstProductName = By.XPath("(//div[@class='product-image-wrapper'])[1]//div[contains(@class,'productinfo')]//p[1]");
        private readonly By firstProductPrice = By.XPath("(//div[@class='product-image-wrapper'])[1]//div[contains(@class,'productinfo')]//h2");
        private readonly By firstProductAddToCartButton = By.XPath("(//a[text()='Add to cart'])[1]");
        private readonly By continueShoppingButton = By.CssSelector("#cartModal .close-modal");
        private readonly By consentButton = By.CssSelector("button.fc-cta-consent");

        // Constructor
        public HomePage(IWebDriver driver,Helper helper)
        {
            if (driver == null) throw new ArgumentNullException(nameof(driver));
            if (helper == null) throw new ArgumentNullException(nameof(helper));

            this.driver = driver;
            this.helper = helper;
        }

        // Actions / Methods

        public bool IsHomePageDisplayed()
        {
            return helper.WaitForElementVisible(featuresItemsTitle).Displayed;
        }

        public void GoToSignupLogin()
        {
            helper.Click(signupLoginLink);
        }

        public void AddFirstProductToCart()
        {
            helper.Click(firstProductAddToCartButton);
        }

        public string GetFirstProductName()
        {
            return helper.GetText(firstProductName);
        }

        public bool IsFirstProductVisible()
        {
            return helper.WaitForElementVisible(firstProductContainer).Displayed;
        }

        public void ClickContinueShopping()
        {
            helper.WaitForElementVisible(continueShoppingButton);
            helper.Click(continueShoppingButton);
        }

        public string GetProductPrice()
        {
            return helper.GetText(firstProductPrice);
        }

        public void AcceptConsent()
        {
            try
            {
                if (helper.WaitForElementVisible(consentButton, 5).Displayed)
                {
                    helper.Click(consentButton);
                }
            }
            catch (WebDriverTimeoutException)
            {
                TestContext.WriteLine("Consent button not found or already handled.");
            }
            catch (Exception ex)
            {
                TestContext.WriteLine("Unexpected error while handling consent button: " + ex.Message);
            }
        }
    }
}
