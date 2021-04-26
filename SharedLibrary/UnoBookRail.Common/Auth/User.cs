namespace UnoBookRail.Common.Auth
{
    public class User
    {
        public string Identifier { get; internal set; }

        public string Initial { get; internal set; }

        public string Surname { get; internal set; }

        public bool IsAdmin { get; internal set; } = false;

        public string FormattedName => $"{Initial}. {Surname} ({Identifier})";
    }
}
