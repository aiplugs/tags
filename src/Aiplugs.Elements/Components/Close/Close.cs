using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Aiplugs.Elements
{
    [HtmlTargetElement(Attributes="[behave$='-close']")]
    public class AiplugsClose : TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (output.Attributes.TryGetAttribute("behave", out var attr))
            {
                var behave = attr.Value.ToString().Replace("-close", "#close");
                output.Attributes.Add("data-action", behave);
            }
        }
    }
}