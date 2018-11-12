using System.Threading.Tasks;
using Aiplugs.Elements.Extensions;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Aiplugs.Elements
{
    [HtmlTargetElement("a", Attributes = "[is=aiplugs-button]")]
    [HtmlTargetElement("input", Attributes = "[is=aiplugs-button],[type=submit]")]
    [HtmlTargetElement("button", Attributes = "[is=aiplugs-button]")]
    public class AiplugsButton : TagHelper
    {
        public string ElementName => "aiplugs-button";

        public ButtonStyle Looks { get; set; } = ButtonStyle.@default;
        public ButtonSize Size { get; set; } = ButtonSize.@default;
        public Color Color { get; set; } = Color.@default;

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var style = Looks != ButtonStyle.@default ? "--" + Looks.ToString().ToLower() : "";
            var size =  Size  != ButtonSize.@default  ? "--" + Size.ToString().ToLower()  : "";
            var color = Color != Color.@default       ? "--" + Color.ToString().ToLower() : "";
            output.Attributes.Merge("class", $"{ElementName} {style} {size} {color}");
            if (output.Attributes.TryGetAttribute("is", out var attr)) {
                output.Attributes.Remove(attr);
            }
        }
    }

    public enum ButtonSize
    {
        @default,
        small,
        large,
        full
    }

    public enum ButtonStyle
    {
        @default,
        block
    }
}
