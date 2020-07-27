using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TaskIssue.API.Entities
{
    public class Issue
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string TypeId { get; set; }

        [BsonElement("Summary")]
        public string Summary { get; set; }
        public string Description { get; set; }

        [BsonElement("Labels")]
        public string Labels { get; set; }
        public decimal StoryPoint { get; set; }

        [BsonElement("SprintId")]
        public string SprintId { get; set; }
        public string LinkedIssues { get; set; }
        public bool Flagged { get; set; }

        [BsonElement("AssigneeUserId")]
        public string AssigneeUserId { get; set; }

        [BsonElement("ReporterUserId")]
        public string ReporterUserId { get; set; }     
        public string Status { get; set; }
        public bool Completed { get; set; }
        public BsonDateTime CompletedDateTime { get; set; }
        public string CreatedBy { get; set; }
        public BsonDateTime CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public BsonDateTime UpdatedOn { get; set; }
    }
}
