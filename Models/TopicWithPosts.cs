namespace Forum.Models
{
    public class TopicWithPosts
    {
        public int Id { get; set; }
        public string Header { get; set; }
        public string OwnerNickname { get; set; }
        public ICollection<PostToDisplay> Posts { get; set; }
        public TopicWithPosts(int id, string header, string ownerNickname, ICollection<PostToDisplay> posts)
        {
            Id = id;
            Header = header;
            OwnerNickname = ownerNickname;
            Posts = posts;
        }
    }
}
