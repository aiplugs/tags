using Aiplugs.Elements.Extensions;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Aiplugs.Elements
{
    [HtmlTargetElement(Attributes="[close]")]
    public class AiplugsClose : TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (output.Attributes.TryGetAttribute("close", out var attr))
            {
                output.Attributes.Add("ic-get-from", "//null");
                output.Attributes.Add("ic-target", attr.Value.ToIcTarget());
                output.Attributes.Add("ic-replace-target", "true");
                output.Attributes.Remove(attr);
            }
        }
    }

    [HtmlTargetElement(Attributes="[with-close]")]
    public class AiplugsWithClose : TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (output.Attributes.TryGetAttribute("with-close", out var attr))
            {
                output.Attributes.Add("ic-target", attr.Value.ToIcTarget());
                output.Attributes.Add("ic-replace-target", "true");
                output.Attributes.Remove(attr);
            }
        }
    }
}