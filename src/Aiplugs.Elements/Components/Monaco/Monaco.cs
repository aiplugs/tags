using System;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Localization;

namespace Aiplugs.Elements
{
    [HtmlTargetElement("aiplugs-monaco")]
    public class AiplugsMonaco : TagHelper
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public string ValueFrom { get; set; }
        public string SettingsFrom { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var id = Guid.NewGuid().ToString();
            output.TagName = "div";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Attributes.Add("id", id);
            output.Attributes.Merge("class", "aiplugs-monaco");
            output.Attributes.Add("data-controller", "aiplugs-monaco");
            output.Attributes.Add("data-aiplugs-monaco-value-from", ValueFrom);
            output.Attributes.Add("data-aiplugs-monaco-settings-from", SettingsFrom);
            output.Content.AppendHtml("<div class=\"aiplugs-progress\" data-target=\"aiplugs-monaco.progress\"></div>");

            output.Tag("textarea", () => {
                if (Name != null)
                    output.Attr("name", Name);

                output.Attr("class", "aiplugs-monaco__input");
                output.Attr("data-target", "aiplugs-monaco.textarea");
            }, Value);
        }
    }
}
