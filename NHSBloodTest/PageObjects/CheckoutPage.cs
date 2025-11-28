using SeleniumProject.Utilities;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;

namespace SeleniumProject.PageObjects
{
    public class CheckoutPage
    {
        private readonly IWebDriver driver;
        private readonly Helper helper;

        // Locators
        private readonly By productRows = By.CssSelector("tbody tr[id^='product-']");
        private readonly By productNames = By.CssSelector("tbody tr[id^='product-'] td.cart_description h4 a");
        private readonly By productPrices = By.CssSelector("tbody tr[id^='product-'] td.cart_price p");
        private readonly By commentTextArea = By.Name("message");
        private readonly By placeOrderButton = By.CssSelector(".btn.btn-default.check_out");

        // Delivery Address
        private readonly By deliveryName = By.CssSelector("#address_delivery li.address_firstname");
        private readonly By deliveryAddress1 = By.CssSelector("#address_delivery li:nth-of-type(3)");
        private readonly By deliveryAddress2 = By.CssSelector("#address_delivery li:nth-of-type(4)");
        private readonly By deliveryAddress3 = By.CssSelector("#address_delivery li:nth-of-type(5)");
        private readonly By deliveryCityStatePostcode = By.CssSelector("#address_delivery li.address_city");
        private readonly By deliveryCountry = By.CssSelector("#address_delivery li.address_country_name");
        private readonly By deliveryPhone = By.CssSelector("#address_delivery li.address_phone");

        // Billing Address
        private readonly By billingName = By.CssSelector("#address_invoice li.address_firstname");
        private readonly By billingAddress1 = By.CssSelector("#address_invoice li:nth-of-type(3)");
        private readonly By billingAddress2 = By.CssSelector("#address_invoice li:nth-of-type(4)");
        private readonly By billingAddress3 = By.CssSelector("#address_invoice li:nth-of-type(5)");
        private readonly By billingCityStatePostcode = By.CssSelector("#address_invoice li.address_city");
        private readonly By billingCountry = By.CssSelector("#address_invoice li.address_country_name");
        private readonly By billingPhone = By.CssSelector("#address_invoice li.address_phone");

        // Constructor
        public CheckoutPage(IWebDriver driver,Helper helper)
        {
            if (driver == null) throw new ArgumentNullException(nameof(driver));
            if (helper == null) throw new ArgumentNullException(nameof(helper));

            this.driver = driver;
            this.helper = helper;
        }

        // Cart products
        public List<(string Name, string Price)> GetCartProducts()
        {
            var names = helper.FindElements(productNames);
            var prices = helper.FindElements(productPrices);
            var products = new List<(string Name, string Price)>();

            for (int i = 0; i < names.Count; i++)
            {
                products.Add((names[i].Text.Trim(), prices[i].Text.Trim()));
            }

            return products;
        }

        // Comment
        public void EnterOrderComment(string comment)
        {
            helper.SendKeys(commentTextArea, comment);
        }

        // Place order
        public void ClickPlaceOrder()
        {
            helper.Click(placeOrderButton);
        }

        // Delivery address getters
        public string GetDeliveryName()
        {
            return helper.GetText(deliveryName);
        }

        public string GetDeliveryAddress1()
        {
            return helper.GetText(deliveryAddress1);
        }

        public string GetDeliveryAddress2()
        {
            return helper.GetText(deliveryAddress2);
        }

        public string GetDeliveryAddress3()
        {
            return helper.GetText(deliveryAddress3);
        }

        public string GetDeliveryCityStatePostcode()
        {
            return helper.GetText(deliveryCityStatePostcode);
        }

        public string GetDeliveryCountry()
        {
            return helper.GetText(deliveryCountry);
        }

        public string GetDeliveryPhone()
        {
            return helper.GetText(deliveryPhone);
        }


        // Billing address getters
        public string GetBillingName()
        {
            return helper.GetText(billingName);
        }

        public string GetBillingAddress1()
        {
            return helper.GetText(billingAddress1);
        }

        public string GetBillingAddress2()
        {
            return helper.GetText(billingAddress2);
        }

        public string GetBillingAddress3()
        {
            return helper.GetText(billingAddress3);
        }

        public string GetBillingCityStatePostcode()
        {
            return helper.GetText(billingCityStatePostcode);
        }

        public string GetBillingCountry()
        {
            return helper.GetText(billingCountry);
        }

        public string GetBillingPhone()
        {
            return helper.GetText(billingPhone);
        }

    }
}
