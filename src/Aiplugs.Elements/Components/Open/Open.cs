using Aiplugs.Elements.Extensions;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Aiplugs.Elements
{
    [HtmlTargetElement(Attributes="[open]")]
    public class AiplugsOpen : TagHelper
    {
        public string Open { get; set; }
        public string OpenTo { get; set; } = "aiplugs-blade";
        public string ReplaceTo { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Attributes.Add("ic-get-from", Open);

            if (string.IsNullOrEmpty(ReplaceTo))
            {
                output.Attributes.Add("ic-target", OpenTo.ToIcTarget());
                output.Attributes.Add("ic-swap-style", "append");
            }
            else
            {
                output.Attributes.Add("ic-target", ReplaceTo.ToIcTarget());
                output.Attributes.Add("ic-swap-style", "replace");
            }
        }
    }
}