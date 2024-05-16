using System.Text.RegularExpressions;

namespace Afs.Translator.FunTranslations
{
    public static class Constants
    {
        public const int MinLenTextToTranslate = 1;
        public const int MaxLenTextToTranslate = 500;
        private const string _AnyNewlineRegex = @"\n|\r";
        public static readonly Regex AnyNewlineRegexFormat = new (_AnyNewlineRegex, RegexOptions.Compiled);
        public const int MinLenTranslation = 1;
        public const int MaxLenTranslation = 50;
    }
}
