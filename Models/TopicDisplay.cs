namespace Forum.Models
{
    public class TopicDisplay
    {
        public int Id { get; set; }
        public string Header { get; set; }
        public string OwnerNickname { get; set; }
        public ICollection<Post> Posts { get; set; }
        public TopicDisplay(int id, string header, string ownerNickname, ICollection<Post> posts)
        {
            Id = id;
            Header = header;
            OwnerNickname = ownerNickname;
            Posts = posts;
        }
    }
}
