using SeleniumProject.PageObjects;

using SeleniumProject.Utilities;
using OpenQA.Selenium;
using System;

namespace SeleniumProject.Managers
{
    public class PageObjectManager
    {
        private IWebDriver driver;
        private Helper helper;

        // Private instances
        private HomePage homePage;
        private LoginPage loginPage;
        private SignupPage signupPage;
        private CartPage cartPage;
        private CheckoutPage checkoutPage;
        private OrderConfirmationPage orderConfirmationPage;
        private DeleteAccountPage deleteAccountPage;
        private AccountCreatedPage accountCreatedPage;
        private PaymentPage paymentPage;
        private NavigationPage navigationPage;

      

        // Constructor
        public PageObjectManager(IWebDriver driver, Helper helper)
        {
            if (driver == null)
            {
                throw new ArgumentNullException(nameof(driver));
            }
            this.driver = driver;

            if (helper == null)
            {
                throw new ArgumentNullException(nameof(helper));
            }
            this.helper = helper;
        }

        // Properties

        public HomePage HomePage
        {
            get
            {
                if (homePage == null) homePage = new HomePage(driver, helper);
                return homePage;
            }
        }

        public PaymentPage PaymentPage
        {
            get
            {
                if (paymentPage == null) paymentPage = new PaymentPage(driver, helper);
                return paymentPage;
            }
        }

        public LoginPage LoginPage
        {
            get
            {
                if (loginPage == null) loginPage = new LoginPage(driver, helper);
                return loginPage;
            }
        }

        public SignupPage SignupPage
        {
            get
            {
                if (signupPage == null) signupPage = new SignupPage(driver, helper);
                return signupPage;
            }
        }

        public CartPage CartPage
        {
            get
            {
                if (cartPage == null) cartPage = new CartPage(driver, helper);
                return cartPage;
            }
        }

        public CheckoutPage CheckoutPage
        {
            get
            {
                if (checkoutPage == null) checkoutPage = new CheckoutPage(driver, helper);
                return checkoutPage;
            }
        }

        public OrderConfirmationPage OrderConfirmationPage
        {
            get
            {
                if (orderConfirmationPage == null) orderConfirmationPage = new OrderConfirmationPage(driver, helper);
                return orderConfirmationPage;
            }
        }

        public DeleteAccountPage DeleteAccountPage
        {
            get
            {
                if (deleteAccountPage == null) deleteAccountPage = new DeleteAccountPage(driver, helper);
                return deleteAccountPage;
            }
        }

        public AccountCreatedPage AccountCreatedPage
        {
            get
            {
                if (accountCreatedPage == null) accountCreatedPage = new AccountCreatedPage(driver, helper);
                return accountCreatedPage;
            }
        }

        public NavigationPage NavigationPage
        {
            get
            {
                if (navigationPage == null) navigationPage = new NavigationPage(driver, helper);
                return navigationPage;
            }
        }

      
    }
}
