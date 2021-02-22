using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazify.Shared.Models.Spotify;
using SpotifyAPI.Web;

namespace Blazify.Server.Extensions
{
    public static class SpotifyExtensions
    {
        public static List<Track> GetTracks(this List<FullTrack> tracksFromSpotify)
        {
            int i = 1;
            var tracks = tracksFromSpotify.Select(s => new Track
            {
                Position = i++,
                Name = s.Name,
                Artist = s.Artists.FirstOrDefault().Name,
                ImageUrl = s.Album.Images.FirstOrDefault().Url
            });

            return tracks.ToList();
        }
    }
}
