namespace Forum.Models
{
    public class Topic
    {
        public int Id { get; set; }
        public string Header { get; set; }
        public int OwnerId { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}
