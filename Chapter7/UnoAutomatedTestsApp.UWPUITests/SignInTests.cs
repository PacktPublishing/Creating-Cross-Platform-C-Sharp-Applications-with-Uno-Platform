using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using System;
using Axe.Windows.Automation;
using System.Diagnostics;

namespace UnoAutomatedTestsApp.UWPUITests
{
    [TestClass]
    public class SignInTests
    {
        private static WindowsDriver<WindowsElement> session;

        [AssemblyInitialize]
        public static void InitializeTests(TestContext _)
        {
            AppiumOptions appiumOptions = new AppiumOptions();
            appiumOptions.AddAdditionalCapability("app", "UnoAutomatedTestsApp_cdfyh0xbha7kw!App");
            appiumOptions.AddAdditionalCapability("deviceName", "WindowsPC");

            session = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723"), appiumOptions);
        }

        [TestMethod]
        public void VerifyButtonIsEnabledWithUsernameAndPasswordUWP()
        {
            var usernameInput = session.FindElementByAccessibilityId("UsernameInput");
            usernameInput.SendKeys("test");
            var passwordInput = session.FindElementByAccessibilityId("PasswordInput");
            passwordInput.SendKeys("test");
            var signInButton = session.FindElementByAccessibilityId("SignInButton");
            Assert.IsTrue(signInButton.Enabled, "Sign in button should be enabled.");
        }

        [TestMethod]
        public void VerifySignInInterfaceIsAccessible()
        {
            var processes = Process.GetProcessesByName("UnoAutomatedTestsApp");
            Assert.IsTrue(processes.Length > 0);
            var config = Config.Builder.ForProcessId(processes[0].Id).Build();
            var scanner = ScannerFactory.CreateScanner(config);
            Assert.IsTrue(scanner.Scan().ErrorCount == 0, "Accessibility issues found.");
        }
    }
}
