using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Aiplugs.Elements
{
    [HtmlTargetElement("Aiplugs-Actions")]
    public class ActionsTagHelper : TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.Attributes.Add("class", "aiplugs-actions");
        }
    }
}
