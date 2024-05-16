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
        public static string ExampleResponse = @"{
            ""success"": {
                ""total"": 1
            },
            ""contents"": {
                ""translated"": ""h3ll0 W0R1d!"",
                ""text"": ""Hello World!"",
                ""translation"": ""leetspeak""
            }
        }";
        public static FunTranslationsResponse ExampleDeserializedResponse = new FunTranslationsResponse()
        {
            Success = new Success()
            {
                Total = 1
            },
            Contents = new Contents()
            {
                Translated = "h3ll0 W0R1d!",
                Text = "Hello World!",
                Translation = "leetspeak"
            }
        };
        public static HttpResponseMessage ExampleSuccessResponseMessage = new HttpResponseMessage()
        {
            StatusCode = System.Net.HttpStatusCode.OK,
            Content = new StringContent(ExampleResponse, System.Text.Encoding.UTF8, "application/json")
        };
    }
}
