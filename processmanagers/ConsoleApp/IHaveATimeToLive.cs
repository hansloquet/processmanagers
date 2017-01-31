using System;

namespace ConsoleApp
{
    public interface IHaveATimeToLive
    {
        DateTime DueTime { get; }
    }
}