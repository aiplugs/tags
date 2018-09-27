using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Aiplugs.Elements
{
    [HtmlTargetElement("aiplugs-dialog")]
    public class AiplugsDialog : TagHelper
    {
        public string Open { get; set; }
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "template";
            output.Attributes.Add("data-controller", "aiplugs-dialog-template");
            output.Attributes.Add("data-aiplugs-dialog-template-open", Open);

            var content = await output.GetChildContentAsync();
            output.HtmlLine($"<div class=\"aiplugs-dialog\" data-controller=\"aiplugs-dialog\">");
            output.HtmlLine($"<div class=\"aiplugs-dialog__content\">");
            output.Content.AppendHtml(content);
            output.HtmlLine("</div>");
            output.HtmlLine("</div>");
        }
    }

    [HtmlTargetElement(Attributes="[behave=aiplugs-dialog-close]", ParentTag="aiplugs-dialog")]
    public class AiplugsDialogClose : TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Attributes.Add("data-action", "aiplugs-dialog#close");
        }
    }
}
