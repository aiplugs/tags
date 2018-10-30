using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Aiplugs.Elements
{
    [HtmlTargetElement("aiplugs-blade")]
    public class AiplugsBlade : TagHelper
    {
        public string Label { get; set; }
        public bool Expanded { get; set; }
        public bool Wide { get; set; }
        public bool Full { get; set; }
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "section";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Attributes.Merge("class", "aiplugs-blade");
            output.Attributes.Add("data-controller", "aiplugs-blade");
            
            if (Expanded)
                output.Attributes.Add("data-aiplugs-blade-expanded", "true");

            if (Wide)
                output.Attributes.Merge("class", "--wide");

            if (Full)
                output.Attributes.Merge("class", "--full");

            var content = await output.GetChildContentAsync();
            output.Html("<button class=\"aiplugs-blade__expand far\" data-action=\"aiplugs-blade#toggle\"></button>");
            output.Html($"<header class=\"aiplugs-blade__header\">{Label??""}</header>");
            output.Content.AppendHtml(content);
        }
    }
}
