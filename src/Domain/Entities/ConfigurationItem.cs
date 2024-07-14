using Domain.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;


namespace Domain.Entities;

public class ConfigurationItem
{

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string Name { get; set; }
    public ConfigurationValueType Type { get; set; }
    public string Value { get; set; }
    public bool IsActive { get; set; }
    public string ApplicationName { get; set; }

    [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
    [BsonElement("lastUpdated")]
    public DateTime LastUpdated { get; set; }

}
