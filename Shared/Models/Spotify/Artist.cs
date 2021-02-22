﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Blazify.Shared.Models.Spotify
{
    public class Artist
    {
        public int Position { get; set; }
        public string Name { get; set; }
        public List<string> Genres { get; set; }
        public string ImageUrl { get; set; }
        public string SpotifyUrl { get; set; }
    }
}
