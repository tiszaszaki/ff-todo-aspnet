using Microsoft.AspNetCore.Mvc.Testing;

namespace ff_todo_aspnet_test.IntegrationTests;

public class BoardIntegrationTestFixture : IDisposable
{
    public BoardIntegrationTestFixture()
    {
        var webapp = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder => builder.UseSetting("IsRealDatabase", "false"));
        client = webapp.CreateDefaultClient();
    }

    public void Dispose()
    {
    }

    public HttpClient client { get; }
    public long testBoardId { get; set; }
}
