using GitHubFollowerApp.Services;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using GitHubFollowerApp.Models;

var builder = WebApplication.CreateBuilder(args);

// Swagger'ý ekleyin
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "GitHub Follower API", Version = "v1" });
});

// GitHubOptions için yapýlandýrma ekleyin
builder.Services.Configure<GitHubOptions>(builder.Configuration.GetSection("GitHub"));

// GitHubService için HttpClient ekleyin
builder.Services.AddHttpClient<GitHubService>();

var app = builder.Build();

// Swagger'ý kullanýn
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "GitHub Follower API V1");
    });
}

// API endpoint'lerini ekleyin
app.MapGet("/followers/{username}", async (string username, GitHubService gitHubService) =>
{
    var followers = await gitHubService.GetFollowersAsync(username);
    return Results.Ok(followers);
});

app.MapGet("/following/{username}", async (string username, GitHubService gitHubService) =>
{
    var following = await gitHubService.GetFollowingAsync(username);
    return Results.Ok(following);
});

app.Run();
