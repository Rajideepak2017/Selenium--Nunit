using AngleSharp.Dom;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Model;
using AventStack.ExtentReports.Reporter;
using Newtonsoft.Json;
using NHSBloodTest.Managers;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.BiDi.BrowsingContext;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;

namespace NHSBloodTest.Utilities
{
    public class BaseClass
    {
        private  IWebDriver driver;
        private  Helper helper;
        public PageObjectManager pageObjectManager;
        public static ExtentTest test;
        public static ExtentReports extent = new ExtentReports();

        [OneTimeSetUp]
        public void Setup()
        {
            string workingdirectory = Environment.CurrentDirectory;
            string parentdirectory = Directory.GetParent(workingdirectory).Parent.Parent.FullName;

            var monthYear = DateTime.Now.ToString("yyyy-MM");
            var reportFolder = Path.Combine(parentdirectory, "Reports", monthYear);
            Directory.CreateDirectory(reportFolder);

            var timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
            string reportpath = Path.Combine(reportFolder, $"index_{timestamp}.html");

            var reporter = new ExtentSparkReporter(reportpath);
            extent = new ExtentReports();
            extent.AttachReporter(reporter);

            extent.AddSystemInfo("Env", " NHS site");
            TestContext.WriteLine("Report will be saved to: " + reportpath);
        }

        [SetUp]
        public void Startbrowser()
        {
            try
            {
                if (extent == null)
                {
                    throw new Exception("ExtentReports is not initialized.");
                }

                string testName = TestContext.CurrentContext.Test.Name;
                test = extent.CreateTest(testName);
                EnvLoader.LoadSecureEnv();
                string browser = ConfigurationManager.AppSettings["Browser"];
                if (string.IsNullOrEmpty(browser))
                {
                    throw new Exception("Browser is not configured.");
                }

                driver = BrowserFactory.CreateDriver(browser);
                driver.Manage().Window.Maximize();

                string siteUrl = ConfigurationManager.AppSettings["URL"];
                if (string.IsNullOrEmpty(siteUrl))
                {
                    throw new Exception("Site URL is not configured.");
                }

                test.Log(Status.Pass, "Browser is Launched");
                driver.Navigate().GoToUrl(siteUrl);
                
                test.Log(Status.Pass, "Navigated to " + siteUrl + " successfully");
                helper = new Helper(driver);
                pageObjectManager = new PageObjectManager(driver,helper);
            }
            catch (Exception ex)
            {
                TestContext.WriteLine("Startbrowser failed: " + ex.Message);
                throw;
            }
        }

       

        public static JsonReader getJSon()
        {
            return new JsonReader();
        }

       

        [TearDown]
        public void Aftertest()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stacktrace = TestContext.CurrentContext.Result.StackTrace;

            if (status == TestStatus.Failed)
            {
                DateTime time = DateTime.Now;
                string fileName = "Screenshot_" + time.ToString("yyyyMMdd_HHmmss") + ".png";
                test.Fail("Test Failed", screenresponse(driver, fileName));
                test.Log(Status.Fail, "Test failed with logtrace: " + stacktrace);
            }
            else if (status == TestStatus.Passed)
            {
                TestContext.WriteLine("Test Passed");
            }

            try
            {
                driver?.Quit();
                driver?.Dispose();
            }
            catch (Exception ex)
            {
                TestContext.WriteLine("Error during driver cleanup: " + ex.Message);
            }

            extent.Flush();
        }

        public Media screenresponse(IWebDriver driver, string ScreenName)
        {
            ITakesScreenshot ts = (ITakesScreenshot)driver;
            var screenshot = ts.GetScreenshot().AsBase64EncodedString;

            return MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot, ScreenName).Build();
        }

        [OneTimeTearDown]
        public void Cleanup()
        {
            try
            {
                extent.Flush();
            }
            catch (Exception ex)
            {
                TestContext.WriteLine("Cleanup failed: " + ex.Message);
            }
        }
    }
}
