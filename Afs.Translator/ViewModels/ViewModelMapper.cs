using Afs.Translator.DTOs;

namespace Afs.Translator.ViewModels
{
    public static class ViewModelMapper
    {
        public static TranslationRequestCreateDto TranslationUserViewModelToDto(TranslationUserViewModel viewModel)
        {
            var dto = new TranslationRequestCreateDto()
            {
                TranslationId = viewModel.TranslationId,
                TextToTranslate = viewModel.TextToTranslate,
                RequestDate = viewModel.RequestDate,
                Id = viewModel.Id,
                TranslationName = null
            };
            return dto;
        }
    }
}
