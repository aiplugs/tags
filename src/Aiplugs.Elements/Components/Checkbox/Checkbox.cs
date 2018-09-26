using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Localization;

namespace Aiplugs.Elements
{
    [HtmlTargetElement("aiplugs-checkbox")]
    public class AiplugsCheckbox : BaseInputTagHelper
    {
        public override string ElementName => "aiplugs-checkbox";
        public bool Checked { get; set; }
        public AiplugsCheckbox(IStringLocalizer<SharedResource> localizer) : base(localizer)
        {
        }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var id = GetDomId();
            
            output.TagName = "div";
            output.Attributes.Merge("class", "aiplugs-checkbox");
            output.Attributes.Add("data-controller", "aiplugs-checkbox");

            output.Html("<label>");
            output.Tag("input", () => {
                output.Attr("type", "checkbox");
                output.Attr("data-target", "aiplugs-checkbox.checkbox");
                output.Attr("data-action", "aiplugs-checkbox#update");

                if (id != null)
                    output.Attr("id", id);

                if (Name != null)
                    output.Attr("name", Name);
                
                if (Checked)
                    output.Html(" checked ");
            });
            
            output.Text(Label);
            output.Html("</label>");

            if (!string.IsNullOrEmpty(Description))
                output.Html("<i class=\"fa fa-info-circle aiplugs-checkbox__info\" data-action=\"click->aiplugs-checkbox#toggleDescription\"></i>");
            
            output.Tag("p", () => {

                output.Attr("class", "aiplugs-checkbox__description");
                output.Attr("data-target", "aiplugs-checkbox.description");
            
            }, Description);
        }
    }
}
