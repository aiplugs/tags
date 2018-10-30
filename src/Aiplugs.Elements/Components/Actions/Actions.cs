using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Aiplugs.Elements
{
    [HtmlTargetElement("aiplugs-actions")]
    public class AiplugsActions : TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.Attributes.Merge("class", "aiplugs-actions");
            output.Attributes.Add("data-controller", "aiplugs-actions");
        }
    }
}
