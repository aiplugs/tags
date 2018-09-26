using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Aiplugs.Elements
{
    [HtmlTargetElement("aiplugs-list")]
    public class AiplugsList : TagHelper
    {
        public Dictionary<string, string> Labels { get; set; } 
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            if (Labels == null) 
            {
                Labels = context.AllAttributes
                                .Where(attr => attr.Name.StartsWith("label-"))
                                .ToDictionary(attr => attr.Name.Replace("label-",""), attr => ((HtmlString)attr.Value).Value);
            }
            
            var content = await output.GetChildContentAsync();

            output.TagName = "table";
            output.Attributes.Merge("class", "aiplugs-list");
            output.Attributes.Add("data-controller", "aiplugs-list");

            var labels = string.Join("", Labels.Select(kv => {
                string text = kv.Value, @class = "";
                if (text.EndsWith("*")) {
                    @class = "class=\"flex\"";
                    text = text.Substring(0, text.Length - 1);
                }
                return $"<th {@class}>{text}</th>";
            }));
            output.Content.AppendHtml($"<thead><th></th>{labels}</thread>");
            output.Content.AppendHtml("<tbody>");
            output.Content.AppendHtml(content);
            output.Content.AppendHtml("</tbody>");
        }
    }
}
