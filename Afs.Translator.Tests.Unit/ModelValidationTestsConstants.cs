using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Afs.Translator.Tests.Unit
{
    public static class ModelValidationTestsConstants
    {
        public const string DefaultTranslation = "leetspeak";
        public const string text0char = "";
        public const string text1char = "a";
        public const string text50char = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
        public const string text51char = text50char + "a";
        public const string text100char = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
            "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
        public const string text101char = text100char + "a";
        public const string text200char = text100char + text100char;
        public const string text201char = text200char + "a";
    }
}
