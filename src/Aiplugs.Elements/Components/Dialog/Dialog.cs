using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Aiplugs.Elements
{
    [HtmlTargetElement("aiplugs-dialog")]
    public class AiplugsDialog : TagHelper
    {
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "template";
            output.Attributes.Add("data-controller", "aiplugs-dialog-template");

            var content = await output.GetChildContentAsync();
            output.HtmlLine($"<div class=\"aiplugs-dialog\" data-controller=\"aiplugs-dialog\">");
            output.HtmlLine($"<div class=\"aiplugs-dialog__body\">");
            output.Content.AppendHtml(content);
            output.HtmlLine("</div>");
            output.HtmlLine("</div>");
        }
    }
    [HtmlTargetElement(Attributes="[slot=content]", ParentTag = "aiplugs-dialog")]
    public class AiplugsDialogMessage : TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Attributes.Merge("class", "aiplugs-dialog__content");
        }
    }

    [HtmlTargetElement(Attributes="[slot=actions]", ParentTag = "aiplugs-dialog")]
    public class AiplugsDialogActions : TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Attributes.Merge("class", "aiplugs-dialog__actions");
        }
    }
}
