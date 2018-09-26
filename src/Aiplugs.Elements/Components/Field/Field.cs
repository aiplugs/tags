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
            output.TagName = "div";
            output.Attributes.Merge("class", ElementName);
            output.Attributes.Add("data-controller", ElementName);

            output.Html($"<div class=\"aiplugs-field__header\" data-controller=\"aiplugs-info\">");
        
            output.Tag("label", null, () => {

                output.Text(Label);

                if (Required)
                    output.Html(Localizer.LabelRequired());
                
                else
                    output.Html(Localizer.LabelOptional());
            });
            
            if (!string.IsNullOrEmpty(Description))
                output.Html($"<i class=\"aiplugs-info__switch fa fa-info-circle\" data-action=\"click->aiplugs-info#toggle\"></i>");

            output.Html($"<p class=\"aiplugs-info__detail\" data-target=\"aiplugs-info.detail\">{Description}</p>");
            
            output.Html("</div>");
        }
    }
}
