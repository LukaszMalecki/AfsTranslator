using Afs.Translator.FunTranslations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Afs.Translator.Tests.Unit
{
    public static class TranslatorControllerTestsConstants
    {
        public const string DefaultTranslated = "h3ll0 W0R1d!";
        public const string DefaultText = "Hello World!";
        public const string DefaultTranslation = "leetspeak";
        public const int DefaultTranslationId = 1;
        public static readonly DateTime DefaultDateTime = new DateTime(2024, 5, 17);
        public static string DefaultResponse = @"{
            ""success"": {
                ""total"": 1
            },
            ""contents"": {
                ""translated"": ""h3ll0 W0R1d!"",
                ""text"": ""Hello World!"",
                ""translation"": ""leetspeak""
            }
        }";
        public static FunTranslationsResponse DefaultDeserializedResponse = new FunTranslationsResponse()
        {
            Success = new Success()
            {
                Total = 1
            },
            Contents = new Contents()
            {
                Translated = DefaultTranslated,
                Text = DefaultText,
                Translation = DefaultTranslation
            }
        };
        public static HttpResponseMessage DefaultSuccessResponseMessage = new HttpResponseMessage()
        {
            StatusCode = System.Net.HttpStatusCode.OK,
            Content = new StringContent(DefaultResponse, System.Text.Encoding.UTF8, "application/json")
        };
    }
}
