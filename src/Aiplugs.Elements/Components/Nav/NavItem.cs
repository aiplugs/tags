using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Razor;

namespace Aiplugs.Elements
{
    [HtmlTargetElement("aiplugs-nav-item")]
    public class AiplugsNavItem : TagHelper
    {
        private readonly IUrlHelperFactory  _urlHelperFactory;
        private readonly IActionContextAccessor  _actionContextAccesor;
        public string Icon { get; set; }
        public string Href { get; set; }
        public AiplugsNavItem(IUrlHelperFactory urlHelperFactory, IActionContextAccessor  actionContextAccesor)
        {
            _urlHelperFactory = urlHelperFactory;
            _actionContextAccesor = actionContextAccesor;
        }
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var urlHelper = _urlHelperFactory.GetUrlHelper(_actionContextAccesor.ActionContext);
            var content = await output.GetChildContentAsync();
            output.TagName = "a";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Attributes.Merge("class", "aiplugs-nav-item");
            
            if (!string.IsNullOrEmpty(Href))
            {
                output.Attributes.Add("href", urlHelper.Content(Href));
            }
            
            output.Content.AppendHtml("<span class=\"left\">");
            if (!string.IsNullOrEmpty(Icon))
            {
                if (Icon.Contains("/") || Icon.Contains(".") || Icon.StartsWith("~/"))
                {
                    output.Content.AppendHtml($"<img src=\"{urlHelper.Content(Icon)}\">");
                }
                else
                {
                    output.Content.AppendHtml($"<i class=\"{Icon}\"></i>");
                }
            }
            output.Content.AppendHtml("</span>");

            output.Content.AppendHtml("<span class=\"right\">");
            output.Content.AppendHtml(content);
            output.Content.AppendHtml("</span>");
        }
    }
}