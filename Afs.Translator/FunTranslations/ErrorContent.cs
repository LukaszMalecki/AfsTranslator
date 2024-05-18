using Newtonsoft.Json;
using System.Text;

namespace Afs.Translator.FunTranslations
{
    public record ErrorContent
    {
        [JsonProperty("error")]
        public Error Error { get; set; }
        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(nameof(ErrorContent));
            stringBuilder.Append(" { ");
            if (PrintMembers(stringBuilder))
            {
                stringBuilder.Append(' ');
            }
            stringBuilder.Append('}');
            return stringBuilder.ToString();
        }
        protected virtual bool PrintMembers(StringBuilder stringBuilder)
        {
            stringBuilder.Append($"{nameof(Error)} = ");
            stringBuilder.Append(Error.ToString());
            return true;
        }
    }
}