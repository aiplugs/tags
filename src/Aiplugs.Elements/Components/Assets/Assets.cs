using System;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Threading.Tasks;
using System.Linq;
using System.IO;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Aiplugs.Elements.Extensions;

namespace Aiplugs.Elements
{
    [HtmlTargetElement("aiplugs-assets")]
    public class AiplugsAssets: TagHelper
    {
        private readonly IUrlHelperFactory  _urlHelperFactory;
        private readonly IActionContextAccessor  _actionContextAccesor;
        public AiplugsAssets(IUrlHelperFactory urlHelperFactory, IActionContextAccessor  actionContextAccesor)
        {
            _urlHelperFactory = urlHelperFactory;
            _actionContextAccesor = actionContextAccesor;
        }
        
        [HtmlAttributeName("jquery")]
        public bool jQuery { get; set; } = true;
        private string _jquery => "~/lib/aiplugs-elements/lib/jquery/dist/jquery.min.js";

        [HtmlAttributeName("jquery-validation")]
        public bool jQueryValidation { get; set; } = true;
        private string _jqueryValidation => "~/lib/aiplugs-elements/lib/jquery-validation/dist/jquery.validate.min.js";
        private string _jqueryValidationEx => "~/lib/aiplugs-elements/lib/jquery-validation/dist/additional-methods.min.js";

        [HtmlAttributeName("jquery-validation-unobtrusive")]
        public bool jQueryValidationUnobtrusive { get; set; } = true;
        private string _jqueryValidationUnobtrusive => "~/lib/aiplugs-elements/lib/jquery-validation-unobtrusive/jquery.validate.unobtrusive.min.js";

        [HtmlAttributeName("intercooler")]
        public bool Intercooler { get; set; } = true;
        private string _intercooler => "https://cdnjs.cloudflare.com/ajax/libs/intercooler-js/1.2.1/intercooler.min.js";

        public bool Stimulus { get; set; } = true;
        private string _stimulus => "https://unpkg.com/stimulus/dist/stimulus.umd.js";

        public bool Monaco { get; set; } = true;
        private string _monaco => "https://unpkg.com/monaco-editor/min/vs/loader.js";

        [HtmlAttributeName("tinymce")]
        public bool TinyMCE { get; set; } = true;
        private string _tinymce => "~/lib/aiplugs-elements/lib/tinymce/js/tinymce/tinymce.min.js";

        public bool Highlight { get; set; } = true;
        private string _highligt => "https://cdnjs.cloudflare.com/ajax/libs/highlight.js/9.12.0/highlight.min.js";

        public string HighlightTheme { get; set; } = "atom-one-light";
        private string _highligtTheme => $"https://cdnjs.cloudflare.com/ajax/libs/highlight.js/9.12.0/styles/{HighlightTheme}.min.css";

        public bool FontAwesome { get; set; } = true;
        private string _fontawesome => "https://use.fontawesome.com/releases/v5.1.0/css/all.css";
        
        public bool GoogleFontRobot { get; set; } = true;
        public bool GoogleFontNoto { get; set; } = true;
        public bool GoogleFontNotoJa { get; set; } = true;
        public bool GoogleFontNotoKr { get; set; } = true;
        public bool GoogleFontMaterialIcon { get; set; } = true;
        private string _googleFontRobot => GoogleFontRobot ? "Robot" : null;
        private string _googleFontNoto => GoogleFontNotoJa ? "Noto+Sans" : null;
        private string _googleFontNotoJa => GoogleFontNotoJa ? "Noto+Sans+JP" : null;
        private string _googleFontNotoKr => GoogleFontNotoKr ? "Noto+Sans+KR" : null;
        private string _googleFontMaterialIcons => GoogleFontRobot ? "Material+Icons" : null;
        private string[] _googleFont => new [] {"Comfortaa", _googleFontRobot,_googleFontNoto,_googleFontNotoJa,_googleFontNotoKr,_googleFontMaterialIcons}.Where(_ => _ != null).ToArray();
        private string _googleFontUrl => $"https://fonts.googleapis.com/css?family={string.Join("|", _googleFont)}";

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = null;

            var urlHelper = _urlHelperFactory.GetUrlHelper(_actionContextAccesor.ActionContext);
            string style(string url) {
                return $"<link rel=\"stylesheet\" href=\"{urlHelper.Content(url)}\">";
            }
            string script(string url) {
                return $"<script type=\"text/javascript\" src=\"{urlHelper.Content(url)}\"></script>";
            }

            if (FontAwesome)
                output.HtmlLine(style(_fontawesome));

            if (Highlight)
                output.HtmlLine(style(_highligtTheme));
            
            output.HtmlLine(style(_googleFontUrl));
            output.HtmlLine(style("~/lib/aiplugs-elements/css/variables.css"));
            output.HtmlLine(style("~/lib/aiplugs-elements/css/reset.css"));
            output.HtmlLine(style("~/lib/aiplugs-elements/css/layout.css"));
            output.HtmlLine(style("~/lib/aiplugs-elements/css/style.css"));

            if (jQuery)
                output.HtmlLine(script(_jquery));
            
            if (jQueryValidation)
                output.HtmlLine(script(_jqueryValidation));

            if (jQueryValidationUnobtrusive)
            {
                output.HtmlLine(script(_jqueryValidationUnobtrusive));
                output.HtmlLine(script("~/lib/aiplugs-elements/js/unobtrusive.adapters.js"));
            }

            if (Intercooler)
                output.HtmlLine(script(_intercooler));

            if (Stimulus)
                output.HtmlLine(script(_stimulus));
            
            if (TinyMCE)
                output.HtmlLine(script(_tinymce));
            
            if (Monaco)
            {
                output.HtmlLine(script(_monaco));
                output.HtmlLine(script("~/lib/aiplugs-elements/js/monaco-settings.js"));
            }

            if (Highlight) 
            {
                output.HtmlLine(script(_highligt));
                output.HtmlLine("<script>hljs.initHighlightingOnLoad();</script>");
            }
                
            output.HtmlLine(script("~/lib/aiplugs-elements/js/script.js"));
        }
    }
}
