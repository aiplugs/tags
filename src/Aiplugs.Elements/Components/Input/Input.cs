using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Aiplugs.Elements.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;
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

        protected override void ExtractFromModelExpression()
        {
            base.ExtractFromModelExpression();

            if (ModelExpression != null)
            {
                if (Type == null)
                {
                    Type = GetInputType(ModelExpression.ModelExplorer, out var _);
                }

                if (Value == null)
                {
                    Value = (string)GetModelStateValue(ViewContext, Name, typeof(string)) ?? ModelExpression.ModelExplorer.Model?.ToString();
                }

                if (Placeholder == null)
                {
                    Placeholder = ModelExpression.Metadata.Placeholder;
                }

                foreach (var attribute in ModelExpression.ModelExplorer.Metadata.ValidatorMetadata)
                {
                    if (attribute is RegularExpressionAttribute regularExpressionAttribute)
                    {
                        if (Pattern == null)
                            Pattern = regularExpressionAttribute.Pattern;
                        
                        if (PatternErrorMessage == null)
                            PatternErrorMessage = regularExpressionAttribute.ErrorMessage;
                    }

                    else if (attribute is RangeAttribute rangeAttribute)
                    {
                        if (Min == null)
                            Min = Convert.ToDouble(rangeAttribute.Minimum);
                        
                        if (Max == null)
                            Max = Convert.ToDouble(rangeAttribute.Maximum);
                    }

                    else if (attribute is StringLengthAttribute stringLengthAttribute)
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
                    output.Attr("data-val-minlength-min", MinLength.ToString());
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

        #region https://github.com/aspnet/Mvc/blob/a67d9363e22be8ef63a1a62539991e1da3a6e30e/src/Microsoft.AspNetCore.Mvc.TagHelpers/InputTagHelper.cs
        // Mapping from datatype names and data annotation hints to values for the <input/> element's "type" attribute.
        private static readonly Dictionary<string, string> _defaultInputTypes =
            new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "HiddenInput", InputType.Hidden.ToString().ToLowerInvariant() },
                { "Password", InputType.Password.ToString().ToLowerInvariant() },
                { "Text", InputType.Text.ToString().ToLowerInvariant() },
                { "PhoneNumber", "tel" },
                { "Url", "url" },
                { "EmailAddress", "email" },
                { "Date", "date" },
                { "DateTime", "datetime-local" },
                { "DateTime-local", "datetime-local" },
                { nameof(DateTimeOffset), "text" },
                { "Time", "time" },
                { "Week", "week" },
                { "Month", "month" },
                { nameof(Byte), "number" },
                { nameof(SByte), "number" },
                { nameof(Int16), "number" },
                { nameof(UInt16), "number" },
                { nameof(Int32), "number" },
                { nameof(UInt32), "number" },
                { nameof(Int64), "number" },
                { nameof(UInt64), "number" },
                { nameof(Single), InputType.Text.ToString().ToLowerInvariant() },
                { nameof(Double), InputType.Text.ToString().ToLowerInvariant() },
                { nameof(Boolean), InputType.CheckBox.ToString().ToLowerInvariant() },
                { nameof(Decimal), InputType.Text.ToString().ToLowerInvariant() },
                { nameof(String), InputType.Text.ToString().ToLowerInvariant() },
                { nameof(IFormFile), "file" },
                { TemplateRenderer.IEnumerableOfIFormFileName, "file" },
            };

        // Mapping from <input/> element's type to RFC 3339 date and time formats.
        private static readonly Dictionary<string, string> _rfc3339Formats =
            new Dictionary<string, string>(StringComparer.Ordinal)
            {
                { "date", "{0:yyyy-MM-dd}" },
                { "datetime", @"{0:yyyy-MM-ddTHH\:mm\:ss.fffK}" },
                { "datetime-local", @"{0:yyyy-MM-ddTHH\:mm\:ss.fff}" },
                { "time", @"{0:HH\:mm\:ss.fff}" },
            };
        /// <summary>
        /// Gets an &lt;input&gt; element's "type" attribute value based on the given <paramref name="modelExplorer"/>
        /// or <see cref="InputType"/>.
        /// </summary>
        /// <param name="modelExplorer">The <see cref="ModelExplorer"/> to use.</param>
        /// <param name="inputTypeHint">When this method returns, contains the string, often the name of a
        /// <see cref="ModelMetadata.ModelType"/> base class, used to determine this method's return value.</param>
        /// <returns>An &lt;input&gt; element's "type" attribute value.</returns>
        protected string GetInputType(ModelExplorer modelExplorer, out string inputTypeHint)
        {
            foreach (var hint in GetInputTypeHints(modelExplorer))
            {
                if (_defaultInputTypes.TryGetValue(hint, out var inputType))
                {
                    inputTypeHint = hint;
                    return inputType;
                }
            }

            inputTypeHint = InputType.Text.ToString().ToLowerInvariant();
            return inputTypeHint;
        }
        // A variant of TemplateRenderer.GetViewNames(). Main change relates to bool? handling.
        private static IEnumerable<string> GetInputTypeHints(ModelExplorer modelExplorer)
        {
            if (!string.IsNullOrEmpty(modelExplorer.Metadata.TemplateHint))
            {
                yield return modelExplorer.Metadata.TemplateHint;
            }

            if (!string.IsNullOrEmpty(modelExplorer.Metadata.DataTypeName))
            {
                yield return modelExplorer.Metadata.DataTypeName;
            }

            // In most cases, we don't want to search for Nullable<T>. We want to search for T, which should handle
            // both T and Nullable<T>. However we special-case bool? to avoid turning an <input/> into a <select/>.
            var fieldType = modelExplorer.ModelType;
            if (typeof(bool?) != fieldType)
            {
                fieldType = modelExplorer.Metadata.UnderlyingOrModelType;
            }

            foreach (var typeName in TemplateRenderer.GetTypeNames(modelExplorer.Metadata, fieldType))
            {
                yield return typeName;
            }
        }
        #endregion
    }
    public class Ajax 
    {
        public string Url { get; set; }
        public IDictionary<string, string> Headers { get; set; }
        public string Label { get; set; }
        public string Value { get; set; }
    }
}
