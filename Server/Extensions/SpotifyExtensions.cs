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
                ImageUrl = s.Album.Images.FirstOrDefault().Url,
                SpotifyUrl = s.Uri
            });

            return tracks.ToList();
        }


        public static List<Artist> GetArtists(this List<FullArtist> artistsFromSpotify)
        {
            int i = 1;
            var artists = artistsFromSpotify.Select(s => new Artist
            {
                Position = i++,
                Name = s.Name,
                Genres = s.Genres,
                ImageUrl = s.Images.FirstOrDefault().Url,
                SpotifyUrl = s.Uri
                
            });

            return artists.ToList();
        }
    }
}
