using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using Aiplugs.Elements.Extensions;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Localization;

namespace Aiplugs.Elements
{
    [HtmlTargetElement("aiplugs-tag")]
    public class AiplugsTag : AiplugsField
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
        private readonly IHtmlHelper _htmlHelper; 
        public AiplugsTag(IStringLocalizer<SharedResource> localizer, IHtmlHelper htmlHelper) : base(localizer)
        {
            _htmlHelper = htmlHelper;
        }

        protected override void ExtractFromModelExpression()
        {
            base.ExtractFromModelExpression();

            if (ModelExpression != null)
            {
                var isMultiple = ModelExpression.ModelExplorer.ModelType.IsMultiple();

                if (Value == null)
                    Value = (string[])GetModelStateValue(ViewContext, Name, typeof(string[]));

                if (Value == null && ModelExpression.ModelExplorer.Model != null && isMultiple)
                {
                    var itemType = ModelExpression.ModelExplorer.ModelType.GetItemType();
                    if (itemType.IsEnum)
                    {
                        Value = ((IEnumerable<int>)ModelExpression.ModelExplorer.Model).Select(value => value.ToString()).ToArray();
                    }
                    else
                    {
                        Value = ((IEnumerable<object>)ModelExpression.ModelExplorer.Model).Select(o => o.ToString()).ToArray();
                    }
                    
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
                    
                    else if (MaxLength == null && attribute is MaxLengthAttribute maxLengthAttribute)
                        MaxLength = maxLengthAttribute.Length;

                    else if (MinLength == null && attribute is MinLengthAttribute minLengthAttribute)
                        MinLength = minLengthAttribute.Length;
                }

                if (Selection == null && isMultiple)
                {
                    var itemType = ModelExpression.ModelExplorer.ModelType.GetItemType();
                    if (itemType.IsEnum)
                        Selection = _htmlHelper.GetEnumSelectList(itemType);

                    if (!Required)
                        Selection = (new [] {new SelectListItem()}).Concat(Selection ?? Enumerable.Empty<SelectListItem>());
                }
            }
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            Name = Name.WithArraySuffix();
            base.Process(context, output);

            if (!string.IsNullOrEmpty(Name))
                output.Attributes.Add($"data-{ElementName}-name", Name);

            RenderFieldHeader(context, output);

            var id = GetDomId();
            var datalistId = "suggestion-" + id ?? Guid.NewGuid().ToString();
            var name = Name;

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
            
            RenderFieldFooter(context, output, name);

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
                    output.Attr("class", "aiplugs-tag__input val-ignore");
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
                    output.Attr("data-val-maxcount", Localizer.MsgValMaxLengthForArray(Label, MaxLength.Value));
                    output.Attr("data-val-maxcount-max", MaxLength.ToString());
                }

                if (MinLength.HasValue)
                {
                    output.Attr("data-val-mincount", Localizer.MsgValMinLengthForArray(Label, MinLength.Value));
                    output.Attr("data-val-mincount-min", MinLength.ToString());
                }

                if (!MinLength.HasValue && Required)
                {
                    output.Attr("data-val-mincount", Localizer.MsgValRequired(Label));
                    output.Attr("data-val-mincount-min", "1");
                }

                if (target)
                {
                    if (name != null)
                        output.Attr("name", name);
                    output.Attr("data-target", "aiplugs-tag-item.input");
                    output.Html(" checked ");
                }
                else {
                    output.Attr("class", "aiplugs-tag__dummy val-ignore");
                }
            });
        }
    }
}
