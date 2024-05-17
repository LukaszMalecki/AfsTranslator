using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Afs.Translator.Tests.Unit
{
    public static class ModelValidationTestsConstants
    {
        public const string text0char = "";
        public const string text1char = "a";
        public const string text100char = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
            "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
        public const string text101char = text100char + "a";
    }
}
