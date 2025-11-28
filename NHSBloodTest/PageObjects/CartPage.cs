using SeleniumProject.Utilities;
using OpenQA.Selenium;
using System;

namespace SeleniumProject.PageObjects
{
    public class CartPage
    {
        private readonly IWebDriver driver;
        private readonly Helper helper;

        // Locators
        private readonly By proceedToCheckout = By.CssSelector("a.btn.check_out");
        private readonly By productName = By.CssSelector(".cart_description .product-name");
        private readonly By cartTable = By.Id("cart_info_table");
        private readonly By registerLoginLink = By.CssSelector("#checkoutModal a[href='/login']");
        private readonly By checkoutModal = By.CssSelector("#checkoutModal");

        // Constructor
        public CartPage(IWebDriver driver,Helper helper)
        {
            if (driver == null) throw new ArgumentNullException(nameof(driver));
            if (helper == null) throw new ArgumentNullException(nameof(helper));

            this.driver = driver;
            this.helper = helper;
        }

        // Actions / Methods

        public void ClickProceedToCheckout()
        {
            helper.Click(proceedToCheckout);
        }

        public string GetProductName()
        {
            return helper.WaitForElementVisible(productName).Text.Trim();
        }

        public bool IsCartPageDisplayed()
        {
            return helper.WaitForElementVisible(cartTable).Displayed;
        }

        public void ClickRegisterLoginFromModal()
        {
            helper.Click(registerLoginLink);
        }

        public bool IsCheckoutModalDisplayed()
        {
            return helper.WaitForElementVisible(checkoutModal).Displayed;
        }
    }
}
