using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Localization;

namespace Aiplugs.Elements
{
    public abstract class BaseInputTagHelper : TagHelper
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
        public BaseInputTagHelper(IStringLocalizer<SharedResource> localizer)
        {
            Localizer = localizer;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.Attributes.Merge("class", ElementName);
            output.Attributes.Add("data-controller", ElementName);

            output.Html($"<div class=\"{ElementName}__header\">");
        
            output.Tag("label", null, () => {

                output.Text(Label);

                if (Required)
                    output.Html(Localizer.LabelRequired());
                
                else
                    output.Html(Localizer.LabelOptional());
            });
            
            if (!string.IsNullOrEmpty(Description))
                output.Html($"<i class=\"fa fa-info-circle {ElementName}__info\" data-action=\"click->{ElementName}#toggleDescription\"></i>");
            
            output.Tag("p", () => {
                output.Attr("class", $"{ElementName}__description");
                output.Attr("data-target", $"{ElementName}.description");
            }, Description);
            
            output.Html("</div>");
        }
    }
}