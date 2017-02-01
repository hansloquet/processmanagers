using System;

namespace ProcessManagers
{
    public interface IHaveATimeToLive
    {
        DateTime DueTime { get; }
    }
}