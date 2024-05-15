using Afs.Translator.FunTranslations;

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
                throw new ArgumentNullException(nameof(textToTranslate), "Null is not permited");
            }
            if (textToTranslate.Equals("")) 
            {
                throw new ArgumentOutOfRangeException(nameof(textToTranslate), "Empty value is not permited");
            }
            if(textToTranslate.Length is < Constants.MinLenTextToTranslate or > Constants.MaxLenTextToTranslate)
            {
                throw new ArgumentOutOfRangeException(nameof(textToTranslate), 
                    $"Length should be between {Constants.MinLenTextToTranslate} and {Constants.MaxLenTextToTranslate} inclusive");
            }
            return null;
        }
    }
}