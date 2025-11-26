using NHSBloodTest.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace NHSBloodTest.PageObjects
{
    public class SignupPage
    {
        private readonly Helper helper;
        private readonly IWebDriver driver;

        // Locators
        private readonly By mrTitleRadio = By.Id("id_gender1");
        private readonly By mrsTitleRadio = By.Id("id_gender2");
        private readonly By nameInput = By.Id("name");
        private readonly By emailInput = By.Id("email");
        private readonly By passwordInput = By.Id("password");
        private readonly By daysDropdown = By.Id("days");
        private readonly By monthsDropdown = By.Id("months");
        private readonly By yearsDropdown = By.Id("years");
        private readonly By newsletterCheckbox = By.Id("newsletter");
        private readonly By specialOffersCheckbox = By.Id("optin");
        private readonly By firstNameInput = By.Id("first_name");
        private readonly By lastNameInput = By.Id("last_name");
        private readonly By companyInput = By.Id("company");
        private readonly By address1Input = By.Id("address1");
        private readonly By address2Input = By.Id("address2");
        private readonly By countryDropdown = By.Id("country");
        private readonly By stateInput = By.Id("state");
        private readonly By cityInput = By.Id("city");
        private readonly By zipcodeInput = By.Id("zipcode");
        private readonly By mobileNumberInput = By.Id("mobile_number");
        private readonly By createAccountButton = By.CssSelector("button[data-qa='create-account']");

        public SignupPage(IWebDriver driver,Helper helper)
        {
            if (driver == null) throw new ArgumentNullException(nameof(driver));
            if (helper == null) throw new ArgumentNullException(nameof(helper));

            this.driver = driver;
            this.helper = helper;
        }

        //Select title method
        public void SelectTitle(string title)
        {
            if (title.ToLower() == "mr")
                helper.Click(mrTitleRadio);
            else
                helper.Click(mrsTitleRadio);
        }

        //Enter acc Info
        public void EnterAccountInfo(string password, string day, string month, string year)
        {
            helper.ClearAndSendKeys(passwordInput, password);

            new SelectElement(helper.WaitForElementVisible(daysDropdown)).SelectByText(day);
            new SelectElement(helper.WaitForElementVisible(monthsDropdown)).SelectByText(month);
            new SelectElement(helper.WaitForElementVisible(yearsDropdown)).SelectByText(year);
        }

        //Enter address mrthod
        public void EnterAddressInfo(string firstName, string lastName, string company, string address1, string address2,
                                     string country, string state, string city, string zipcode, string mobile)
        {
            helper.ClearAndSendKeys(firstNameInput, firstName);
            helper.ClearAndSendKeys(lastNameInput, lastName);
            helper.ClearAndSendKeys(companyInput, company);
            helper.ClearAndSendKeys(address1Input, address1);
            helper.ClearAndSendKeys(address2Input, address2);

            new SelectElement(helper.WaitForElementVisible(countryDropdown)).SelectByText(country);

            helper.ClearAndSendKeys(stateInput, state);
            helper.ClearAndSendKeys(cityInput, city);
            helper.ClearAndSendKeys(zipcodeInput, zipcode);
            helper.ClearAndSendKeys(mobileNumberInput, mobile);
        }

        //Click create account button
        public void ClickCreateAccount()
        {
            helper.ScrollDown();
            helper.Click(createAccountButton);
        }
    }
}
