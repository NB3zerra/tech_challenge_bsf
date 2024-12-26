using System.Runtime.Serialization;

namespace PS.Domain.Entities
{
    [Serializable]
    [DataContract(IsReference = true)]

    public abstract class BaseEntity
    {
        public Guid Uuid { get; set; } = Guid.NewGuid();
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

    }

}



