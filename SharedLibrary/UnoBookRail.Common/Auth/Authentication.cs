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

        public static SignInResponse SignIn(string username, string password)
        {
            var response = new SignInResponse();

            if (string.IsNullOrWhiteSpace(username))
            {
                response.Messages.Add("Username must be provided.");
            }
            else if (username.Trim().Length < 3)
            {
                response.Messages.Add("Username is not valid.");
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                response.Messages.Add("Password must be provided.");
            }
            else if (password.Trim().Length <= 4)
            {
                response.Messages.Add("Password is too short.");
            }

            if (username.ToLowerInvariant() == Users.DemoUser.Identifier
              && password == "1234")
            {
                response.UserDetails = Users.DemoUser;
                response.IsSuccessful = true;
            }
            else if (!response.Messages.Any())
            {
                response.Messages.Add("Username or password invalid or user does not exist.");
            }

            return response;
        }
    }
}
