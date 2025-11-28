using AngleSharp.Text;
using NHSBloodTest.Managers;
using NHSBloodTest.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHSBloodTest.Tests
{
    public class PractiseTest :BaseTest
    {
        
        public void praticeTest()
        {
            pageObjectManager.AlertPage.clickAlertMenu();
            string AlertURL = getDriver().Url;
            Assert.That(AlertURL,Contains.Substring("alertsWindows"));
        }
    }
}
