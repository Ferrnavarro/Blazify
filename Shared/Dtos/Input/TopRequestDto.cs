using System;
using System.Collections.Generic;
using System.Text;
using SpotifyAPI.Web;

namespace Blazify.Shared.Dtos.Input
{
    public class TopRequestDto
    {
        public string Token { get; set; }
        public PersonalizationTopRequest.TimeRange TimeRange { get; set; }
    }
}
