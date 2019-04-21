using System.ComponentModel.DataAnnotations;
using Aiplugs.Elements.Extensions;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Localization;

namespace Aiplugs.Elements
{
    [HtmlTargetElement("aiplugs-textarea")]
    public class AiplugsTextarea : AiplugsField
    {
        public override string ElementName => "aiplugs-textarea";
        public string Placeholder { get; set; }
        public int? MaxLength { get; set; }
        public int? MinLength { get; set; }
        public string Pattern { get; set; }
        public string PatternErrorMessage { get; set; }
        public string Value { get; set; }
        public AiplugsTextarea(IStringLocalizer<SharedResource> localizer) : base(localizer)
        {
        }

        protected override void ExtractFromModelExpression()
        {
            base.ExtractFromModelExpression();

            if (ModelExpression != null)
            {
                if (Placeholder == null)
                    Placeholder = ModelExpression.Metadata.Placeholder;

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
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            base.Process(context, output);
            RenderFieldHeader(context, output);

            var id = GetDomId();

            output.Content.AppendHtml("<textarea data-target=\"aiplugs-textarea.textarea\" data-action=\"keydown->aiplugs-textarea#updateHeight\" data-val=\"true\"");
            
            if (id != null) 
                output.Content.AppendHtml($"id=\"{id}\"");
            
            if (Name != null)
                output.Content.AppendHtml($"name=\"{Name}\"");

            if (Placeholder != null)
                output.Content.AppendHtml($"placeholder=\"{Placeholder}\"");

            if (Required)
                output.Content.AppendHtml($"required data-val-required=\"{Localizer[SharedResource.VAL_MSG_REQUIRED, Label]}\"");

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

            output.Content.AppendHtml(">");
            output.Content.Append(Value??"");
            output.Content.AppendHtml("</textarea>");

            RenderFieldFooter(context, output, Name);
        }
    }
}
