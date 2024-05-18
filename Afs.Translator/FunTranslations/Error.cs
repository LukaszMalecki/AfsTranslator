using Newtonsoft.Json;
using System.Text;

namespace Afs.Translator.FunTranslations
{
    public record Error
    {
        [JsonProperty("code")]
        public int Code { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(nameof(Error));
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
            stringBuilder.Append($"{nameof(Code)} = ");
            stringBuilder.Append(Code.ToString());
            stringBuilder.Append($", {nameof(Message)} = ");
            stringBuilder.Append(Message.ToString());
            return true;
        }
    }
}
