using Fizzler.Systems.HtmlAgilityPack;
using FluentAssertions;
using IntegrationTests.Helpers;

namespace IntegrationTests
{
    public class ErrorControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public ErrorControllerIntegrationTests(
                CustomWebApplicationFactory factory
            )
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Error_ToReturnView()
        {
            HttpResponseMessage response = await _client.GetAsync("/Error");

            response.Should().BeSuccessful();

            string responseBody = await response.Content.ReadAsStringAsync();

            var document = HtmlHelper.GetHtmlNode(responseBody);

            document.QuerySelectorAll("h1.error-header").Should().NotBeNull();
        }
    }
}
