using Aiplugs.Elements.Extensions;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Aiplugs.Elements
{
    [HtmlTargetElement("aiplugs-modal")]
    public class AiplugsModal : TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.Attributes.Merge("class", "aiplugs-modal");
            output.Attributes.Add("data-controller", "aiplugs-modal");
        }
    }
}