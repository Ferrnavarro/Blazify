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

        public static List<TrackDto> GetTracksDto(this List<PlayHistoryItem> tracksFromSpotify)
        {
            int i = 1;
            var tracks = tracksFromSpotify.Select(s => new TrackDto
            {
                Position = i++,
                Name = s.Track.Name,
                Artist = s.Track.Artists.FirstOrDefault().Name           
            });

            return tracks.ToList();
        }


        public static List<ArtistDto> GetArtistsDto(this List<FullArtist> artistsFromSpotify)
        {
            int i = 1;
            var artists = artistsFromSpotify.Select(s => new ArtistDto
            {
                Position = i++,
                Name = s.Name,
                Genres = s.Genres,
                ImageUrl = s.Images.FirstOrDefault().Url
            }) ;

            return artists.ToList();
        }

    }
}
