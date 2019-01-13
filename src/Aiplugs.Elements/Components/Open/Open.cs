using Aiplugs.Elements.Extensions;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Aiplugs.Elements
{
    [HtmlTargetElement(Attributes="[open]")]
    public class AiplugsOpen : TagHelper
    {
        public string Open { get; set; }
        public string OpenTo { get; set; } = "aiplugs-blade";

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Attributes.Add("ic-get-from", Open);
            output.Attributes.Add("ic-target", OpenTo.ToIcTarget());
            output.Attributes.Add("ic-swap-style", "append");
        }
    }
}