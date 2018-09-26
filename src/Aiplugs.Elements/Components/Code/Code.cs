using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Localization;

namespace Aiplugs.Elements
{
    [HtmlTargetElement("aiplugs-code")]
    public class AiplugsCode : AiplugsField
    {
        public override string ElementName => "aiplugs-code";
        public string Lang { get; set; }
        public string Value { get; set; }
        public AiplugsCode(IStringLocalizer<SharedResource> localizer) : base(localizer)
        {
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var content = await output.GetChildContentAsync();
            
            Process(context, output);

            output.Content.AppendHtml(content);
        }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            base.Process(context, output);
            RenderFieldHeader(context, output);

            var id = GetDomId();

            output.Html("<div class=\"aiplugs-code__cover\">");
                
            output.Tag("textarea", () => {
                if (id != null)
                    output.Attr("id", id);
                
                if (Name != null)
                    output.Attr("name", Name);

                if (Required)
                {
                    output.Attr("required");
                    output.Attr("data-val-required", Localizer.MsgValRequired(Label));
                }
                    
                output.Attr("data-target", "aiplugs-code.input");
                output.Attr("data-val", "true");

            }, Value);

            output.Tag("pre", null, () => {
                output.Tag("code", () => {
                    var @class = "aiplugs-code__view";
                    if (Lang != null)
                        @class = $"{@class} {Lang}";
                    
                    output.Attr("class", @class);
                    output.Attr("data-target", "aiplugs-code.view");
                }, Value);
            });

            output.Html("<div class=\"aiplugs-code__screen\" data-action=\"click->aiplugs-code#edit\">");
            output.Html("<div><i class=\"fa fa-edit\"></i></div>");
            output.Html("</div>");

            output.Html("</div>");

            output.Html("<button type=\"button\" class=\"fas aiplugs-code__toggle\" data-action=\"aiplugs-code#toggleView\"></button>");

            RenderFieldFooter(context, output, Name);
        }
    }
}
