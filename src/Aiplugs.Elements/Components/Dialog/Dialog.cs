using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Aiplugs.Elements
{
    [HtmlTargetElement("aiplugs-dialog")]
    public class AiplugsDialog : TagHelper
    {
        public string Open { get; set; }
        public string Close { get; set; }
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "template";
            output.Attributes.Add("class", "aiplugs-dialog-template");
            output.Attributes.Add("data-open", Open);
            output.Attributes.Add("data-close", Close);

            var id = Guid.NewGuid().ToString();
            var content = await output.GetChildContentAsync();
            output.Content.AppendHtml($"<div class=\"aiplugs-dialog\" id=\"{id}\">");
            output.Content.AppendHtml($"<div class=\"aiplugs-dialog-content\">");
            output.Content.AppendHtml(content);
            output.Content.AppendHtml("</div>");
            output.Content.AppendHtml("</div>");
        }
    }
}
