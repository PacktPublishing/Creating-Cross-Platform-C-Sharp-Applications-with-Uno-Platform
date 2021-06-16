namespace UnoBookRail.Common.Auth
{
    public class User
    {
        public string Identifier { get; internal set; }

        public string FirstName { get; internal set; }

        public string Surname { get; internal set; }

        public bool IsAdmin { get; internal set; } = false;

        public Role Role { get; internal set; } = Role.Unknown;

        public string FormattedName => $"{FirstName}. {Surname} ({Identifier})";
    }
}
