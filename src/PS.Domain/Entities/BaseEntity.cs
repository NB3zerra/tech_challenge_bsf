using System.Runtime.Serialization;

namespace PS.Domain.Entities
{
    [Serializable]
    [DataContract(IsReference = true)]

    public abstract class BaseEntity
    {
        public Guid Uuid { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; }

    }

}



