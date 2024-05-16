namespace Afs.Translator.FunTranslations
{
    public class HttpClientStub : IHttpClientWrapper
    {
        private readonly Func<HttpResponseMessage> _funcMessage;
        public HttpClientStub(Func<HttpResponseMessage> funcMessage) 
        {
            _funcMessage = funcMessage;
        }

        public async Task<HttpResponseMessage> GetAsync(string url)
        {
            var value = _funcMessage();
            return value;
        }
    }
}
