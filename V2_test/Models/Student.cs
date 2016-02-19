using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace V2_test.Models
{
    public class Student
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public int StudentId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }
}