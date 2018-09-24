using System;
using System.Linq;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Aiplugs.Elements
{
    public static class TagHelperExtensions
    {
        public static void Merge(this TagHelperAttributeList attributes, string name, string value)
        {
            var attr = attributes[name];
            var _value = (value + " " + attr?.Value ?? "").Trim();

            if (attr != null)
                attributes.Remove(attr);
            
            attributes.Add(name, _value);
        }

        public static void Text(this TagHelperOutput output, string text)
        {
            output.Content.Append(text);
        }

        public static void Html(this TagHelperOutput output, string html)
        {
            output.Content.AppendHtml(html);
        }

        public static void HtmlLine(this TagHelperOutput output, string html)
        {
            output.Content.AppendHtml(html + "\n");
        }

        public static void Attr(this TagHelperOutput output, string key)
        {
            output.Content.Append($" {key}");
        }

        public static void Attr(this TagHelperOutput output, string key, string value)
        {
            output.Content.Append($" {key}");
            
            output.Content.AppendHtml("=\"");

            output.Content.Append(value);
            
            output.Content.AppendHtml("\"");
        }

        private static string[] _selfCloseTags = new[] {"input", "img"};
        public static void Tag(this TagHelperOutput output, string tagName, string content = "")
        {
            output.Content.AppendHtml($"<{tagName}>");
                    
            if (!_selfCloseTags.Contains(tagName)) 
            {
                output.Content.Append(content);
                output.Content.AppendHtml($"</{tagName}>");
            }
        }

        public static void Tag(this TagHelperOutput output, string tagName, Action attr, string content = "")
        {
            output.Content.AppendHtml($"<{tagName} ");

            attr?.Invoke();

            output.Content.AppendHtml(">");
            
            if (!_selfCloseTags.Contains(tagName)) 
            {
                output.Content.Append(content);
                output.Content.AppendHtml($"</{tagName}>");
            }
        }
        
        public static void Tag(this TagHelperOutput output, string tagName, Action attr, Action content)
        {
            output.Content.AppendHtml($"<{tagName} ");

            attr?.Invoke();

            output.Content.AppendHtml(">");
            
            content?.Invoke();
            
            if (!_selfCloseTags.Contains(tagName))
                output.Content.AppendHtml($"</{tagName}>");
        }
    }
}