using NUnit.Framework;
using Query = System.Func<Uno.UITest.IAppQuery, Uno.UITest.IAppQuery>;
using System.Linq;
using Uno.UITest.Helpers.Queries;

namespace UnoAutomatedTestsApp.UITests.Tests
{
    public class SignInTests : TestBase
    {
        Query usernameInput = q => q.Marked("UsernameInput");
        Query passwordInput = q => q.Marked("PasswordInput");
        Query signInButton = q => q.Marked("SignInButton");
        Query errorMessageLabel = q => q.Marked("SignInErrorMessageTextBlock");
        Query signedInLabel = q => q.Marked("SignedInLabel");

        [Test]
        public void VerifyLoginRenders()
        {
        App.WaitForElement("UsernameInput", "Username input wasn't found.");
        App.WaitForElement("PasswordInput", "Password input wasn't found.");
        App.WaitForElement("SignInButton", "Sign in button wasn't found.");
        }

        [Test]
        public void VerifyButtonIsEnabledWithUsernameAndPassword()
        {
            App.ClearText(usernameInput);
            App.EnterText(usernameInput, "test");
            App.ClearText(passwordInput);
            App.EnterText(passwordInput, "test");

            var signInButtonResult = App.WaitForElement(signInButton);
            Assert.IsTrue(signInButtonResult[0].Enabled, "Sign in button was not enabled.");
        }

        [Test]
        public void VerifyInvalidCredentialsHaveErrorMessage()
        {
            App.ClearText(usernameInput);
            App.EnterText(usernameInput, "invalid");
            App.ClearText(passwordInput);
            App.EnterText(passwordInput, "invalid");
            App.Tap(signInButton);

            var errorMessage = App.Query(q => errorMessageLabel(q).GetDependencyPropertyValue("Text").Value<string>()).First();
            Assert.AreEqual(errorMessage, "Username or password invalid or user does not exist.", "Error message not correct.");
        }

        [Test]
        public void VerifySigningInWorks()
        {
            App.ClearText(usernameInput);
            App.EnterText(usernameInput, "demo");
            App.ClearText(passwordInput);
            App.EnterText(passwordInput, "1234");
            App.Tap(signInButton);

            var signedInMessage = App.Query(q => signedInLabel(q).GetDependencyPropertyValue("Text").Value<string>()).First();
            Assert.AreEqual(signedInMessage, "Successfully signed in!", "Success message not correct.");
        }
    }
}
