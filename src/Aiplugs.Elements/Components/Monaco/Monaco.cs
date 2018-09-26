using System;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Localization;

namespace Aiplugs.Elements
{
    [HtmlTargetElement("aiplugs-monaco")]
    public class AiplugsMonaco : TagHelper
    {
        public string Value { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var id = Guid.NewGuid().ToString();
            output.TagName = "div";
            output.Attributes.Add("id", id);
            output.Attributes.Merge("class", "aiplugs-monaco");
            output.Attributes.Add("data-controller", "aiplugs-monaco");
            output.Content.AppendHtml("<div class=\"aiplugs-progress\" data-target=\"aiplugs-monaco.progress\"></div>");
        }
    }
}
