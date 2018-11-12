using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Aiplugs.Elements.Extensions;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Localization;

namespace Aiplugs.Elements
{
    [HtmlTargetElement("aiplugs-select")]
    public class AiplugsSelect : AiplugsField
    {
        public override string ElementName => "aiplugs-select";
        public string Type { get; set; } 
        public bool Multiple { get; set; }
        public int? MaxLength { get; set; }
        public int? MinLength { get; set; }
        public IEnumerable<SelectListItem> Selection { get; set; }
        private readonly IHtmlHelper _htmlHelper;

        public AiplugsSelect(IStringLocalizer<SharedResource> localizer, IHtmlHelper htmlHelper) : base(localizer)
        {
            _htmlHelper = htmlHelper;
        }
        
        protected override void ExtractFromModelExpression()
        {
            base.ExtractFromModelExpression();

            if (ModelExpression != null)
            {
                foreach (var attribute in ModelExpression.ModelExplorer.Metadata.ValidatorMetadata)
                {
                    if (MaxLength == null && attribute is MaxLengthAttribute maxLengthAttribute)
                        MaxLength = maxLengthAttribute.Length;

                    else if (MinLength == null && attribute is MinLengthAttribute minLengthAttribute)
                        MinLength = minLengthAttribute.Length;
                }
                var fieldType = ModelExpression.ModelExplorer.ModelType;
                Multiple = fieldType.IsMultiple();

                if (Selection == null)
                {
                    var itemType = Multiple ? fieldType.GetItemType() : fieldType;
                    var isNullable = false;
                    if (itemType.IsGenericType && itemType.GetGenericTypeDefinition() == typeof(Nullable<>))
                    {
                        itemType = Nullable.GetUnderlyingType(itemType);
                        isNullable = true;
                    }

                    if (itemType.IsEnum)
                    {
                        Selection = _htmlHelper.GetEnumSelectList(itemType);
                        Required = !isNullable;
                    }

                    if (!Required)
                       Selection = (new [] {new SelectListItem()}).Concat(Selection ?? Enumerable.Empty<SelectListItem>());

                    if (ModelExpression.ModelExplorer.Model != null)
                    { 
                        var model = ModelExpression.ModelExplorer.Model;
                        var values = Multiple ? (itemType.IsEnum ? ((IEnumerable<int>)model).Select(n => n.ToString())
                                                                 : ((IEnumerable<object>)model).Select(n => n.ToString()))
                                              : new []{(itemType.IsEnum ? ((int)model).ToString() : model.ToString())};
                        foreach(var option in Selection)
                        {
                            option.Selected = values.Contains(option.Value);
                        }
                    }
                }
            }
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            base.Process(context, output);
            RenderFieldHeader(context, output);

            var type = GetInputType();

            if (type == InputType.Radio)
                RenderRadio(context, output);
            
            else if (type == InputType.Checkbox)
                RenderCheckbox(context, output);
            
            else
                RenderSelect(context, output);
        }
        public void RenderSelect(TagHelperContext context, TagHelperOutput output)
        {
            var id = GetDomId();
            var name  = Multiple ? Name.WithArraySuffix() : Name;
            var selection = Selection ?? new SelectListItem[0];

            output.Tag("select", ()=> {
                output.Attr("data-val", "true");

                if (id != null)
                    output.Attr("id", id);

                if (name != null)
                    output.Attr("name", name);

                if (Multiple)
                {
                    output.Html(" multiple ");
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
                }
                
                if (Required)
                {
                    output.Html(" required ");
                    output.Attr("data-val-required", Localizer.MsgValRequired(Label));
                }

            }, () => {
                foreach (var item in selection)
                {
                    output.Tag("option", () => {

                        output.Attr("value", item.Value);
                        
                        if (item.Selected)
                            output.Html(" selected");
                        
                        if (item.Disabled)
                            output.Html(" disabled");

                    }, item.Text);
                }
            });

            RenderFieldFooter(context, output, name);
        }
        public void RenderCheckbox(TagHelperContext context, TagHelperOutput output)
        {
            var id = GetDomId();
            var selection = Selection ?? new SelectListItem[0];

            RenderFieldFooter(context, output, Name?.WithArraySuffix());

            foreach(var item in selection)
            {
                output.Html("<label class=\"aiplugs-select__checkbox\">");

                output.Tag("input", () => {
                    
                    output.Attr("type", "checkbox");
                    output.Attr("data-target", "aiplugs-select.checkbox");
                    output.Attr("data-action", "change->aiplugs-select#update");
                    output.Attr("data-val", "true");

                    if (Name != null)
                        output.Attr("name", Name?.WithArraySuffix());
                    
                    if (item.Selected)
                        output.Html(" checked ");
                    
                    if (item.Disabled)
                        output.Html(" disabled ");

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
                    
                    if (item.Value != null)
                        output.Attr("value", item.Value);
                });

                
                output.Text(item.Text);
                output.Html("</label>");
            }
        }
        public void RenderRadio(TagHelperContext context, TagHelperOutput output)
        {
            var id = GetDomId();
            var selection = Selection ?? new SelectListItem[0];

            RenderFieldFooter(context, output, Name);

            foreach(var item in selection)
            {
                output.Html("<label class=\"aiplugs-select__radio\">");

                output.Tag("input", () => {

                    output.Attr("type", "radio");
                    output.Attr("data-target", "aiplugs-select.checkbox");
                    output.Attr("data-action", "change->aiplugs-select#update");
                    output.Attr("data-val", "true");

                    if (Name != null)
                        output.Attr("name", Name);
                    
                    if (item.Selected)
                        output.Html(" checked ");
                    
                    if (item.Disabled)
                        output.Html(" disabled ");

                    if (Required)
                    {
                        output.Attr("data-val-mincount", Localizer.MsgValRequired(Label));
                        output.Attr("data-val-mincount-min", "1");
                    }

                    if (item.Value != null)
                        output.Attr("value", item.Value);
                });

                output.Text(item.Text);
                output.Html("</label>");
            }
        }

        protected InputType GetInputType()
        {
            if (!Required)
                return InputType.Select;
            
            switch(Type?.ToLower())
            {
                case "select": return InputType.Select;
                case "checkbox": return InputType.Checkbox;
                case "radio": return InputType.Radio;
                default:
                    if (Selection != null && !Selection.Any(item => item.Value == null) && Selection.Count() <= 7)
                        return Multiple ? InputType.Checkbox : InputType.Radio;
                    
                    return InputType.Select;
            }
        }
        protected enum InputType
        {
            Select, Checkbox, Radio
        }
    }
}
