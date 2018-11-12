using System.Collections.Generic;
using Aiplugs.Elements.Extensions;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Localization;

namespace Aiplugs.Elements
{
    [HtmlTargetElement("aiplugs-dictionary")]
    public class Dictionary : AiplugsField
    {
        public override string ElementName => "aiplugs-dictionary";
        public string PatternKey { get; set; }
        public string PatternValue { get; set; }
        public string PlaceholderKey { get; set; }
        public string PlaceholderValue { get; set; }
        public IDictionary<string, string> Value { get; set; }
        public Dictionary(IStringLocalizer<SharedResource> localizer) : base(localizer)
        {
        }

        protected override void ExtractFromModelExpression()
        {
            base.ExtractFromModelExpression();

            if (ModelExpression != null)
            {
                if (Value == null)
                    Value = ModelExpression.ModelExplorer.Model as IDictionary<string, string>;
                
            }
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            base.Process(context, output);
            RenderFieldHeader(context, output);

            output.Attributes.Add("data-aiplugs-dictionary-regex-key", Localizer.MsgValPatternKey(Label, PatternKey));
            output.Attributes.Add("data-aiplugs-dictionary-regex-value", Localizer.MsgValPatternValue(Label, PatternValue));
            output.Attributes.Add("data-aiplugs-dictionary-regex-key-pattern", PatternKey);
            output.Attributes.Add("data-aiplugs-dictionary-regex-value-pattern", PatternValue);

            output.Html("<div data-target=\"aiplugs-dictionary.items\">");
            output.Html("<template data-target=\"aiplugs-dictionary.template\">");
            RenderItem(context, output);
            output.Html("</template>");
            if (Value != null)
            {
                foreach (var item in Value)
                {
                    RenderItem(context, output, item);
                }
            }
            output.Html("</div>");
            
            output.Tag("p", () => {
                output.Attr("class", "aiplugs-dictionary__message");
                output.Attr("data-target", "aiplugs-dictionary.message");
            });
            output.Html("<button type=\"button\" class=\"aiplugs-dictionary__add\" data-action=\"aiplugs-dictionary#add\"><i class=\"fa fa-plus\"></i></button>");
        }

        public void RenderItem(TagHelperContext context, TagHelperOutput output, KeyValuePair<string, string> item = default(KeyValuePair<string, string>))
        {
            output.Tag("div", () => {
                output.Attr("class", "aiplugs-dictionary__item");
                output.Attr("data-target", "aiplugs-dictionary.item");
                output.Attr("data-controller", "aiplugs-dictionary-item");
                
                if (Name != null)
                    output.Attr("data-aiplugs-dictionary-item-name", Name);
                
            }, () => {
                
                output.Tag("input", () => {
                    output.Attr("class", "aiplugs-dictionary__item-key");
                    output.Attr("data-target", "aiplugs-dictionary-item.itemKey");
                    output.Attr("data-action", "input->aiplugs-dictionary#validate");
                    // output.Attr("data-val", "true");

                    if (PatternKey != null)
                    {
                        output.Attr("pattern", PatternKey);
                        output.Attr("data-val-regex", Localizer.MsgValPatternKey(Label, PatternKey));
                        output.Attr("data-val-regex-pattern", PatternKey);
                    }

                    if (PlaceholderKey != null)
                        output.Attr("placeholder", PlaceholderKey);

                    if (item.Key != null) 
                        output.Attr("value", item.Key);
                });
                
                output.Tag("input", () => {
                    output.Attr("class", "aiplugs-dictionary__item-value");
                    output.Attr("data-target", "aiplugs-dictionary-item.itemValue");
                    output.Attr("data-action", "aiplugs-dictionary#validate invalid->aiplugs-dictionary#validate");
                    // output.Attr("data-val", "true");

                    if (PatternValue != null)
                    {
                        output.Attr("pattern", PatternValue);
                        output.Attr("data-val-regex", Localizer.MsgValPatternValue(Label, PatternValue));
                        output.Attr("data-val-regex-pattern", PatternValue);
                    }

                    if (PlaceholderValue != null)
                        output.Attr("placeholder", PlaceholderValue);

                    if (item.Value != null)
                        output.Attr("value", item.Value.ToString());

                    if (Name != null && item.Key != null)
                        output.Attr("name", $"{Name}[{item.Key}]"); 

                });
                
                output.Html("<button class=\"aiplugs-dictionary__remove\" data-action=\"aiplugs-dictionary-item#remove\">&times;</button>");
            });
        }
    }
}
