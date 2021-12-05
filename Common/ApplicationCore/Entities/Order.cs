using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Entities
{
    public class Order
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        //[BsonElement("Name")]

        [Required]
        public string UserID { get; set; }
        [Required]
        public List<Product> Products { get; set; }

        public string Status { get; set; }
    }

    public class Product
    {
        public string ProductID { get; set; }
        public int ProductQty { get; set; }
        public decimal ProductPrice { get; set; }
    }
}
