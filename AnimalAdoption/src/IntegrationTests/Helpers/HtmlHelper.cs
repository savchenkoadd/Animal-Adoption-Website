using HtmlAgilityPack;

namespace IntegrationTests.Helpers
{
    internal class HtmlHelper
    {
        internal static HtmlNode? GetHtmlNode(string responseBody)
        {
            HtmlDocument html = new HtmlDocument();

            html.LoadHtml(responseBody);

            return html.DocumentNode;
        }
    }
}
