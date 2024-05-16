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

            if(translation == null)
            {
                throw new ArgumentNullException(nameof(translation), "Null is not permitted");
            }
            translation = translation.Trim();
            if (translation.Length is < Constants.MinLenTranslation or > Constants.MaxLenTranslation)
            {
                throw new ArgumentOutOfRangeException(nameof(translation),
                    $"Length should be between {Constants.MinLenTranslation} and {Constants.MaxLenTranslation} inclusive");
            }
            if (Constants.AnyNewlineRegexFormat.Match(translation).Success)
            {
                throw new ArgumentException("Newline characters are forbidden", nameof(translation));
            }
            return null;
        }
    }
}