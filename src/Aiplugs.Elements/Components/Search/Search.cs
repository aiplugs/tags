using Aiplugs.Elements.Extensions;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Collections.Generic;

namespace Aiplugs.Elements.Components.Search
{
    [HtmlTargetElement("aiplugs-search")]
    public class Search : TagHelper
    {
        public IEnumerable<SelectListItem> MethodList { get; set; }
        public string MethodName { get; set; } = "method";
        public string Name { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.TagMode = TagMode.StartTagAndEndTag;

            output.Attributes.Merge("class", "aiplugs-search");

            if (MethodList != null)
            {
                output.Tag("select", () =>
                {
                    output.Attr("name", MethodName);
                    output.Attr("class", "aiplugs-search__method");
                }, () =>
                {
                    foreach (var item in MethodList)
                    {
                        output.Tag("option", () =>
                        {
                            output.Attr("value", item.Value);

                            if (item.Selected)
                                output.Attr("selected");

                        }, item.Text);
                    }
                });
            }

            output.Tag("input", () =>
            {
                output.Attr("name", Name);
                output.Attr("class", "aiplugs-search__query");
            });

            output.Tag("button", () =>
            {
                output.Attr("type", "submit");
                output.Attr("class", "aiplugs-search__submit");
            }, () =>
            {
                output.Html("<i class=\"fa fa-search\"></i>");
            });
        }
    }
}
