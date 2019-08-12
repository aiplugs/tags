using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aiplugs.Elements.Extensions;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Aiplugs.Elements
{
    [HtmlTargetElement("aiplugs-list")]
    public class AiplugsList : TagHelper
    {
        public Dictionary<string, string> Labels { get; set; } 
        public Dictionary<string, string> LabelClasses { get; set; } 
        public Dictionary<string, string> LabelStyles { get; set; } 
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            if (Labels == null) 
            {
                Labels = context.AllAttributes
                                .Where(attr => attr.Name.StartsWith("label-"))
                                .ToDictionary(attr => attr.Name.Replace("label-",""), attr => ((HtmlString)attr.Value).Value);
                LabelClasses = context.AllAttributes
                                .Where(attr => attr.Name.StartsWith("class-"))
                                .ToDictionary(attr => attr.Name.Replace("class-",""), attr => ((HtmlString)attr.Value).Value);
                LabelStyles = context.AllAttributes
                                .Where(attr => attr.Name.StartsWith("style-"))
                                .ToDictionary(attr => attr.Name.Replace("style-",""), attr => ((HtmlString)attr.Value).Value);
            }
            
            var content = await output.GetChildContentAsync();

            output.TagName = "table";
            output.Attributes.Merge("class", "aiplugs-list");
            output.Attributes.Add("data-controller", "aiplugs-list");

            var labels = string.Join("", Labels.Select(kv => {
                string text = kv.Value, @class = string.Empty, style = string.Empty;
                if (LabelClasses.ContainsKey(kv.Key)) {
                    @class = $"class=\"{LabelClasses[kv.Key]}\"";
                }
                if (LabelStyles.ContainsKey(kv.Key)) {
                    style = $"style=\"{LabelStyles[kv.Key]}\"";
                }
                return $"<th {@class} {style}>{text}</th>";
            }));
            output.Content.AppendHtml($"<thead><th></th>{labels}</thread>");
            output.Content.AppendHtml("<tbody>");
            output.Content.AppendHtml(content);
            output.Content.AppendHtml("</tbody>");
        }
    }
}
