using StackExchange.Redis;

namespace SprintActive.API.Data.Interfaces
{
    public interface ISprintActiveContext
    {
        IDatabase Redis { get; }
    }
}
