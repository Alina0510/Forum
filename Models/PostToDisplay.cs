namespace Forum.Models
{
    public class PostToDisplay
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public string OwnerEmail { get; set; }
        public int TopicId { get; set; }
        public PostToDisplay(int id, string body, string ownerNickname, int topicId)
        {
            Id = id;
            Body = body;
            OwnerEmail = ownerNickname;
            TopicId = topicId;
        }
    }
}
