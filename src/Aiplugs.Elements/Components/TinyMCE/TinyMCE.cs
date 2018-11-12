using System;
using System.Threading.Tasks;
using Aiplugs.Elements.Extensions;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Localization;

namespace Aiplugs.Elements
{
    [HtmlTargetElement("aiplugs-tinymce")]
    public class AiplugsTinyMCE : TagHelper
    {
        private readonly IUrlHelperFactory  _urlHelperFactory;
        private readonly IActionContextAccessor  _actionContextAccesor;
        public AiplugsTinyMCE(IUrlHelperFactory urlHelperFactory, IActionContextAccessor  actionContextAccesor)
        {
            _urlHelperFactory = urlHelperFactory;
            _actionContextAccesor = actionContextAccesor;
        }
        public string Name { get; set; }
        public string Value { get; set; }
        public string ValueFrom { get; set; }
        public string SettingsFrom { get; set; }
        public string ModalImage { get; set; }
        public string ModalVideo { get; set; }
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var urlHelper = _urlHelperFactory.GetUrlHelper(_actionContextAccesor.ActionContext);
            
            var id = Guid.NewGuid().ToString();
            output.TagName = "div";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Attributes.Merge("class", "aiplugs-tinymce");
            output.Attributes.Add("data-controller", "aiplugs-tinymce");
            output.Attributes.Add("data-aiplugs-tinymce-value-from", ValueFrom);
            output.Attributes.Add("data-aiplugs-tinymce-settings-from", SettingsFrom);

            var content = await output.GetChildContentAsync();

            output.Tag("textarea", () => {
                output.Attr("id", id);

                if (Name != null)
                    output.Attr("name", Name);

                output.Attr("data-target", "aiplugs-tinymce.textarea");
            }, Value??"");

            output.Tag("button", () => {
                output.Attr("type", "button");
                output.Attr("class", "aiplugs-tinymce__image");
                output.Attr("data-target", "aiplugs-tinymce.insertImage");
                output.Attr("ic-target", "body");
                output.Attr("ic-swap-style", "append");

                if (!string.IsNullOrEmpty(ModalImage)) 
                {
                    var callback = $"_insert_image_{id}";
                    var from = urlHelper.Content(ModalImage);
                    from += from.Contains("?") ? "&" : "?";
                    from += $"callback={callback}";

                    output.Attr("ic-get-from", from);
                }
            });      

            output.Tag("button", () => {
                output.Attr("type", "button");
                output.Attr("class", "aiplugs-tinymce__video");
                output.Attr("data-target", "aiplugs-tinymce.insertVideo");
                output.Attr("ic-target", "body");
                output.Attr("ic-swap-style", "append");
                
                if (!string.IsNullOrEmpty(ModalVideo)) 
                {
                    var callback = $"_insert_video_{id}";
                    var from = urlHelper.Content(ModalVideo);
                    from += from.Contains("?") ? "&" : "?";
                    from += $"callback={callback}";

                    output.Attr("ic-get-from", from);
                }
            });          

            output.Content.AppendHtml(content);
        }
    }
}
