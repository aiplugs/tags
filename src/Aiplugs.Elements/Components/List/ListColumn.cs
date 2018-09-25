using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Aiplugs.Elements
{
    [HtmlTargetElement("Aiplugs-List-Column")]
    public class ListColumnTagHelper : TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "td";
            output.Attributes.Merge("class", "aiplugs-list__column");
        }
    }
}
