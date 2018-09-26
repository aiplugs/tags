using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Aiplugs.Elements
{
    [HtmlTargetElement("a", Attributes = "[is=aiplugs-button]")]
    [HtmlTargetElement("input", Attributes = "[is=aiplugs-button],[type=submit]")]
    [HtmlTargetElement("button", Attributes = "[is=aiplugs-button]")]
    public class AiplugsButton : TagHelper
    {
        public string ElementName => "aiplugs-button";

        public ButtonStyle Style { get; set; } = ButtonStyle.Default;
        public ButtonSize Size { get; set; } = ButtonSize.Default;
        public Color Color { get; set; } = Color.Default;

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Attributes.Merge("class", ElementName);
        }
    }

    public enum ButtonSize
    {
        Default,
        Small,
        Large
    }

    public enum ButtonStyle
    {
        Default,
        Block,
        Ghost
    }
}
