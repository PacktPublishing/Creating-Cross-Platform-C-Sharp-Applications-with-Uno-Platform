using System;

namespace UnoBookRail.Common
{
    public interface ITimeAbstraction
    {
        DateTimeOffset GetNow();
    }
}
