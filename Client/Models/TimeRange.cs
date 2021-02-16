namespace Blazify.Client.Models
{
    public class TimeRange
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Label { get; set; }
        public bool Selected { get; set; }
        public string CssClass { get { return Selected ? "time-option time--active" : "time-option"; } private set { } }
        public SpotifyAPI.Web.PersonalizationTopRequest.TimeRange TimeRangeEnum { get; set; }



    }
}
