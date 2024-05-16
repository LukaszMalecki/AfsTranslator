namespace Afs.Translator.FunTranslations
{
    public class HttpClientStub : IHttpClientWrapper
    {
        private readonly Func<string> _funcString;
        public HttpClientStub(Func<string> func) 
        {
            _funcString = func;
        }
        public Task<string> GetStringAsync(string url)
        {
            return new Task<string>(_funcString);
        }
    }
}
