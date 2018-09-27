using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Aiplugs.Elements
{
    [HtmlTargetElement("aiplugs-blade")]
    public class AiplugsBlade : TagHelper
    {
        public string Label { get; set; }
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "section";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Attributes.Merge("class", "aiplugs-blade");
            output.Attributes.Add("data-controller", "aiplugs-blade");

            var content = await output.GetChildContentAsync();
            output.Html("<button class=\"aiplugs-blade__expand far\" data-action=\"aiplugs-blade#toggle\"></button>");
            output.Html($"<header class=\"aiplugs-blade__header\">{Label??""}</header>");
            output.Content.AppendHtml(content);
        }
    }
}
