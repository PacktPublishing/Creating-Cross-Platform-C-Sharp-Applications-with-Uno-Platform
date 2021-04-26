namespace UnoBookRail.Common.Auth
{
    public class Users
    {
        public static User DemoUser = new User()
        {
            Identifier = "demo",
            Initial = "C",
            Surname = "Smith",
        };

        public static User GetUserFromIdentifier(string userIdentifier)
        {
            if (userIdentifier == DemoUser.Identifier)
            {
                return DemoUser;
            }
            return new User()
            {
                Identifier = userIdentifier
            };
        }

    }
}
