using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Aiplugs.Elements.Extensions;
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
        public int? MaxLength { get; set; }
        public int? MinLength { get; set; }
        public string Pattern { get; set; }
        public string PatternErrorMessage { get; set; }
        public AiplugsCode(IStringLocalizer<SharedResource> localizer) : base(localizer)
        {
        }

        protected override void ExtractFromModelExpression()
        {
            base.ExtractFromModelExpression();

            if (ModelExpression != null)
            {
                if (Value == null)
                    Value = (string)GetModelStateValue(ViewContext, Name, typeof(string)) ?? ModelExpression.ModelExplorer.Model?.ToString();

                foreach (var attribute in ModelExpression.ModelExplorer.Metadata.ValidatorMetadata)
                {
                    if (attribute is StringLengthAttribute stringLengthAttribute)
                    {
                        if (MinLength == null)
                            MinLength = stringLengthAttribute.MinimumLength;

                        if (MaxLength == null)
                            MaxLength = stringLengthAttribute.MaximumLength;
                    }

                    else if (MinLength == null && attribute is MinLengthAttribute minLengthAttribute)
                        MinLength = minLengthAttribute.Length;

                    else if (MaxLength == null && attribute is MaxLengthAttribute maxLengthAttribute)
                        MaxLength = maxLengthAttribute.Length;

                    else if (attribute is RegularExpressionAttribute regularExpressionAttribute)
                    {
                        if (Pattern == null)
                            Pattern = regularExpressionAttribute.Pattern;

                        if (PatternErrorMessage == null)
                            PatternErrorMessage = regularExpressionAttribute.ErrorMessage;
                    }
                }
            }
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

                if (!string.IsNullOrEmpty(Pattern))
                {
                    var message = !string.IsNullOrEmpty(PatternErrorMessage) ? PatternErrorMessage : Localizer.MsgValPattern(Label, Pattern);
                    output.Attr("data-val-regex", message);
                    output.Attr("data-val-regex-pattern", Pattern);
                }

                if (MaxLength.HasValue)
                {
                    output.Attr("data-val-maxlength", Localizer.MsgValMaxLengthForString(Label, MaxLength.Value));
                    output.Attr("data-val-maxlength-max", MaxLength.ToString());
                }

                if (MinLength.HasValue)
                {
                    output.Attr("data-val-minlength", Localizer.MsgValMinLengthForString(Label, MinLength.Value));
                    output.Attr("data-val-minlength-min", MinLength.ToString());
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

            output.Tag("template", () => {
                output.Attr("data-controller", "aiplugs-dialog-template");
            }, () => {
                output.HtmlLine($"<div class=\"aiplugs-dialog\" data-controller=\"aiplugs-dialog\">");
                output.HtmlLine($"<div class=\"aiplugs-dialog__content\">");
                output.HtmlLine($"<p class=\"aiplugs-dialog__message\">{Localizer.MsgConfirmDiscard(Label)}</p>");
                output.HtmlLine("<div class=\"aiplugs-dialog__actions\">");
                output.HtmlLine($"<button class=\"aiplugs-button --warning aiplugs-code__close-realy\">{Localizer.LabelConfirmDiscardYes()}</button>");
                output.HtmlLine($"<button class=\"aiplugs-button --block --primary\" data-action=\"aiplugs-dialog#close\">{Localizer.LabelConfirmDiscardNo()}</button>");
                output.HtmlLine("</div>");
                output.HtmlLine("</div>");
                output.HtmlLine("</div>");
            });

            RenderFieldFooter(context, output, Name);
        }
    }

    [HtmlTargetElement(Attributes="[aiplugs-code]")]
    public class AiplugsCodeElement : TagHelper
    {
        public AiplugsCodeElementType AiplugsCode { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Attributes.Merge("class", $"aiplugs-code__{AiplugsCode}");
        }
    }

    public enum AiplugsCodeElementType
    {
        close,
        cancel,
        editor
    }
}
