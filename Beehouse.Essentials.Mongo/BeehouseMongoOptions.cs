using Beehouse.Essentials.Mongo.Context;
using Beehouse.Essentials.Mongo.Entities;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Beehouse.Essentials.Mongo
{
    public class BeehouseMongoOptions
    {
        private ICollection<Func<MongoContext, Task>> _buildActions;

        public string MongoConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public bool UseEntityIdentification { get; set; }

        

        public void CreateIndex<T>(IndexKeysDefinition<T> keysDefinition) where T : MongoEntity
        {
            // Ensure that buildActions is not null.
            _buildActions ??= new List<Func<MongoContext, Task>>();

            // Add function to list.
            Func<MongoContext, Task> func = async (db) => await db.Collection<T>().Indexes.CreateOneAsync(new CreateIndexModel<T>(keysDefinition));
            _buildActions.Add(func);
        }

        public async Task IndexCreation(MongoContext db)
        {
            foreach(var func in _buildActions)
            {
                await func?.Invoke(db);
            }
        }
    }
}
