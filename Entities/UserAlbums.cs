namespace Modsenfy.Entities
{
    public class UserAlbums
    {
        public int UserId { get; set; }

        public User User { get; set; }

        public int AlbumId { get; set; }

        public Album Album { get; set;}
    }
}
