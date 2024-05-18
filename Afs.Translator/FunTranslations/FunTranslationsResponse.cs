using Newtonsoft.Json;
using System.Text;

namespace Afs.Translator.FunTranslations
{
    public record FunTranslationsResponse
    {
        [JsonProperty("success")]
        public Success Success { get; set; }
        [JsonProperty("contents")]
        public Contents Contents { get; set; }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(nameof(FunTranslationsResponse));
            stringBuilder.Append(" { ");
            if( PrintMembers(stringBuilder) ) 
            {
                stringBuilder.Append(' ');
            }
            stringBuilder.Append('}');
            return stringBuilder.ToString();
        }
        protected virtual bool PrintMembers(StringBuilder stringBuilder) 
        {
            stringBuilder.Append($"{nameof(Success)} = ");
            stringBuilder.Append(Success.ToString());
            stringBuilder.Append($", {nameof(Contents)} = ");
            stringBuilder.Append(Contents.ToString());
            return true;
        }
    }
}
