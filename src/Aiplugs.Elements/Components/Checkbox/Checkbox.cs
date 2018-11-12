using Aiplugs.Elements.Extensions;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Localization;

namespace Aiplugs.Elements
{
    [HtmlTargetElement("aiplugs-checkbox")]
    public class AiplugsCheckbox : AiplugsField
    {
        public override string ElementName => "aiplugs-checkbox";
        public bool Checked { get; set; }
        public AiplugsCheckbox(IStringLocalizer<SharedResource> localizer) : base(localizer)
        {
        }

        protected override void ExtractFromModelExpression()
        {
            base.ExtractFromModelExpression();

            if (ModelExpression != null)
            {
                var type = typeof(bool);
                if (ModelExpression.ModelExplorer.ModelType == type) 
                {
                    Checked = (bool?)GetModelStateValue(ViewContext, Name, typeof(bool?)) ?? (bool)ModelExpression.ModelExplorer.Model;
                }
            }
        }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            base.Process(context, output);
            
            var id = GetDomId();

            RenderInfo(context, output, "div", null, () => {
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
            });
        }
    }
}
