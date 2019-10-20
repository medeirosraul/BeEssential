using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Beehouse.Essentials.Mongo
{
    public class BeehouseMongoOptions
    {
        public string MongoConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public bool UseEntityIdentification { get; set; }
    }
}
