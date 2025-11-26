using AventStack.ExtentReports;
using NHSBloodTest.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHSBloodTest.Utilities
{
    public class OrderHelper :BaseClass
    {
        public void AcceptConsent()
        {
            pageObjectManager.HomePage.AcceptConsent();
        }

        public void AssertHomePage()
        {
            bool homeVisible = pageObjectManager.HomePage.IsHomePageDisplayed();
            Assert.IsTrue(homeVisible, "Home page is not displayed successfully");
            test.Log(Status.Pass, "Home page is displayed successfully");
        }

        public void AddProductToCart()
        {
            pageObjectManager.HomePage.AddFirstProductToCart();
            test.Log(Status.Pass, "First product added successfully");
            pageObjectManager.HomePage.ClickContinueShopping();

            pageObjectManager.NavigationPage.ClickMenuItem("Cart");
            Assert.IsTrue(pageObjectManager.CartPage.IsCartPageDisplayed(), "Cart page is not displayed");
            test.Log(Status.Pass, "Cart page is displayed");

            pageObjectManager.CartPage.ClickProceedToCheckout();
            Assert.IsTrue(pageObjectManager.CartPage.IsCheckoutModalDisplayed(), "Checkout modal is not displayed.");
            test.Log(Status.Pass, "Proceed to checkout clicked");
        }

        public void RegisterAndLogin()
        {
            pageObjectManager.CartPage.ClickRegisterLoginFromModal();
            Assert.IsTrue(pageObjectManager.LoginPage.IsSignupFormDisplayed(), "Signup form is not displayed.");
            test.Log(Status.Pass, "Signup form displayed");

            var json = getJSon();
            pageObjectManager.LoginPage.SignupNewUser(json.Getvalue("Name"), json.Getvalue("Email"));
            test.Log(Status.Pass, "Signup form completed");

            pageObjectManager.SignupPage.SelectTitle(json.Getvalue("Title"));
            pageObjectManager.SignupPage.EnterAccountInfo(
                EnvLoader.GetEnvironmentVariable("MY_PASSWORD"),
                json.Getvalue("Day"),
                json.Getvalue("Month"),
                json.Getvalue("Year")
            );

            pageObjectManager.SignupPage.EnterAddressInfo(
                json.Getvalue("Name"), json.Getvalue("Surname"), json.Getvalue("Company"),
                json.Getvalue("Address"), json.Getvalue("Address2"), json.Getvalue("Country"),
                json.Getvalue("State"), json.Getvalue("City"), json.Getvalue("Pincode"),
                json.Getvalue("Mobile")
            );
            pageObjectManager.SignupPage.ClickCreateAccount();
            test.Log(Status.Pass, "Account created successfully");
        }

        public void VerifyAccountCreation()
        {
            pageObjectManager.AccountCreatedPage.WaitForAccountCreatedMessage();
            Assert.IsTrue(pageObjectManager.AccountCreatedPage.IsAccountCreatedMessageDisplayed(), "Account created message not displayed");
            test.Log(Status.Pass, "Account created message displayed");

            pageObjectManager.AccountCreatedPage.ClickContinue();
        }

        public void VerifyLoggedInUser()
        {
            string expectedText = getJSon().Getvalue("LoggedUser");
            string actualText = pageObjectManager.NavigationPage.GetLoggedInText();
            Assert.That(actualText, Is.EqualTo(expectedText), "Logged-in user name mismatch");
            test.Log(Status.Pass, expectedText + " is displayed");
        }

        public void ProceedToCheckout()
        {
            pageObjectManager.NavigationPage.ClickMenuItem(getJSon().Getvalue("MenuCart"));
            pageObjectManager.CartPage.ClickProceedToCheckout();
            test.Log(Status.Pass, "Proceed to checkout clicked");
        }

        public void VerifyAddressDetails()
        {
            string fullName = getJSon().Getvalue("AddressTitle") + " " + getJSon().Getvalue("Name") + " " + getJSon().Getvalue("Surname");

            Assert.Multiple(() =>
            {
                Assert.That(pageObjectManager.CheckoutPage.GetDeliveryName(), Is.EqualTo(fullName));
                Assert.That(pageObjectManager.CheckoutPage.GetDeliveryAddress1(), Is.EqualTo(getJSon().Getvalue("Company")));
                Assert.That(pageObjectManager.CheckoutPage.GetDeliveryAddress2(), Is.EqualTo(getJSon().Getvalue("Address")));
                Assert.That(pageObjectManager.CheckoutPage.GetDeliveryAddress3(), Is.EqualTo(getJSon().Getvalue("Address2")));

                string deliveryCityStatePincode = pageObjectManager.CheckoutPage.GetDeliveryCityStatePostcode();
                Assert.That(deliveryCityStatePincode, Does.Contain(getJSon().Getvalue("City")));
                Assert.That(deliveryCityStatePincode, Does.Contain(getJSon().Getvalue("State")));
                Assert.That(deliveryCityStatePincode, Does.Contain(getJSon().Getvalue("Pincode")));

                Assert.That(pageObjectManager.CheckoutPage.GetDeliveryCountry(), Is.EqualTo(getJSon().Getvalue("Country")));
                Assert.That(pageObjectManager.CheckoutPage.GetDeliveryPhone(), Is.EqualTo(getJSon().Getvalue("Mobile")));
            });
        }

        public void VerifyProductDetails()
        {
            var items = pageObjectManager.CheckoutPage.GetCartProducts();
            Assert.That(items[0].Name, Is.EqualTo(getJSon().Getvalue("Product")));
            Assert.That(items[0].Price, Is.EqualTo(getJSon().Getvalue("Price")));
            test.Log(Status.Pass, $"{items[0].Name} with price {items[0].Price} verified");
        }

        public void PlaceOrderAndMakePayment()
        {
            pageObjectManager.CheckoutPage.EnterOrderComment(getJSon().Getvalue("CardName"));
            pageObjectManager.CheckoutPage.ClickPlaceOrder();
            test.Log(Status.Pass, "Order placed");

            string cardNumber = EnvLoader.GetEnvironmentVariable("MY_CARD_NUMBER");
            string cardCvv = EnvLoader.GetEnvironmentVariable("MY_CARD_CVV");
            string cardExpiry = EnvLoader.GetEnvironmentVariable("MY_CARD_EXPIRY");

            pageObjectManager.PaymentPage.EnterPaymentDetails(
                getJSon().Getvalue("CardName"),
                cardNumber,
                cardCvv,
                getJSon().Getvalue("Day"),
                cardExpiry
            );
            test.Log(Status.Pass, "Payment details entered");

            string actualMessage = pageObjectManager.PaymentPage.GetSuccessMessageBeforeReload();
            pageObjectManager.PaymentPage.ClickPayAndConfirmOrder();
            string expectedMessage = getJSon().Getvalue("PayConfirm");
            Assert.That(actualMessage, Does.Contain(expectedMessage), "Order success message is not displayed as expected!");
            test.Log(Status.Pass, actualMessage + " is displayed");

            // Order confirmation
            string orderMessage = pageObjectManager.OrderConfirmationPage.GetSuccessMessage();
            string orderExpectedMessage = getJSon().Getvalue("OrderConfirm");

            Assert.That(orderMessage, Is.EqualTo(orderExpectedMessage), "Order confirmation message mismatch!");
            test.Log(Status.Pass, orderExpectedMessage + " message is displayed");
        }


        public void DeleteAccount()
        {
            pageObjectManager.NavigationPage.ClickMenuItem(getJSon().Getvalue("MenuDelete"));
            pageObjectManager.DeleteAccountPage.WaitForAccountDeletedMessage();
            Assert.IsTrue(pageObjectManager.DeleteAccountPage.IsAccountDeletedMessageDisplayed(), "Account deletion message mismatch");
            test.Log(Status.Pass, "Account deleted successfully");
            pageObjectManager.DeleteAccountPage.ClickContinue();
        }
    }
}
