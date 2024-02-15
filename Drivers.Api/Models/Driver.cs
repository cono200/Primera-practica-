


using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Drivers.Api.Models;

public class Driver
{
    [BsonId] //OBTENER EL ID AUTO GENERADO POR MONGO
    [BsonRepresentation(BsonType.ObjectId)]

    public string Id{get; set;} = string.Empty;
    
    [BsonElement("Name")]
    public string name{get; set;} = string.Empty;
        [BsonElement("number")]
    public int number{get; set;}
        [BsonElement("Team")]
        public string Team{get; set;} = string.Empty;


}