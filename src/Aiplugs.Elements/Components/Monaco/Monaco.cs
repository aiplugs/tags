using System;
using Aiplugs.Elements.Extensions;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;
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
        public string JsonSchemas { get; set; }
        [HtmlAttributeName("asp-for")]
        public ModelExpression ModelExpression { get; set; }
        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (ModelExpression != null)
            {
                var expression = ModelExpression.Name;
                var name = NameAndIdProvider.GetFullHtmlFieldName(ViewContext, expression);

                if (Name == null)
                    Name = name;

                if (Value == null)
                    Value = (string)AiplugsField.GetModelStateValue(ViewContext, Name, typeof(string)) ?? ModelExpression.ModelExplorer.Model?.ToString();
            }
            var id = Guid.NewGuid().ToString();
            output.TagName = "div";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Attributes.Add("id", id);
            output.Attributes.Merge("class", "aiplugs-monaco");
            output.Attributes.Add("data-controller", "aiplugs-monaco");

            if (!string.IsNullOrEmpty(ValueFrom))
                output.Attributes.Add("data-aiplugs-monaco-value-from", ValueFrom);

            if (!string.IsNullOrEmpty(SettingsFrom))
                output.Attributes.Add("data-aiplugs-monaco-settings-from", SettingsFrom);

            if (!string.IsNullOrEmpty(JsonSchemas))
                output.Attributes.Add("data-aiplugs-monaco-json-schemas", JsonSchemas);

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
