using AventStack.ExtentReports;
using AventStack.ExtentReports.Model;
using DotNetEnv;
using NHSBloodTest.Managers;
using NHSBloodTest.PageObjects;
using NHSBloodTest.Utilities;
using NUnit.Framework;
using OpenQA.Selenium;
using System;

namespace NHSBloodTest.Tests
{
    [TestFixture]
    
    public class PlaceOrderTest : OrderHelper
    {
        [Test]
        [Retry(1)]
        public void PlaceOrder()
        {
            // Accepting consent and verifying home page
            AcceptConsent();
            AssertHomePage();

            // Adding product to cart and navigating to checkout
            AddProductToCart();

            // Register and login
            RegisterAndLogin();

            // Verifying account creation and logged-in user
            VerifyAccountCreation();
            VerifyLoggedInUser();

            // Verify and proceed with checkout
            ProceedToCheckout();

            // Verify address and product details
            VerifyAddressDetails();
            VerifyProductDetails();

            // Entering order comment and placing the order and confirm order success
            PlaceOrderAndMakePayment();

            // Delete account after order completion
            DeleteAccount();
        }

    }
}
