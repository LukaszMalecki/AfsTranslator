namespace Afs.Translator.FunTranslations
{
    public class HttpClientStub : IHttpClientWrapper
    {
        private readonly Func<string> _funcString;
        public HttpClientStub(Func<string> funcJsonString) 
        {
            _funcString = funcJsonString;
        }
        public async Task<string> GetStringAsync(string url)
        {
            var value = _funcString();
            return value;
        }
    }
}
