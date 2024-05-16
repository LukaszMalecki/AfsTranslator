namespace Afs.Translator.FunTranslations
{
    public class HttpClientWrapper : IHttpClientWrapper
    {
        private readonly HttpClient _httpClient;
        public HttpClientWrapper() 
        {
            _httpClient = new HttpClient();
        }

        public Task<string> GetStringAsync(string url)
        {
            return _httpClient.GetStringAsync(url);
        }
    }
}
