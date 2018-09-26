using System;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Localization;

namespace Aiplugs.Elements
{
    public abstract class AiplugsField : TagHelper
    {
        public abstract string ElementName { get; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public bool Required { get; set; }
        public bool Invalid { get; set; }
        public string GetDomId()
        {
            return Id ?? Name?.Replace(".", "_");
        }

        protected readonly IStringLocalizer<SharedResource> Localizer;
        public AiplugsField(IStringLocalizer<SharedResource> localizer)
        {
            Localizer = localizer;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var @class = $"{ElementName} aiplugs-field {(Invalid ? "--invalid":"")}";

            output.TagName = "div";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Attributes.Merge("class", @class);
            output.Attributes.Add("data-controller", ElementName);
        }

        protected void RenderFieldHeader(TagHelperContext context, TagHelperOutput output)
        {
            RenderInfo(context, output, "div", () => {
                output.Attr("class", "aiplugs-field__header");
            }, () => {
                output.Tag("label", null, () => {

                    output.Text(Label);

                    if (Required)
                        output.Html(Localizer.LabelRequired());
                    
                    else
                        output.Html(Localizer.LabelOptional());
                });
            });
        }
        protected void RenderInfo(TagHelperContext context, TagHelperOutput output, string tag,  Action attr, Action content)
        {
            output.Tag(tag, () => {
                attr?.Invoke();
                output.Attr("data-controller", "aiplugs-info");
            },() => {
                content?.Invoke();
                if (!string.IsNullOrEmpty(Description))
                    output.Html($"<i class=\"aiplugs-info__switch fa fa-info-circle\" data-action=\"click->aiplugs-info#toggle\"></i>");

                output.Html($"<p class=\"aiplugs-info__detail\" data-target=\"aiplugs-info.detail\">{Description}</p>");
            });
        }  

        protected void RenderFieldFooter(TagHelperContext context, TagHelperOutput output, string name)
        {
            output.Tag("p", () => {
                var @class = $"aiplugs-field__message {(Invalid ? "field-validation-error" : "field-validation-valid")}";

                output.Attr("class", @class);
                
                if (name != null)
                {
                    output.Attr("data-valmsg-replace", "true");
                    output.Attr("data-valmsg-for", name);
                }

            });
        }
    }
}
