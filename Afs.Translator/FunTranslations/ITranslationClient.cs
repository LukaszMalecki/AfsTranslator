namespace Afs.Translator.FunTranslations
{
    public interface ITranslationClient
    {
        Task<FunTranslationsResponse> TranslateAsync(string textToTranslate, string translation);
    }
}
