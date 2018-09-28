#r "System.Resources.ResourceManager"
#r "System.Windows.Forms"

using System.Linq;
using System.Collections.Generic;
using System.Resources;

var target = "SharedResource";
var i18n = new Dictionary<string, Dictionary<string, string>>
{
    { "Message::Confirm::Delete {0}", new Dictionary<string, string> 
        {
            { "", "Are you sure you want to delete {0} item(s)?" },
            { "ja", "選択された{0}個のアイテムを削除します。" },
        } },
    
    { "Label::Confirm::Delete::Yes", new Dictionary<string, string> 
        {
            { "", "Yes" },
            { "ja", "削除する" },
        } },
    
    { "Label::Confirm::Delete::No", new Dictionary<string, string> 
        {
            { "", "No" },
            { "ja", "考え直す" },
        } },
    
    { "Message::Confirm::Discard", new Dictionary<string, string> 
        {
            { "", "Are you sure you want to close without save changes of {0}?" },
            { "ja", "「{0}」の変更を破棄します。" },
        } },
    
    { "Label::Confirm::Discard::Yes", new Dictionary<string, string> 
        {
            { "", "Yes" },
            { "ja", "変更を破棄する" },
        } },
    
    { "Label::Confirm::Discard::No", new Dictionary<string, string> 
        {
            { "", "No" },
            { "ja", "考え直す" },
        } },

    { "Label::Optional", new Dictionary<string, string> 
        {
            { "", "(optional)" },
            { "ja", "（任意）" },
        } },
    
    { "Label::Required", new Dictionary<string, string> 
        {
            { "", "" },
            { "ja", "（必須）" },
        } },
    
    { "Message::Validation::Required {0}", new Dictionary<string, string> 
        {
            { "", "The field {0} is rquired." },
            { "ja", "「{0}」は必須項目です。" },
        } },
    
    { "Message::Validation::Email {0}", new Dictionary<string, string> 
        {
            { "", "The field {0} is not a valid email address." },
            { "ja", "「{0}」はメールアドレスとして正しいフォーマットではありません。" },
        } },
    
    { "Message::Validation::Number {0}", new Dictionary<string, string> 
        {
            { "", "The field {0} must be a number." },
            { "ja", "「{0}」は数値である必要があります。" },
        } },
    
    { "Message::Validation::Pattern {0} {1}", new Dictionary<string, string> 
        {
            { "", "The field {0} is not correct format: '{1}'." },
            { "ja", "「{0}」は次のフォーマットにマッチする必要があります。({1})" },
        } },
    
    { "Message::Validation::MaxLength.String {0} {1}", new Dictionary<string, string> 
        {
            { "", "The field {0} must be with a maximum length of '{1}'." },
            { "ja", "「{0}」は{1}文字より短い必要があります。" },
        } },
    
    { "Message::Validation::MinLength.String {0} {1}", new Dictionary<string, string> 
        {
            { "", "The field {0} must be with a minimum length of '{1}'." },
            { "ja", "「{0}」は{1}文字より長い必要があります。" },
        } },
    
    { "Message::Validation::MaxLength.Array {0} {1}", new Dictionary<string, string> 
        {
            { "", "The field {0} must be with a maximum length of '{1}'." },
            { "ja", "「{0}」は{1}個以下である必要があります。" },
        } },
    
    { "Message::Validation::MinLength.Array {0} {1}", new Dictionary<string, string> 
        {
            { "", "The field {0} must be with a minimum length of '{1}'." },
            { "ja", "「{0}」は{1}個以上である必要があります。" },
        } },
    
    { "Message::Validation::MaxValue {0} {1}", new Dictionary<string, string> 
        {
            { "", "The field {0} must be lesser than {1}." },
            { "ja", "「{0}」は{1}以下である必要があります。" },
        } },
    
    { "Message::Validation::MinValue {0} {1}", new Dictionary<string, string> 
        {
            { "", "The field {0} must be greater thant {1}." },
            { "ja", "「{0}」は{1}以上である必要があります。" },
        } },
    
    { "Message::Validation::Range {0} {1} {2}", new Dictionary<string, string> 
        {
            { "", "The field {0} must be between {1} and {2}." },
            { "ja", "「{0}」は{1}から{2}の間の値である必要があります。" },
        } },
};

#region Write out to resouce file.
if (!Directory.Exists("Resources"))
    Directory.CreateDirectory("Resources");

foreach (var group in i18n.SelectMany(msg => msg.Value.Select(pair => (lang: pair.Key, msg: msg.Key, text: pair.Value))).GroupBy(_ => _.lang))
{
    var file = group.Key == "" ? $"{target}.resx" : $"{target}.{group.Key}.resx";
    using(var writer = new ResXResourceWriter($"Resources/{file}"))
    {
        foreach(var item in group) 
        {
            writer.AddResource(item.msg, item.text);
        }
    }
}
#endregion