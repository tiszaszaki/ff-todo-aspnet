using Microsoft.AspNetCore.Mvc.Testing;

namespace ff_todo_aspnet_test.IntegrationTests;

public class BoardIntegrationTestFixture
{
    public BoardIntegrationTestFixture()
    {
        var webapp = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder => builder.UseSetting("IsRealDatabase", "false"));
        client = webapp.CreateDefaultClient();

        waitForAddOperation = true;
        testBoardId = -666L;
    }

    public HttpClient client { get; }

    public bool waitForAddOperation { get; set; }
    public long testBoardId { get; set; }
}
