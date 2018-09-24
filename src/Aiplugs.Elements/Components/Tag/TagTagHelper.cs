using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Localization;

namespace Aiplugs.Elements
{
    [HtmlTargetElement("aiplugs-tag")]
    public class TagTagHelper : BaseInputTagHelper
    {
        public override string ElementName => "aiplugs-tag";
        public string Pattern { get; set; }
        public string PatternErrorMessage { get; set; }
        public string[] Value { get; set; }
        public int? MaxLength { get; set; }
        public int? MinLength { get; set; }
        public bool IgnoreCase { get; set; }
        public Ajax Ajax { get; set;}
        public IEnumerable<SelectListItem> Selection { get; set; }
        public TagTagHelper(IStringLocalizer<SharedResource> localizer) : base(localizer)
        {
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            base.Process(context, output);

            var id = GetDomId();
            var datalistId = "suggestion-" + id ?? Guid.NewGuid().ToString(); 
            var name = Name != null ? Name + "[]" : null;

            output.Attributes.Add("data-aiplugs-tag-ignore-case", IgnoreCase.ToString().ToLower());
            if (Ajax != null) 
            {
                output.Attributes.Add($"data-aiplugs-tag-ajax-url", Ajax.Url ?? "");
                if (Ajax.Headers != null)
                {
                    foreach(var kv in Ajax.Headers)
                    {
                        output.Attributes.Add($"data-aiplugs-tag-ajax-headers-{kv.Key}",  kv.Value);
                    }
                }
                output.Attributes.Add("data-aiplugs-tag-ajax-label", Ajax.Label ?? "label");
                output.Attributes.Add("data-aiplugs-tag-ajax-value", Ajax.Value ?? "value");
            }

            output.Tag("p", () => {
                output.Attr("class", "aiplugs-tag__message field-validation-valid");
                if (Name != null)
                {
                    output.Attr("data-valmsg-replace", "true");
                    output.Attr("data-valmsg-for", Name + "[]");
                }
            });

            output.Tag("div", () => {
                output.Attr("class", "aiplugs-tag__values");
                output.Attr("data-target", "aiplugs-tag.items");
            }, () => {
                RenderInput(context, output, name, target:false);
                output.Html("<template data-target=\"aiplugs-tag.template\">");
                RenderValue(context, output, name);
                output.Html("</template>");
                foreach(var value in Value ?? new string[0])
                {
                    RenderValue(context, output, name, value);
                }

                output.Tag("input", () => {
                    output.Attr("list", datalistId);
                    output.Attr("class", "aiplugs-tag__input");
                    output.Attr("data-target", "aiplugs-tag.input");
                    output.Attr("data-action", "keydown->aiplugs-tag#onKeydown");                    
                });
                
                if (Ajax != null)
                {
                    output.Html($"<datalist id=\"{datalistId}\" data-target=\"aiplugs-tag.suggestion\"></datalist>");
                }
            });
        }
        public void RenderValue(TagHelperContext context, TagHelperOutput output, string name, string value = null)
        {
            output.Tag("span", () => { 
                output.Attr("class", "aiplugs-tag__item");
                output.Attr("data-controller", "aiplugs-tag-item");
                output.Attr("data-target", "aiplugs-tag.item");
            }, () => {
                var label = Selection?.Where(s => s.Value == value).FirstOrDefault()?.Text ?? value;
                output.Tag("span", () => {
                    output.Attr("data-target", "aiplugs-tag-item.label");
                }, label);
                output.Html("<button type=\"button\" class=\"aiplugs-tag__item-close\" data-action=\"aiplugs-tag-item#remove\">&times;</button>");
                RenderInput(context, output, name, value);
            });
        }
        public void RenderInput(TagHelperContext context, TagHelperOutput output, string name, string value = null, bool target = true)
        {
            output.Tag("input", () => {

                output.Attr("type", "checkbox");
                output.Attr("data-val", "true");

                if (name != null)
                    output.Attr("name", name);

                if (value != null)
                    output.Attr("value", value);

                if (Pattern != null) 
                {
                    var message = !string.IsNullOrEmpty(PatternErrorMessage) ? PatternErrorMessage : Localizer.MsgValPattern(Label, Pattern);
                    output.Attr("data-val-regex", message);
                    output.Attr("data-val-regex-pattern", Pattern);
                }

                if (MaxLength.HasValue)
                {
                    output.Attr("data-val-select-maxlength", Localizer.MsgValMaxLengthForArray(Label, MaxLength.Value));
                    output.Attr("data-val-select-maxlength-max", MaxLength.ToString());
                }

                if (MinLength.HasValue)
                {
                    output.Attr("data-val-select-minlength", Localizer.MsgValMinLengthForArray(Label, MinLength.Value));
                    output.Attr("data-val-select-minlength-min", MinLength.ToString());
                }

                if (!MinLength.HasValue && Required)
                {
                    output.Attr("data-val-select-minlength", Localizer.MsgValRequired(Label));
                    output.Attr("data-val-select-minlength-min", "1");
                }

                if (target)
                {
                    output.Attr("data-target", "aiplugs-tag-item.input");
                    output.Html(" checked ");
                }
                else {
                    output.Attr("class", "aiplugs-tag__dummy");
                }
            });
        }
    }
}
