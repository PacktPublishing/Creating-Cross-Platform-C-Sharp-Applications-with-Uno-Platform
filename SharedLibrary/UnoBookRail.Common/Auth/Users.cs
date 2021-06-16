using System.Collections.Generic;

namespace UnoBookRail.Common.Auth
{
    public class Users
    {
        public static User DemoUser = new User()
        {
            Identifier = "demo",
            FirstName = "Chris",
            Surname = "Smith",
        };

        public static IList<User> DemoUsers = new List<User>()
        {
            new User()
            {
                Identifier = "acrawford",
                FirstName = "Alex",
                Surname = "Crawford",
                Role = Role.Engineering
            },
            new User()
            {
                Identifier = "fcurtis",
                FirstName = "Felix",
                Surname = "Curtis",
                Role = Role.Cleaning
            },
            new User()
            {
                Identifier = "lvasquez",
                FirstName = "Laura",
                Surname = "Vasquez",
                Role = Role.HR,
            },
            new User()
            {
                Identifier = "kbell",
                FirstName = "Kathryn",
                Surname = "Bell",
                Role = Role.Accounting
            },
            new User()
            {
                Identifier = "cmartinez",
                FirstName = "Clara",
                Surname = "Alvarez",
                Role = Role.IT
            },
            new User()
            {
                Identifier = "palvarez",
                FirstName = "Phillip",
                Surname = "Alvarez",
                Role = Role.PR
            },
            new User()
            {
                Identifier = "celliot",
                FirstName = "Connor",
                Surname = "Elliot",
                Role = Role.Cleaning
            },
            new User()
            {
                Identifier = "ljennings",
                FirstName = "Lucy",
                Surname = "Jennings",
                Role = Role.PR
            },
            new User()
            {
                Identifier = "trice",
                FirstName = "Tracy",
                Surname = "Rice",
                Role = Role.Engineering
            },
            new User()
            {
                Identifier = "jromero",
                FirstName = "Judith",
                Surname = "Romero",
                Role = Role.Drivers
            },
            new User()
            {
                Identifier = "shawkins",
                FirstName = "Sue",
                Surname = "Hawkins",
                Role = Role.Drivers
            },
            new User()
            {
                Identifier = "jromero",
                FirstName = "Enrique",
                Surname = "Morino",
                Role = Role.Drivers
            },
            new User()
            {
                Identifier = "amiller",
                FirstName = "Anita",
                Surname = "Miller",
                Role = Role.Drivers
            },
            new User()
            {
                Identifier = "jholt",
                FirstName = "Jamie",
                Surname = "Holt",
                Role = Role.Drivers
            }
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
