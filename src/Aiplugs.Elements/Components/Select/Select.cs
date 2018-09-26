using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        public AiplugsSelect(IStringLocalizer<SharedResource> localizer) : base(localizer)
        {
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
            var name  = Multiple ? Name.ToArraySuffix() : Name;
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
                        output.Attr("data-val-select-maxlength", Localizer.MsgValMaxLengthForArray(Label, MaxLength.Value));
                        output.Attr("data-val-select-maxlength-max", MaxLength.ToString());
                    }

                    if (MinLength.HasValue)
                    {
                        output.Attr("data-val-select-minlength", Localizer.MsgValMinLengthForArray(Label, MinLength.Value));
                        output.Attr("data-val-select-minlength-min", MinLength.ToString());
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

            RenderFieldFooter(context, output, Name?.ToArraySuffix());

            foreach(var item in selection)
            {
                output.Html("<label class=\"aiplugs-select__checkbox\">");

                output.Tag("input", () => {
                    
                    output.Attr("type", "checkbox");
                    output.Attr("data-target", "aiplugs-select.checkbox");
                    output.Attr("data-action", "change->aiplugs-select#update");
                    output.Attr("data-val", "true");

                    if (Name != null)
                        output.Attr("name", Name?.ToArraySuffix());
                    
                    if (item.Selected)
                        output.Html(" checked ");
                    
                    if (item.Disabled)
                        output.Html(" disabled ");

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
                        output.Attr("data-val-select-minlength", Localizer.MsgValRequired(Label));
                        output.Attr("data-val-select-minlength-min", "1");
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
