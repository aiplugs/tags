using Aiplugs.Elements.Extensions;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Aiplugs.Elements
{
    [HtmlTargetElement("aiplugs-nav")]
    public class AiplugsNav : TagHelper
    {
        public int FoldThreshold { get; set; } = 768;
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "nav";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Attributes.Merge("class", "aiplugs-nav");
            output.Attributes.Add("data-controller", "aiplugs-nav");
            output.Attributes.Add("data-aiplugs-nav-threshold", FoldThreshold);
        }
    }

    [HtmlTargetElement(Attributes="[is=aiplugs-nav-fold]")]
    public class AiplugsNavFold : TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Attributes.Add("data-action", "aiplugs-nav#toggle");
        }
    }
}