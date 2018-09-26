using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Localization;

namespace Aiplugs.Elements
{
    [HtmlTargetElement("aiplugs-array")]
    public class AiplugsArray : AiplugsField
    {
        public override string ElementName => "aiplugs-array";

        public AiplugsArray(IStringLocalizer<SharedResource> localizer) : base(localizer)
        {
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var content = await output.GetChildContentAsync();

            base.Process(context, output);
            RenderFieldHeader(context, output);

            output.Html("<div class=\"aiplugs-array__items\" data-target=\"aiplugs-array.items\">");
            output.Content.AppendHtml(content);
            output.Html("</div>");

            output.Html("<button type=\"button\" class=\"aiplugs-array__add\" data-action=\"aiplugs-array#add\" data-target=\"aiplugs-array.add\">");
            output.Html("<i class=\"fa fa-plus\"></i>");
            output.Html("</button>");
        }
    }

    [HtmlTargetElement("aiplugs-array-item")]
    public class ArrayItemTagHelper : TagHelper
    {
        public string Label { get; set; }
        public int Index { get; set; }
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var content = await output.GetChildContentAsync();

            output.TagName = "div";
            output.Attributes.Merge("class", "aiplugs-array__item");
            output.Attributes.Add("data-target", "aiplugs-array.item");
            output.Attributes.Add("data-controller", "aiplugs-array-item");
            output.Attributes.Add("data-aiplugs-array-item-label", Label);
            output.Attributes.Add("data-aiplugs-array-item-index", Index);

            output.Tag("div", () => {
                output.Attr("class", "aiplugs-array__item-header");                
            }, () => {
                output.Html("<span class=\"aiplugs-array__item-label\" data-target=\"aiplugs-array-item.label\"></span>");
                output.Html("<button type=\"button\" class=\"aiplugs-array__item-up\" data-action=\"aiplugs-array-item#up\" data-target=\"aiplugs-array-item.up\"><i class=\"fa fa-angle-up\"></i></button>");
                output.Html("<button type=\"button\" class=\"aiplugs-array__item-down\" data-action=\"aiplugs-array-item#down\" data-target=\"aiplugs-array-item.down\"><i class=\"fa fa-angle-down\"></i></button>");
                output.Html("<button type=\"button\" class=\"aiplugs-array__item-remove\" data-action=\"aiplugs-array-item#remove\" data-target=\"aiplugs-array-item.remove\">&times;</button>");
            });

            output.Content.AppendHtml(content);
        }
    }
}
