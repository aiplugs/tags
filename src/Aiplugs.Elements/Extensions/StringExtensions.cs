namespace Aiplugs.Elements
{
    public static class StringExtensions
    {
        public static string ToArraySuffix(this string name)
        {
            if (name == null)
                return null;
            
            if (name.EndsWith("[]"))
                return name;
            
            return name + "[]";
        }
    }
}