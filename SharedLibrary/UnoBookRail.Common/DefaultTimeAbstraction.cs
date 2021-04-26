using System;

namespace UnoBookRail.Common
{
    public class DefaultTimeAbstraction : ITimeAbstraction
    {
        public DateTimeOffset GetNow()
        {
            return DateTimeOffset.Now;
        }
    }
}
