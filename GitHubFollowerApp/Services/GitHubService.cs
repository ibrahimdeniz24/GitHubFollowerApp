using System.Net.Http.Headers;
using System.Text.Json;
using Microsoft.Extensions.Options;
using GitHubFollowerApp.Models;

namespace GitHubFollowerApp.Services
{
    public class GitHubService
    {
        public readonly HttpClient _httpClient;

        public GitHubService(HttpClient httpClient, IOptions<GitHubOptions> options)
        {

            _httpClient = httpClient;


            // GitHub API erişim belirtecini ayarlayın
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", options.Value.ApiToken);
            _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/85.0.4183.121 Safari/537.36");
        }

        public async Task<List<User>> GetFollowersAsync(string username)
        {

            var response = await _httpClient.GetAsync($"https://api.github.com/users/{username}/followers");
            response.EnsureSuccessStatusCode(); // Hata durumunda bir exception fırlatır
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<User>>(content);
        }

        public async Task<List<User>> GetFollowingAsync(string username)
        {
            var response = await _httpClient.GetAsync($"https://api.github.com/users/{username}/following");
            response.EnsureSuccessStatusCode(); // Hata durumunda bir exception fırlatır
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<User>>(content);
        }
    }
}
