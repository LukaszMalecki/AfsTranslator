﻿namespace Afs.Translator.FunTranslations
{
    public class HttpClientWrapper : IHttpClientWrapper
    {
        private readonly HttpClient _httpClient;
        public HttpClientWrapper() 
        {
            _httpClient = new HttpClient();
        }

        public Task<HttpResponseMessage> GetAsync(string url)
        {
            return _httpClient.GetAsync(url);
        }
    }
}
