using System;
using System.Threading.Tasks;
using Aiplugs.Elements.Extensions;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Aiplugs.Elements
{
    [HtmlTargetElement("aiplugs-list-column")]
    public class AiplugsListColumn : TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "td";
            output.Attributes.Merge("class", "aiplugs-list__column");
        }
    }
}
