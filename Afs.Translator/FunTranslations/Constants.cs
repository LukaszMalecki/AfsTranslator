using System.Text.RegularExpressions;
using static System.Net.WebRequestMethods;

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
        public const string BaseUrl = @"https://api.funtranslations.com/translate";
        public const string TargetFileFormat = ".json";
        public const string QueryTextName = "text";
        public const int IncorrectTranslationStatusCode = 404;
    }
}
