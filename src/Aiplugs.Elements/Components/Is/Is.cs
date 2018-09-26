using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Localization;

namespace Aiplugs.Elements
{
    [HtmlTargetElement(Attributes="aiplugs-is")]
    public class AiplugsIs : TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var attr = output.Attributes.Where(a => a.Name.StartsWith("aiplugs-is")).First();
            var value = attr.Value.ToString();
            if (!value.StartsWith("aiplugs-"))
                value = "aiplugs-" + value;
            var @class = value.Replace("::", "__").Replace(":", "__").Replace(".", "__");
            output.Attributes.Remove(attr);
            output.Attributes.Merge("class", @class);
        }
    }
}
