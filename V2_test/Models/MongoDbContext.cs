using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using V2_test.Properties;

namespace V2_test.Models
{
    /// <summary>
    /// Db Context for Mongo 
    /// </summary>
    public class MongoDbContext
    {
        public IMongoDatabase Database;

        public MongoDbContext()
        {
            var client = new MongoClient(Settings.Default.ConnectionString);
            Database = client.GetDatabase(Settings.Default.Database);
        }

        public IMongoCollection<Student> Students
        {
            get { return Database.GetCollection<Student>("Students"); }
        }
    }
}