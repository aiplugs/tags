using System.Collections.Generic;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Localization;

namespace Aiplugs.Elements
{
    [HtmlTargetElement("aiplugs-input")]
    public class AiplugsInput : AiplugsField
    {
        public override string ElementName => "aiplugs-input";
        public string Type { get; set; }
        public string Value { get; set; }
        public string Placeholder { get; set; }
        public double? Step { get; set; }
        public double? Max { get; set; }
        public double? Min { get; set; }
        public int? MaxLength { get; set; }
        public int? MinLength { get; set; }
        public bool Readonly { get; set; }
        public string Pattern { get; set; }
        public string PatternErrorMessage { get; set; }
        public bool Unique { get; set; }
        public bool IgnoreCase { get; set; }
        public Ajax Ajax { get; set;}
        public AiplugsInput(IStringLocalizer<SharedResource> localizer) : base(localizer)
        {
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            base.Process(context, output);
            RenderFieldHeader(context, output);

            var id = GetDomId();
            var type = (Type??"text").ToLower().Trim();

            if (Ajax != null) 
            {
                output.Attributes.Add($"data-aiplugs-input-ajax-url", Ajax.Url ?? "");
                if (Ajax.Headers != null)
                {
                    foreach(var kv in Ajax.Headers)
                    {
                        output.Attributes.Add($"data-aiplugs-input-ajax-headers-{kv.Key}",  kv.Value);
                    }
                }
                output.Attributes.Add("data-aiplugs-input-ajax-label", Ajax.Label ?? "label");
                output.Attributes.Add("data-aiplugs-input-ajax-value", Ajax.Value ?? "value");
            }

            output.Attributes.Add("data-aiplugs-input-unique", Unique.ToString().ToLower());
            output.Attributes.Add("data-aiplugs-input-ignore-case", IgnoreCase.ToString().ToLower());

            output.Html("<div class=\"aiplugs-input__field\">");

            output.Tag("input", () => {

                output.Attr("type", type);
                output.Attr("data-target", "aiplugs-input.input");
                output.Attr("data-action", "input->aiplugs-input#onInput blur->aiplugs-input#onBlur");
                output.Attr("data-val", "true");
                
                if (id != null)
                {
                    output.Attr("id", id);
                    output.Attr("list", $"suggestion-{id}");
                }

                if (Name != null)
                    output.Attr("name", Name);
                
                if (Value != null)
                    output.Attr("value", Value);

                if (type == "email")
                    output.Attr("data-val-email", Localizer.MsgValEmail(Label));

                if (Required)
                {
                    output.Html(" required");
                    output.Attr("data-val-required", Localizer.MsgValRequired(Label));
                }

                if (!string.IsNullOrEmpty(Placeholder))
                    output.Attr("placeholder", Placeholder);

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
                    output.Attr("data-val-minlength-max", MinLength.ToString());
                }

                if (type == "number")
                {
                    output.Attr("data-val-number", Localizer.MsgValNumber(Label));

                    if (Max.HasValue || Min.HasValue)
                    {
                        var message = Max.HasValue && Min.HasValue ? Localizer.MsgValRange(Label, Min.Value, Max.Value)
                                    : Max.HasValue ? Localizer.MsgValMaxValue(Label, Max.Value)
                                    : Localizer.MsgValMinValue(Label, Min.Value);

                        output.Attr($"data-val-range", message);
                    }

                    if (Step.HasValue)
                        output.Html($"step=\"{Step}\"");
                    
                    if (Max.HasValue)
                        output.Html($"max=\"{Max}\" data-val-range-max=\"{Max}\"");

                    if (Min.HasValue)
                        output.Html($"min=\"{Min}\" data-val-range-min=\"{Min}\"");
                }

            });

            output.Html("</div>");

            if (id != null)
            {
                output.Tag("datalist", () => {
                    output.Attr("id", $"suggestion-{id}");
                    output.Attr("data-target", "aiplugs-input.suggestion");
                });
            }
            
            RenderFieldFooter(context, output, Name);
        }
    }
    public class Ajax 
    {
        public string Url { get; set; }
        public IDictionary<string, string> Headers { get; set; }
        public string Label { get; set; }
        public string Value { get; set; }
    }
}
