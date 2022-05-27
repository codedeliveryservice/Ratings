namespace Web.Data.Entities
{
    public class Player
    {
        public Player(
            string name,
            string country,
            int classicalRating,
            int rapidRating,
            int blitzRating,
            string? biography = null,
            string? imageUrl = null)
        {
            Name = name;
            Country = country;
            ClassicalRating = classicalRating;
            RapidRating = rapidRating;
            BlitzRating = blitzRating;
            Biography = biography;
            ImageUrl = imageUrl;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string? Biography { get; set; }
        public string? ImageUrl { get; set; }
        public int ClassicalRating { get; set; }
        public int RapidRating { get; set; }
        public int BlitzRating { get; set; }
    }
}