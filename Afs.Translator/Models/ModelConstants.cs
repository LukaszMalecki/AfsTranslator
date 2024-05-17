namespace Afs.Translator.Models
{
    public static class ModelConstants
    {
        //DTOs.TranslationRequestCreateDto
        public const int InputTextLenMin = 1;
        public const int InputTextLenMax = 100;
        public const int DefaultTranslationId = 1;
        public const string DefaultTranslation = "leetspeak";

        //Models.TranslationRequest
        public const int ModelTranslationRequestTextLenMin = 1;
        public const int ModelTranslationRequestTextLenMax = 100;
        //Models.TranslationResponse
        public const int ModelTranslationResponseTextLenMin = 1;
        public const int ModelTranslationResponseTextLenMax = 200;
        //Models.Translation
        public const int ModelTranslationNameLenMin = 1;
        public const int ModelTranslationNameLenMax = 50;
    }
}
