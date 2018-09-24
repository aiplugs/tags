using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Aiplugs.Elements
{
    [HtmlTargetElement("Aiplugs-Blade")]
    public class BladeTagHelper : TagHelper
    {
        public string Label { get; set; }
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "section";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Attributes.Merge("class", "aiplugs-blade");

            var content = await output.GetChildContentAsync();
            output.Content.AppendHtml("<button class=\"aiplugs-blade-expand far\"></button>");
            output.Content.AppendHtml($"<header>{Label??""}</header>");
            output.Content.AppendHtml(content);
        }
    }
}
