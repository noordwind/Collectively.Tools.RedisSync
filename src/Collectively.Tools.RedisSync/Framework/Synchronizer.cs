using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Collectively.Common.Caching;
using Collectively.Common.Mongo;
using Collectively.Services.Storage.Models.Groups;
using Collectively.Services.Storage.Models.Remarks;
using Collectively.Services.Storage.Models.Users;
using Microsoft.Extensions.Caching.Distributed;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Collectively.Tools.RedisSync.Framework
{
    public class Synchronizer : ISynchronizer
    {
        private readonly IMongoDatabase _database;
        private readonly ICache _cache;
        
        public Synchronizer(IMongoDatabase database, 
            ICache cache)
        {
            _database = database;
            _cache = cache;
        }

        public async Task SynchronizeAsync()
        => await Task.WhenAll(new List<Task> {SyncRemarksAsync(), SyncUsersAsync()});

        private async Task SyncRemarksAsync()
        {
            Console.WriteLine("Synchronizing remarks...");
            var remarks = await _database.GetCollection<Remark>()
                .AsQueryable()
                .ToListAsync();

            var groups = new HashSet<Group>();
            
            foreach(var remark in remarks)
            {
                if(remark.Group != null)
                {
                    var group = groups.SingleOrDefault(x => x.Id == remark.Group.Id);
                    if (group == null)
                    {
                        group = await _database.GetCollection<Group>()
                            .AsQueryable()
                            .FirstOrDefaultAsync(x => x.Id == remark.Group.Id);
                        groups.Add(group);
                    }
                    remark.Group.Criteria = group.Criteria;
                    remark.Group.Members = group.Members.ToDictionary(x => x.UserId, x => x.Role);
                }
            }

            var latestRemarks = remarks.OrderByDescending(x => x.CreatedAt).Take(100);
            foreach (var remark in latestRemarks)
            {
                await _cache.AddToSortedSetAsync("remarks-latest", remark.Id.ToString(), 0, limit: 100);
            }
            foreach (var remark in remarks)
            {
                await _cache.AddAsync($"remarks:{remark.Id}", remark);
                await _cache.GeoAddAsync($"remarks", remark.Location.Longitude,
                    remark.Location.Latitude, remark.Id.ToString());
            }
            Console.WriteLine("Synchronizing remarks has completed.");
        }

        private async Task SyncUsersAsync()
        {
            Console.WriteLine("Synchronizing users...");
            var users = await _database.GetCollection<User>()
                .AsQueryable()
                .ToListAsync();
            foreach (var user in users)
            {
                await _cache.AddAsync($"users:{user.UserId}:state", user.State);
            }
            Console.WriteLine("Synchronizing users has completed.");            
        }
    }
}