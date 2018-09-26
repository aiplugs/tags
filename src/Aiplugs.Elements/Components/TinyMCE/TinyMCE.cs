using System;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Localization;

namespace Aiplugs.Elements
{
    [HtmlTargetElement("aiplugs-tinymce")]
    public class AiplugsTinyMCE : TagHelper
    {
        public string Value { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var id = Guid.NewGuid().ToString();
            output.TagName = "div";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Attributes.Merge("class", "aiplugs-tinymce");
            output.Attributes.Add("data-controller", "aiplugs-tinymce");

            output.Tag("textarea", () => {
                output.Attr("id", id);
                output.Attr("data-target", "aiplugs-tinymce.textarea");
            }, Value??"");
        }
    }
}
