using MongoDB.Bson.Serialization.Attributes;

namespace Catolog.API.Entities
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }

        [BsonElement(nameof(Category))]
        public string Category { get; set; }

        [BsonElement(nameof(Summary))]

        public string Summary { get; set; }

        [BsonElement(nameof(Description))]

        public string Description { get; set; }

        [BsonElement(nameof(Price))]

        public decimal Price { get; set; }

        [BsonElement(nameof(ImageFile  ))]
        public string ImageFile { get; set; }
    }
}
