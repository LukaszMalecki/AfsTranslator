namespace Afs.Translator.FunTranslations
{
    public interface IHttpClientWrapper
    {
        public Task<string> GetStringAsync(string url);
    }
}
