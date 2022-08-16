namespace Forum.Models
{
    public static class Utils
    {
        public static User ConvertUserInputToUser(UserInput userInput)
        {
            User user = new User();
            user.Nickname = userInput.Nickname;
            user.Email = userInput.Email;
            user.Password = userInput.Password;
            return user;
        }
        public static Topic NewTopic(string header, User user)
        {
            Topic topic = new Topic();
            topic.Header = header;
            topic.OwnerId = user.Id;
            return topic;
        }
        public static Post NewPost(string body, User user, Topic topic)
        {
            Post post = new Post();
            post.Body = body;
            post.OwnerId = user.Id;
            post.Topic = topic;
            post.TopicId = topic.Id;
            return post;
        }
        public static TopicWithPosts TopicWithPostsById(int id, AppContext db)
        {
            Topic topic = db.Topics.FirstOrDefault(i => i.Id == id);
            TopicWithPosts result = new TopicWithPosts(id, topic.Header, db.Users.FirstOrDefault(i => i.Id == topic.OwnerId).Nickname, PostsToDiplayable(db.Posts.Where(i => i.TopicId == id).ToList(),db));
            return result;
        }
        public static List<TopicDisplay> TopicsToDiplayable(IEnumerable<Topic> topics, IEnumerable<User> users)
        {
            List<TopicDisplay> result = new List<TopicDisplay>(); 
            foreach (var item in topics)
            {
                result.Add(new TopicDisplay(item.Id, item.Header, users.FirstOrDefault(i => i.Id == item.OwnerId).Nickname, item.Posts));
            }
            return result;
        }
        public static List<PostToDisplay> PostsToDiplayable(List<Post> topics, AppContext db)
        {
            List<PostToDisplay> result = new List<PostToDisplay>();
            foreach (var item in topics)
            {
                result.Add(new PostToDisplay(item.Id,item.Body,db.Users.FirstOrDefault(i => i.Id == item.OwnerId).Email, item.TopicId));
            }
            return result;
        }
        public static PostToDisplay PostToDiplayable(Post item, AppContext db)
        {
            return new PostToDisplay(item.Id, item.Body, db.Users.FirstOrDefault(i => i.Id == item.OwnerId).Email, item.TopicId);
        }

    }
}
