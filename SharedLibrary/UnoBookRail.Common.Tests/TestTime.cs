using System;

namespace UnoBookRail.Common.Tests
{
    public class TestTime : ITimeAbstraction
    {
        public DateTimeOffset Now { get; set; } = DateTimeOffset.Now;

        public DateTimeOffset GetNow()
        {
            return Now;
        }
    }
}
