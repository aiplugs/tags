using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Aiplugs.Elements
{
    [HtmlTargetElement("aiplugs-nav")]
    public class AiplugsNav : TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "nav";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Attributes.Merge("class", "aiplugs-nav");
            output.Attributes.Add("data-controller", "aiplugs-nav");
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