using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Aiplugs.Elements.Extensions;
using Microsoft.AspNetCore.Mvc.ModelBinding.Internal;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;
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

        [HtmlAttributeName("asp-for")]
        public ModelExpression ModelExpression { get; set; }

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }
        public string GetDomId()
        {
            if (Id != null)
                return Id;
            
            if (Name != null)
                return NameAndIdProvider.CreateSanitizedId(ViewContext, Name, "_");

            return null;
        }

        protected readonly IStringLocalizer<SharedResource> Localizer;
        public AiplugsField(IStringLocalizer<SharedResource> localizer)
        {
            Localizer = localizer;
        }

        protected virtual void ExtractFromModelExpression()
        {
            if (ModelExpression != null) 
            {
                var modelExplorer = ModelExpression.ModelExplorer;
                var expression = ModelExpression.Name;
                var name = NameAndIdProvider.GetFullHtmlFieldName(ViewContext, expression);

                if (Id == null) 
                {
                    Id = NameAndIdProvider.CreateSanitizedId(ViewContext, name, "_");
                }

                if (Name == null)
                {
                    Name = name;
                }

                if (Label == null)
                {
                    var label = modelExplorer.Metadata.DisplayName ?? modelExplorer.Metadata.PropertyName;
                    if (label == null && expression != null)
                    {
                        var index = expression.LastIndexOf('.');
                        if (index == -1)
                        {
                            // Expression does not contain a dot separator.
                            label = expression;
                        }
                        else
                        {
                            label = expression.Substring(index + 1);
                        }
                    }
                    Label = label;
                }

                if (Description == null)
                {
                    Description = modelExplorer.Metadata.Description;
                }

                if (modelExplorer.Metadata.ValidatorMetadata.Any(attribute => attribute is RequiredAttribute))
                {
                    Required = true;
                }
            }
        }
        internal static object GetModelStateValue(ViewContext viewContext, string key, Type destinationType)
        {
            if (viewContext.ViewData.ModelState.TryGetValue(key, out var entry) && entry.RawValue != null)
            {
                return ModelBindingHelper.ConvertTo(entry.RawValue, destinationType, culture: null);
            }

            return null;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            ExtractFromModelExpression();

            var @class = $"{ElementName} aiplugs-field {(Invalid ? "--invalid":"")}";

            output.TagName = "div";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Attributes.Merge("class", @class);
            output.Attributes.Add("data-controller", ElementName);

            if (!string.IsNullOrEmpty(Name))
                output.Attributes.Add($"data-{ElementName}-name-template", Name);
        }

        protected void RenderFieldHeader(TagHelperContext context, TagHelperOutput output)
        {
            RenderInfo(context, output, "div", () => {
                output.Attr("class", "aiplugs-field__header");
            }, () => {
                output.Tag("label", null, () => {

                    output.Text(Label);

                    output.Tag("span", () => {
                        output.Attr("class", $"aiplugs-field__badge {(Required ? "--required" : "--optional")}");
                    }, () => {
                        if (Required)
                            output.Html(Localizer.LabelRequired());
                        
                        else
                            output.Html(Localizer.LabelOptional());
                    });
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
