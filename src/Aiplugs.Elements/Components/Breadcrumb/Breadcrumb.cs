using System.Threading.Tasks;
using Aiplugs.Elements.Extensions;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Aiplugs.Elements
{
    [HtmlTargetElement("ul", Attributes = "[is=aiplugs-breadcrumb]")]
    public class AiplugsBreadcrumb : TagHelper
    {
        public string ElementName => "aiplugs-breadcrumb";

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Attributes.Merge("class", ElementName);
            if (output.Attributes.TryGetAttribute("is", out var attr)) {
                output.Attributes.Remove(attr);
            }
        }
    }
}
