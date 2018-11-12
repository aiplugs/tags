namespace Aiplugs.Elements.Extensions
{
    public static class StringExtensions
    {
        public static string WithArraySuffix(this string name)
        {
            if (name == null)
                return null;
            
            if (name.EndsWith("[]"))
                return name;
            
            return name + "[]";
        }
    }
}