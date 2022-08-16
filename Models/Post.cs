namespace Forum.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public int OwnerId { get; set; }
        public int TopicId { get; set; }
        public Topic Topic { get; set; }
    }
}
