namespace Afs.Translator.FunTranslations
{
    public class HttpClientStub : IHttpClientWrapper
    {
        private readonly Func<string> _funcString;
        private readonly Func<HttpResponseMessage> _funcMessage;
        public HttpClientStub(Func<string> funcJsonString, Func<HttpResponseMessage> funcMessage) 
        {
            _funcString = funcJsonString;
            _funcMessage = funcMessage;
        }

        public async Task<HttpResponseMessage> GetAsync(string url)
        {
            var value = _funcMessage();
            return value;
        }

        public async Task<string> GetStringAsync(string url)
        {
            var value = _funcString();
            return value;
        }
    }
}
