using NUnit.Framework;
using Query = System.Func<Uno.UITest.IAppQuery, Uno.UITest.IAppQuery>;
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
            App.WaitForElement("UsernameInput");
            App.WaitForElement("PasswordInput");
            App.WaitForElement("SignInButton");
        }

        [Test]
        public void VerifyButtonIsEnabledWithUsernameAndPassword()
        {
            App.ClearText(usernameInput);
            App.EnterText(usernameInput, "test");
            App.ClearText(passwordInput);
            App.EnterText(passwordInput, "test");

            var signInButtonResult = App.WaitForElement(signInButton);
            Assert.IsTrue(signInButtonResult.Length > 0);
            Assert.IsTrue(signInButtonResult[0].Enabled);
        }

        [Test]
        public void VerifyInvalidCredentialsHaveErrorMessage()
        {
            App.ClearText(usernameInput);
            App.EnterText(usernameInput, "invalid@unorail.com");
            App.ClearText(passwordInput);
            App.EnterText(passwordInput, "invalid");
            App.Tap(signInButton);

            var errorMessageResult = App.WaitForElement(errorMessageLabel);
            Assert.IsTrue(errorMessageResult.Length > 0);
        }

        [Test]
        public void VerifySigningInWorks()
        {
            App.ClearText(usernameInput);
            App.EnterText(usernameInput, "demo");
            App.ClearText(passwordInput);
            App.EnterText(passwordInput, "1234");
            App.Tap(signInButton);

            var signedInLabel = App.WaitForElement(this.signedInLabel);
            Assert.IsTrue(signedInLabel.Length > 0);
        }
    }
}
