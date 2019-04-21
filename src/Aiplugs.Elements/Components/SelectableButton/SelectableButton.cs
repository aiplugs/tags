using Aiplugs.Elements.Extensions;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aiplugs.Elements.Components.SelectableButton
{

    [HtmlTargetElement("a", Attributes = "[is=aiplugs-selectable-button]")]
    [HtmlTargetElement("input", Attributes = "[is=aiplugs-selectable-button],[type=submit]")]
    [HtmlTargetElement("button", Attributes = "[is=aiplugs-selectable-button]")]
    public class SelectableButton : TagHelper
    {
        public ButtonStyle Looks { get; set; } = ButtonStyle.@default;
        public ButtonSize Size { get; set; } = ButtonSize.@default;
        public Color Color { get; set; } = Color.@default;
        public IEnumerable<SelectListItem> Selection { get; set; }
        public string SelectName { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var style = Looks != ButtonStyle.@default ? "--" + Looks.ToString().ToLower() : "";
            var size = Size != ButtonSize.@default ? "--" + Size.ToString().ToLower() : "";
            var color = Color != Color.@default ? "--" + Color.ToString().ToLower() : "";

            var @class = output.Attributes["class"]?.Value ?? string.Empty;

            output.PreElement.AppendHtml($"<div class=\"aiplugs-selectable-button {@class} {style} {size} {color}\" data-controller=\"aiplugs-selectable-button\">");

            output.PreElement.AppendHtml("<select class=\"aiplugs-selectable-button__select\" data-target=\"aiplugs-selectable-button.select\" data-action=\"aiplugs-selectable-button#change\" ");
            if (!string.IsNullOrEmpty(SelectName))
            {
                output.PreElement.AppendHtml("name=\"");
                output.PreElement.Append(SelectName);
                output.PreElement.AppendHtml("\"");
            }
            output.PreElement.AppendHtml(">");

            foreach (var item in Selection ?? new SelectListItem[0])
            {
                output.PreElement.AppendHtml("<option value=\"");
                output.PreElement.Append(item.Value);
                output.PreElement.AppendHtml("\" ");

                if (item.Selected)
                    output.PostContent.AppendHtml("selected");

                output.PreElement.AppendHtml(">");
                output.PreElement.Append(item.Text);
                output.PreElement.AppendHtml("</option>");
            }
            output.PreElement.AppendHtml("</select>");

            output.Attributes.Add("class", "aiplugs-selectable-button__button");
            output.Attributes.Add("data-target", "aiplugs-selectable-button.button");

            output.PostElement.AppendHtml("</div>");
        }
    }
}
