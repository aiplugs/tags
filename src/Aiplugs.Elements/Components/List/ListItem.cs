using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Aiplugs.Elements
{
    [HtmlTargetElement("aiplugs-list-item")]
    public class AiplugsListItem : TagHelper
    {
        public string Name {get; set;}
        public string Item { get; set; }
        public bool Checked { get; set; }
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "tr";
            output.Attributes.Merge("class", "aiplugs-list__item");
            output.Attributes.Add("data-target", "aiplugs-list.item");
            output.Attributes.Add("data-controller", "aiplugs-list-item");
            output.Attributes.Add("data-action", "click->aiplugs-list#select");

            if (Checked) 
                output.Attributes.Merge("class", "aiplugs-list__item--checked");

            var content = await output.GetChildContentAsync();
            output.Html("<td><label class=\"aiplugs-list__item-selector\">");
            output.Tag("input", () => {
                output.Attr("type", "checkbox");
                output.Attr("data-target", "aiplugs-list-item.checkbox");
                output.Attr("data-action", "aiplugs-list-item#update");

                if (!string.IsNullOrEmpty(Name))
                    output.Attr("name", Name);

                if (!string.IsNullOrEmpty(Item))
                    output.Attr("value", Item);
            });
            output.Html("</label></td>");            
            output.Content.AppendHtml(content);
        }
    }
}
