using FluentAssertions;
using Fizzler.Systems.HtmlAgilityPack;
using IntegrationTests.Helpers;

namespace IntegrationTests
{
    public class AnimalControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public AnimalControllerIntegrationTests(
                CustomWebApplicationFactory factory
            )
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Feed_ToReturnView()
        {
            HttpResponseMessage response = await _client.GetAsync("/Feed");

            response.Should().BeSuccessful();

            string responseBody = await response.Content.ReadAsStringAsync();

            var document = HtmlHelper.GetHtmlNode(responseBody);

            document.QuerySelectorAll("h1.feed-header").Should().NotBeNull();
            document.QuerySelectorAll("div#navbar").Should().NotBeNull();
        }

        [Fact]
        public async Task Main_ToReturnView()
        {
            HttpResponseMessage response = await _client.GetAsync("/Main");

            response.Should().BeSuccessful();

            string responseBody = await response.Content.ReadAsStringAsync();

            var document = HtmlHelper.GetHtmlNode(responseBody);

            document.QuerySelectorAll("div.main").Should().NotBeNull();
        }
    }
}