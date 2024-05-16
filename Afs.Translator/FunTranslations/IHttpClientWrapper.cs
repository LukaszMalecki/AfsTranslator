namespace Afs.Translator.FunTranslations
{
    public interface IHttpClientWrapper
    {
        public Task<HttpResponseMessage> GetAsync(string url);
    }
}
