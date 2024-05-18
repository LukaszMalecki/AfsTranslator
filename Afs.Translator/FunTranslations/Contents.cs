using Afs.Translator.FunTranslations;
using Newtonsoft.Json;
using System.Text;

namespace Afs.Translator
{
    public record Contents
    {
        [JsonProperty("translated")]
        public string Translated { get; set; }
        [JsonProperty("text")]
        public string Text { get; set; }
        [JsonProperty("translation")]
        public string Translation { get; set; }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(nameof(Contents));
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
            stringBuilder.Append($"{nameof(Translated)} = ");
            stringBuilder.Append(Translated.ToString());
            stringBuilder.Append($", {nameof(Text)} = ");
            stringBuilder.Append(Text.ToString());
            stringBuilder.Append($", {nameof(Translation)} = ");
            stringBuilder.Append(Translation.ToString());
            return true;
        }
    }
}