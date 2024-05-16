using Afs.Translator.FunTranslations;
using System.Text.RegularExpressions;

namespace Afs.Translator
{
    public class TranslationClient:ITranslationClient
    {
        public TranslationClient()
        {
        }

        public async Task<FunTranslationsResponse> TranslateAsync(string textToTranslate, string translation)
        {
            if (textToTranslate == null) 
            {
                throw new ArgumentNullException(nameof(textToTranslate), "Null is not permitted");
            }
            textToTranslate = textToTranslate.Trim();
            if (textToTranslate.Length is < Constants.MinLenTextToTranslate or > Constants.MaxLenTextToTranslate)
            {
                throw new ArgumentOutOfRangeException(nameof(textToTranslate), 
                    $"Length should be between {Constants.MinLenTextToTranslate} and {Constants.MaxLenTextToTranslate} inclusive");
            }
            if(Constants.AnyNewlineRegexFormat.Match(textToTranslate).Success)
            {
                throw new ArgumentException("Newline characters are forbidden", nameof(textToTranslate));
            }
            return null;
        }
    }
}