using System.Runtime.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PS.Domain.Entities
{
    [Serializable]
    [DataContract(IsReference = true)]

    public abstract class BaseEntity
    {
        [BsonId] // Mapeia o campo _id do MongoDB
        [BsonRepresentation(BsonType.String)] 
        public Guid Uuid { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; }

    }

}



