using System.Collections.Generic;

namespace Blazify.Client.Dtos
{
    public class ArtistDto
    {
        public int Position { get; set; }
        public string Name { get; set; }
        public List<string> Genres { get; set; }
        public string ImageUrl { get; set; }

    }
}
