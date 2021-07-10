using System;
using System.Linq;

namespace UnoBookRail.Common.Auth
{
    public static class Authentication
    {
        public static User GetCurrentUser()
        {
            // In a real application, the code would be more complex.
            // For sake of simplicity, we will assume they are the demo user.
            return Users.DemoUser;
        }

        public static SignInResponse SignIn(string staffIdentifier, string passCode)
        {
            var response = new SignInResponse();

            if (string.IsNullOrWhiteSpace(staffIdentifier))
            {
                response.Messages.Add("Staff identifier must be provided.");
            }
            else if (staffIdentifier.Trim().Length != 4)
            {
                response.Messages.Add("Staff identifier is not valid.");
            }

            if (string.IsNullOrWhiteSpace(passCode))
            {
                response.Messages.Add("Passcode must be provided.");
            }
            else if (passCode.Trim().Length != 4)
            {
                response.Messages.Add("Passcode is not valid.");
            }

            if (staffIdentifier.ToLowerInvariant() == Users.DemoUser.Identifier
              && passCode == "1234")
            {
                response.UserDetails = Users.DemoUser;
                response.IsSuccessful = true;
            }
            else if (!response.Messages.Any())
            {
                response.Messages.Add("Unknown user details.");
            }

            return response;
        }
    }
}
