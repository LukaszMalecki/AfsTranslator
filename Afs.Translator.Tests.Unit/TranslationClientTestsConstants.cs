using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Afs.Translator.FunTranslations;

namespace Afs.Translator.Tests.Unit
{
    public static class TranslationClientTestsConstants
    {
        public const string ValidTranslationPlaceholder = "placeholder";
        public const string Text500Char = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
            "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
            "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
            "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
            "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
            "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
            "aaaaaaaaaaaaaaaaaaaa";
        public const string Text501Char = Text500Char + "a";
        public const string Text1Char = "a";
        public const string Text0Char = "";
        public const string ValidTextToTranslate = "Hello everyone!";
        public const string Text50Char = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
        public const string Text51Char = Text50Char + "a";
        public const string DefaultTranslation = "leetspeak";
        public const string DefaultTextToTranslate = "I shall not test new line I wonder if it works\ttesting tab for a good measure :D - smile >:( - angry :( - sad";
        public const string DefaultTranslated = "I shall not test new line I wonder if it works\ttesting tab for a good measure :D - smile >:( - 4ngRy :( - 5Ad";
        public static string DefaultResponse = @"{
            ""success"": {
                ""total"": 1
            },
            ""contents"": {
                ""translated"": ""I shall not test new line I wonder if it works\ttesting tab for a good measure :D - smile >:( - 4ngRy :( - 5Ad"",
                ""text"": ""I shall not test new line I wonder if it works\ttesting tab for a good measure :D - smile >:( - angry :( - sad"",
                ""translation"": ""leetspeak""
            }
        }";
        public static Func<string> DefaultResponseFunc = () => DefaultResponse;
        public static FunTranslationsResponse DefaultDeserializedResponse = new FunTranslationsResponse()
        {
            Success = new Success()
            {
                Total = 1
            },
            Contents = new Contents()
            {
                Translated = DefaultTranslated,
                Text = DefaultTextToTranslate,
                Translation = DefaultTranslation
            }
        };
        public static HttpResponseMessage DefaultSuccessResponseMessage = new HttpResponseMessage()
        {
            StatusCode = System.Net.HttpStatusCode.OK,
            Content = new StringContent(DefaultResponse, System.Text.Encoding.UTF8, "application/json")
        };
        public static Func<HttpResponseMessage> DefaultSuccessResponseMessageFunc = () => DefaultSuccessResponseMessage;
        public static string DefaultNoTranslationResponse = @"{
            ""error"": {
                ""code"": 404,
                ""message"": ""Not Found""
            }
        }";
        public static Func<string> DefaultNoTranslationResponseFunc = () => DefaultNoTranslationResponse;
        public static HttpResponseMessage DefaultNoTranslationResponseMessage = new HttpResponseMessage()
        {
            StatusCode = System.Net.HttpStatusCode.NotFound,
            Content = new StringContent(DefaultNoTranslationResponse, System.Text.Encoding.UTF8, "application/json")
        };
        public static Func<HttpResponseMessage> DefaultNoTranslationResponseMessageFunc = () => DefaultNoTranslationResponseMessage;

        public static string DefaultTooManyRequestsResponse = @"{
            ""error"": {
                ""code"": 429,
                ""message"": ""Too Many Requests: Rate limit of 10 requests per hour exceeded. Please wait for 54 minutes and 17 seconds.""
            }
        }";
        public static Func<string> DefaultTooManyRequestsResponseFunc = () => DefaultTooManyRequestsResponse;
        public static HttpResponseMessage DefaultTooManyRequestsResponseMessage = new HttpResponseMessage()
        {
            StatusCode = System.Net.HttpStatusCode.TooManyRequests,
            Content = new StringContent(DefaultTooManyRequestsResponse, System.Text.Encoding.UTF8, "application/json")
        };
        public static Func<HttpResponseMessage> DefaultTooManyRequestsResponseMessageFunc = () => DefaultTooManyRequestsResponseMessage;
    }
}
