using Afs.Translator.FunTranslations;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using System.Web;
using Newtonsoft.Json;
namespace Afs.Translator
{
    public class TranslationClient:ITranslationClient
    {
        private readonly IHttpClientWrapper _client;
        public TranslationClient(IHttpClientWrapper httpClient)
        {
            _client = httpClient;
        }

        public async Task<FunTranslationsResponse> TranslateAsync(string textToTranslate, string translation)
        {
            Validation(ref textToTranslate, ref translation);
            UriBuilder uriBuilder = new UriBuilder($"{Constants.BaseUrl}/{translation}{Constants.TargetFileFormat}");
            NameValueCollection nameValueCollection = HttpUtility.ParseQueryString("");
            nameValueCollection[Constants.QueryTextName] = textToTranslate;
            uriBuilder.Query = nameValueCollection.ToString();

            var response = await _client.GetAsync(uriBuilder.Uri.AbsoluteUri);
            if (response == null)
            {
                throw new InvalidOperationException("Null response from the service");
            }
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                if (String.IsNullOrEmpty(responseContent))
                {
                    throw new InvalidOperationException("Null response from the service");
                }
                return JsonConvert.DeserializeObject<FunTranslationsResponse>(responseContent)!;
            }
            switch ((int)response.StatusCode)
            {
                case Constants.IncorrectTranslationStatusCode:
                    throw new ArgumentOutOfRangeException(nameof(translation), "Translation service of such name doesn't exist");
                case Constants.TooManyRequestsStatusCode:
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var errorContent = JsonConvert.DeserializeObject<ErrorContent>(responseContent)!;
                    throw new InvalidOperationException(errorContent.Error.Message);
                default:
                    responseContent = await response.Content.ReadAsStringAsync();
                    errorContent = JsonConvert.DeserializeObject<ErrorContent>(responseContent)!;
                    throw new InvalidOperationException($"Unknown exception, code: {errorContent.Error.Code}, message: {errorContent.Error.Message}");
            }
        }
        private void Validation(ref string textToTranslate, ref string translation)
        {
            if (textToTranslate == null)
            {
                throw new ArgumentNullException(nameof(textToTranslate), "Null is not permitted");
            }
            textToTranslate = textToTranslate.Trim();
            if (textToTranslate.Length is < Constants.MinLenTextToTranslate or > Constants.MaxLenTextToTranslate)
            {
                throw new ArgumentOutOfRangeException(nameof(textToTranslate),
                    $"Length should be between {Constants.MinLenTextToTranslate} and {Constants.MaxLenTextToTranslate} inclusive");
            }
            if (Constants.AnyNewlineRegexFormat.Match(textToTranslate).Success)
            {
                throw new ArgumentException("Newline characters are forbidden", nameof(textToTranslate));
            }

            if (translation == null)
            {
                throw new ArgumentNullException(nameof(translation), "Null is not permitted");
            }
            translation = translation.Trim();
            if (translation.Length is < Constants.MinLenTranslation or > Constants.MaxLenTranslation)
            {
                throw new ArgumentOutOfRangeException(nameof(translation),
                    $"Length should be between {Constants.MinLenTranslation} and {Constants.MaxLenTranslation} inclusive");
            }
            if (Constants.AnyNewlineRegexFormat.Match(translation).Success)
            {
                throw new ArgumentException("Newline characters are forbidden", nameof(translation));
            }
        }

    }
}