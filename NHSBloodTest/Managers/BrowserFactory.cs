using AventStack.ExtentReports;
using NHSBloodTest.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;

namespace NHSBloodTest.Managers
{
    public static class BrowserFactory
    {
        public static IWebDriver CreateDriver(string browser)
        {
            switch (browser.ToLower())
            {
                case "chrome":
                    new DriverManager().SetUpDriver(new ChromeConfig(), VersionResolveStrategy.MatchingBrowser);

                    ChromeOptions chromeOptions = new ChromeOptions();
                    chromeOptions.AddArgument("--disable-extensions");
                    chromeOptions.AddArgument("--disable-popup-blocking");
                    chromeOptions.AddArgument("--disable-notifications");
                    chromeOptions.AddArgument("--disable-infobars");
                    chromeOptions.AddArgument("--disable-plugins");
                    chromeOptions.AddArgument("--start-maximized");
                    chromeOptions.AddExcludedArgument("enable-automation");
                    chromeOptions.AddArgument("--disable-save-password-bubble");
                    chromeOptions.AddUserProfilePreference("credentials_enable_service", false);
                    chromeOptions.AddUserProfilePreference("profile.password_manager_enabled", false);
                    chromeOptions.AddArgument("--incognito");
                    chromeOptions.AddAdditionalOption("useAutomationExtension", false);
                    chromeOptions.AddArgument("--disable-web-security");
                    chromeOptions.AddArgument("--allow-running-insecure-content");

                    return new ChromeDriver(chromeOptions);

                case "edge":
                    new DriverManager().SetUpDriver(new EdgeConfig(), VersionResolveStrategy.MatchingBrowser);

                    EdgeOptions edgeOptions = new EdgeOptions();
                    edgeOptions.AddArgument("--disable-extensions");
                    edgeOptions.AddArgument("--disable-popup-blocking");
                    edgeOptions.AddArgument("--disable-notifications");
                    edgeOptions.AddArgument("--start-maximized");

                    return new EdgeDriver(edgeOptions);

                default:
                    throw new ArgumentException($"Unsupported browser: {browser}");
            }
        }
    }
}
