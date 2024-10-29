using System.Text.Json.Serialization;

namespace GitHubFollowerApp.Models
{
    public class User
    {
        [JsonPropertyName("login")]
        public string Login { get; set; }

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("avatar_url")]
        public string AvatarUrl { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }
    }
}
