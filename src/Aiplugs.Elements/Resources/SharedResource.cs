using Microsoft.Extensions.Localization;

namespace Aiplugs.Elements
{
    public class SharedResource
    {
        public const string LABEL_OPTIONAL = "Label::Optional";
        public const string LABEL_REQUIRED = "Label::Required";
        public const string VAL_MSG_REQUIRED = "Message::Validation::Required {0}";
        public const string MSG_VAL_EMAIL = "Message::Validation::Email {0}";
        public const string MSG_VAL_NUMBER = "Message::Validation::Number {0}";
        public const string VAL_MSG_PATTERN = "Message::Validation::Pattern {0} {1}";
        public const string VAL_MSG_PATTERN_KEY = "Message::Validation::Pattern.Key {0} {1}";
        public const string VAL_MSG_PATTERN_VALUE = "Message::Validation::Pattern.Value {0} {1}";
        public const string VAL_MSG_MAX_LENGTH = "Message::Validation::MaxLength.String {0} {1}";
        public const string VAL_MSG_MIN_LENGTH = "Message::Validation::MinLength.String {0} {1}";
        public const string VAL_MSG_MAX_VALUE = "Message::Validation::MaxValue {0} {1}";
        public const string VAL_MSG_MIN_VALUE = "Message::Validation::MinValue {0} {1}";
        public const string VAL_MSG_RANGE = "Message::Validation::Range {0} {1} {2}";
        public const string MSG_VAL_MAX_LENGTH_ARRAY = "Message::Validation::MaxLength.Array {0} {1}";
        public const string MSG_VAL_MIN_LENGTH_ARRAY = "Message::Validation::MinLength.Array {0} {1}";
        
    }

    public static class SharedResourceLocalizerExtensions
    {
        public static string LabelOptional(this IStringLocalizer<SharedResource> localzer) => localzer[SharedResource.LABEL_OPTIONAL];
        public static string LabelRequired(this IStringLocalizer<SharedResource> localzer) => localzer[SharedResource.LABEL_REQUIRED];
        public static string MsgValRequired(this IStringLocalizer<SharedResource> localzer, string label) => localzer[SharedResource.VAL_MSG_REQUIRED, label];
        public static string MsgValEmail(this IStringLocalizer<SharedResource> localzer, string label) => localzer[SharedResource.MSG_VAL_EMAIL, label];
        public static string MsgValNumber(this IStringLocalizer<SharedResource> localzer, string label) => localzer[SharedResource.MSG_VAL_NUMBER, label];
        public static string MsgValPattern(this IStringLocalizer<SharedResource> localzer, string label, string pattern) => localzer[SharedResource.VAL_MSG_PATTERN, label, pattern];
        public static string MsgValPatternKey(this IStringLocalizer<SharedResource> localzer, string label, string pattern) => localzer[SharedResource.VAL_MSG_PATTERN_KEY, label, pattern];
        public static string MsgValPatternValue(this IStringLocalizer<SharedResource> localzer, string label, string pattern) => localzer[SharedResource.VAL_MSG_PATTERN_VALUE, label, pattern];
        public static string MsgValMaxLengthForString(this IStringLocalizer<SharedResource> localzer, string label, int maxLen) => localzer[SharedResource.VAL_MSG_MAX_LENGTH, label, maxLen];
        public static string MsgValMinLengthForString(this IStringLocalizer<SharedResource> localzer, string label, int minLen) => localzer[SharedResource.VAL_MSG_MIN_LENGTH, label, minLen];
        public static string MsgValMaxLengthForArray(this IStringLocalizer<SharedResource> localzer, string label, int maxLen) => localzer[SharedResource.MSG_VAL_MAX_LENGTH_ARRAY, label, maxLen];
        public static string MsgValMinLengthForArray(this IStringLocalizer<SharedResource> localzer, string label, int minLen) => localzer[SharedResource.MSG_VAL_MIN_LENGTH_ARRAY, label, minLen];
        public static string MsgValMaxValue(this IStringLocalizer<SharedResource> localzer, string label, double max) => localzer[SharedResource.VAL_MSG_MAX_VALUE, label, max];
        public static string MsgValMinValue(this IStringLocalizer<SharedResource> localzer, string label, double min) => localzer[SharedResource.VAL_MSG_MIN_VALUE, label, min];
        public static string MsgValRange(this IStringLocalizer<SharedResource> localzer, string label, double min, double max) => localzer[SharedResource.VAL_MSG_MIN_VALUE, label, min, max];
    }
}
