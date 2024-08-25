using HtmlAgilityPack;
using HtmlAgilityPack.CssSelectors.NetCore;
using Microsoft.AspNetCore.Mvc;

namespace WebScraper.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CharacterForecastController : ControllerBase
    {
        [HttpGet()]
        public async Task<ActionResult> Get()
        {
            var url = "https://attackontitan.fandom.com/wiki/List_of_characters/Anime";
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(url);

            IList<HtmlNode> nodes = doc.QuerySelectorAll("div.characterbox-main")[1]
                .QuerySelectorAll("div.characterbox-container table tbody");

            var data = nodes.Select((node) => {
                var name = node.QuerySelector("tr:nth-child(2) th a").InnerText;
                return new {
                    name = name,
                    imageUrl = node.QuerySelector("tr td div a img")
                        .GetAttributeValue("data-src", "https://www.pinterest.com/pin/cuties3-in-2024--243194448622523247/"),
                    descriptionUrl = $"https://attackontitan.fandom.com/wiki/{name.Replace(" ", "_")}_(Anime)"
                };
            });
            return Ok(data);
        }
    }
}
