using System.ComponentModel.DataAnnotations;
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

            output.Content.AppendHtml(">");
            output.Content.Append(Value??"");
            output.Content.AppendHtml("</textarea>");

            RenderFieldFooter(context, output, Name);
        }
    }
}
