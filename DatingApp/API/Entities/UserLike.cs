using API.Entities;

namespace API.Entities
{
    public class UserLike
    {
        public AppUser SourceUser { get; set; }
        public int SourceUserId { get; set; }
        public AppUser LikdedUser { get; set; }
        public int LikeUserId { get; set; }
    }
}