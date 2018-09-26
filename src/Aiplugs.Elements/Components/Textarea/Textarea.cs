using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Localization;

namespace Aiplugs.Elements
{
    [HtmlTargetElement("aiplugs-textarea")]
    public class AiplugsTextarea : AiplugsField
    {
        public override string ElementName => "aiplugs-textarea";
        public string Placeholder { get; set; }
        public string Value { get; set; }
        public AiplugsTextarea(IStringLocalizer<SharedResource> localizer) : base(localizer)
        {
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            base.Process(context, output);

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

            output.Content.AppendHtml("<p class=\"aiplugs-select__message field-validation-valid\" data-valmsg-replace=\"true\"");

            if (Name != null)
                output.Content.AppendHtml($"data-valmsg-for=\"{Name}\"");
            
            output.Content.AppendHtml("></p>");
        }
    }
}
