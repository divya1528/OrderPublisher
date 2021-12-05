using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.ComponentModel.DataAnnotations;


namespace ApplicationCore.Model
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("firstName")]
        public string FirstName { get; set; }
        [BsonElement("lastName")]
        public string LastName { get; set; }
        [Required]
        [BsonElement("username")]
        public string Username { get; set; }
        [Required]
        [BsonElement("password")]
        public string Password { get; set; }
        [Required]
        [BsonElement("email")]
        public string Email { get; set; }
        [Required]
        [BsonElement("role")]
        public string Role { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
