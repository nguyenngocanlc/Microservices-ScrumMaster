using SprintActive.API.Data.Interfaces;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SprintActive.API.Data
{
    public class SprintActiveContext : ISprintActiveContext
    {
        private readonly ConnectionMultiplexer _redisConnection;
        public SprintActiveContext(ConnectionMultiplexer redisConnection)
        {
            _redisConnection = redisConnection;
            Redis = redisConnection.GetDatabase();
        }

        public IDatabase Redis { get; }
    }
}
