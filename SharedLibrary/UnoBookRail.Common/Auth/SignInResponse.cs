using System.Collections.Generic;

namespace UnoBookRail.Common.Auth
{
    public class SignInResponse
    {
        public bool IsSuccessful { get; internal set; } = false;

        public List<string> Messages { get; internal set; } = new List<string>();

        public User UserDetails { get; internal set; }
    }
}
