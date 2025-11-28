using SeleniumProject.Utilities;
using OpenQA.Selenium;
using System;

namespace SeleniumProject.PageObjects
{
    public class PaymentPage
    {
        private readonly Helper helper;
        private readonly IWebDriver driver;

        // Locators
        private readonly By nameOnCardInput = By.Name("name_on_card");
        private readonly By cardNumberInput = By.Name("card_number");
        private readonly By cvcInput = By.Name("cvc");
        private readonly By expiryMonthInput = By.Name("expiry_month");
        private readonly By expiryYearInput = By.Name("expiry_year");
        private readonly By payAndConfirmOrderButton = By.Id("submit");
        private readonly By successMessage = By.CssSelector("#success_message .alert-success");
        // Success message selector for JS
        private readonly string successMessageSelector = "#success_message .alert-success";

        // Constructor
        public PaymentPage(IWebDriver driver,Helper helper)
        {
            if (driver == null) throw new ArgumentNullException(nameof(driver));
            if (helper == null) throw new ArgumentNullException(nameof(helper));

            this.driver = driver;
            this.helper = helper;
        }

        // Enter payment details
        public void EnterPaymentDetails(string nameOnCard, string cardNumber, string cvc, string expiryMonth, string expiryYear)
        {
            helper.ClearAndSendKeys(nameOnCardInput, nameOnCard);
            helper.ClearAndSendKeys(cardNumberInput, cardNumber);
            helper.ClearAndSendKeys(cvcInput, cvc);
            helper.ClearAndSendKeys(expiryMonthInput, expiryMonth);
            helper.ClearAndSendKeys(expiryYearInput, expiryYear);
        }

        // Click Pay and Confirm Order button
        public void ClickPayAndConfirmOrder()
        {
            helper.Click(payAndConfirmOrderButton);
        }

        // Get Success Message
        public string GetSuccessMessage()
        {
            return helper.WaitForElementVisible(successMessage).Text.Trim();
        }

        public string GetSuccessMessageBeforeReload()
        {
            try
            {
                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                string script = $@"
                    var msg = document.querySelector('{successMessageSelector}');
                    return msg ? msg.innerText.trim() : null;";
                var message = js.ExecuteScript(script) as string;
                return message ?? "Message not yet visible";
            }
            catch
            {
                return "Message not found";
            }
        }
    }
}
