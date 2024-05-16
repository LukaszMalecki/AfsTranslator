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
        public static Func<string> DefaultResponse = () => @"{
            ""success"": {
                ""total"": 1
            },
            ""contents"": {
                ""translated"": ""I shall not test new line I wonder if it works\ttesting tab for a good measure :D - smile >:( - 4ngRy :( - 5Ad"",
                ""text"": ""I shall not test new line I wonder if it works\ttesting tab for a good measure :D - smile >:( - angry :( - sad"",
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
                Text = DefaultTextToTranslate,
                Translation = DefaultTranslation
            }
        };
    }
}
