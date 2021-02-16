using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazify.Client.Dtos;
using SpotifyAPI.Web;

namespace Blazify.Client.Extensions
{
    public static class SpotiyExtensions
    {
        public static List<TrackDto> GetTracksDto(this List<FullTrack> tracksFromSpotify)
        {
            int i = 1;
            var tracks = tracksFromSpotify.Select(s => new TrackDto
            {
                Position = i++,
                Name = s.Name,
                Artist = s.Artists.FirstOrDefault().Name,
                ImageUrl = s.Album.Images.FirstOrDefault().Url
            }) ; 

            return tracks.ToList();
        }
    }
}
