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

        public static string ToIcTarget(this string target)
            => target != null && target.StartsWith("aiplugs-") ? $"closest .{target}" : target;

        public static string ToIcTarget(this object target)
            => ToIcTarget(target?.ToString());
    }
}