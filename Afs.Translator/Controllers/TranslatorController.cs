using Afs.Translator.DTOs;
using Afs.Translator.FunTranslations;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Afs.Translator.Models;
using Afs.Translator.Data;
using Afs.Translator.Wrappers;
using Microsoft.EntityFrameworkCore;
using Afs.Translator.ViewModels;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Diagnostics;

namespace Afs.Translator.Controllers
{
    public class TranslatorController : Controller
    {
        private ITranslationClient _client;
        private readonly TranslatorDbContext _context;
        private readonly INowWrapper _nowWrapper;
        public TranslatorController(ITranslationClient client, TranslatorDbContext context, INowWrapper nowWrapper) 
        {
            _client = client;
            _context = context;
            _nowWrapper = nowWrapper;
        }
        [NonAction]
        public async Task<TranslationResponseBriefDto> GetTranslatedAsync(string textToTranslate, string translation)
        {
            var result = await _client.TranslateAsync(textToTranslate, translation);
            return new TranslationResponseBriefDto() { TranslatedText = result.Contents.Translated };
        }
        [HttpGet("[controller]/Translate")]
        public async Task<IActionResult> TranslateApiAsync([FromQuery] TranslationRequestCreateDto translationRequest)
        {
            //var x = ModelState;
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState.ToDictionary(x => x.Key, x => x.Value!.Errors));
            }
            Tuple<bool, string, int> VerificationResult = await VerifyTranslationAsync(translationRequest);
            if (!VerificationResult.Item1)
            {
                return BadRequest(VerificationResult.Item2);
            }
            translationRequest.TranslationName = VerificationResult.Item2;
            translationRequest.TranslationId = VerificationResult.Item3;

            translationRequest.RequestDate = AdjustedDateTime(translationRequest.RequestDate);
            var request = await AddTranslationRequest(translationRequest);
            try
            {
                var result = await GetTranslatedAsync(translationRequest.TextToTranslate, translationRequest.TranslationName);
                var response = await AddTranslationResponse(result.TranslatedText, request.Id);
                return Ok(response.TranslatedText);
            }
            catch (Exception ex) 
            {
                return BadRequest($"{ex.GetType().Name}: {ex.Message}");
            }
        }
        protected async Task<Tuple<bool, string, int>> VerifyTranslationAsync(TranslationRequestCreateDto translationRequest)
        {
            return await VerifyTranslationAsync(translationRequest.TranslationName, translationRequest.TranslationId);
        }
        protected async Task<Tuple<bool, string, int>> VerifyTranslationAsync(string? translationName ,int? translationId)
        {
            if(translationName != null)
            {
                translationName = translationName.Trim();
                var translation = await _context.Translations.Where(x => x.TranslationName.Equals(translationName)).FirstOrDefaultAsync();
                if (translation != null)
                {
                    return new Tuple<bool, string, int>(true, translationName, translation.Id);
                }
                return new Tuple<bool, string, int>(false, "No available translation with such name", -1);
            }
            if(translationId != null)
            {
                var translation = await _context.Translations.FindAsync(translationId);
                if (translation != null)
                {
                    return new Tuple<bool, string, int>(true, translation.TranslationName, translation.Id);
                }
                return new Tuple<bool, string, int>(false, "No available translation with such id", -1);
            }
            //Both nulls return default translation without need to call the database
            return new Tuple<bool, string, int>(true, ModelConstants.DefaultTranslation, ModelConstants.DefaultTranslationId);
        }
        //Makes sure that given time isn't too different from server time
        protected DateTime AdjustedDateTime(DateTime? dateTime)
        {
            if(dateTime == null)
            {
                return _nowWrapper.Now;
            }
            var differenceTolerance = ControllerConstants.DateMaxDifferenceHours;
            if (dateTime >= _nowWrapper.Now.AddHours(differenceTolerance) || dateTime <= _nowWrapper.Now.AddHours(differenceTolerance))
            {
                return _nowWrapper.Now;
            }
            return dateTime.Value;
        }
        protected async Task<TranslationRequest> AddTranslationRequest(TranslationRequestCreateDto dto)
        {
            return await AddTranslationRequest(dto.TextToTranslate, dto.RequestDate.Value, dto.TranslationId.Value, dto.TranslationName);
        }
        protected async Task<TranslationRequest> AddTranslationRequest(string textToTranslate, DateTime requestDate, int translationId, string translationName)
        {
            var requestItem = new TranslationRequest()
            {
                TextToTranslate = textToTranslate,
                RequestDate = requestDate,
                TranslationId = translationId
            };
            _context.TranslationRequests.Add(requestItem);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch(Exception e)
            {
                throw new Exception($"Internal database problem, {e.Message}");
            }
            return requestItem;
        }
        protected async Task<TranslationResponse> AddTranslationResponse(string translated, int translationRequestId)
        {
            translated = TrimmedToLimit(translated, ModelConstants.ModelTranslationResponseTextLenMax);
            var responseItem = new TranslationResponse()
            {
                TranslatedText = translated,
                TranslationRequestId = translationRequestId
            };

            _context.TranslationResponses.Add(responseItem);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception($"Internal database problem, {e.Message}");
            }
            return responseItem;
        }
        protected string TrimmedToLimit(string text, int maxLen)
        {
            if(text.Length <= maxLen)
                return text;
            return text.Substring(0, maxLen);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] TranslationUserViewModel viewModel)
        {
            if(!ModelState.IsValid)
            {
                SetErrorMessage("Unknown error");
                return RedirectToAction(nameof(Index));
            }
            var dto = ViewModelMapper.TranslationUserViewModelToDto(viewModel);
            var response = await TranslateApiAsync(dto);
            if(response is BadRequestObjectResult)
            {
                var badReq = response as BadRequestObjectResult;
                SetErrorMessage(badReq.Value.ToString());
                return RedirectToAction(nameof(Translator), viewModel);
            }
            var okRes = response as OkObjectResult;
            var responseString = okRes!.Value as string;
            viewModel.TranslatedText = responseString;
            SetErrorMessage(isError: false);
            return RedirectToAction(nameof(Translator), viewModel);
        }
        [NonAction]
        protected void SetErrorMessage(string message = "", bool isError=true)
        {
            if(!isError)
            {
                ViewBag.ErrorMessage = "";
                TempData["error"] = "";
                return;
            }
            ViewBag.ErrorMessage = $"Error: {message}";
            TempData["error"] = $"Error: {message}";
        }
        // GET: TranslatorController
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var viewModel = PrepareTranslator(null);
            return View(viewModel);
        }
        [HttpGet]
        public async Task<ActionResult> Translator(TranslationUserViewModel viewModel)
        {
            viewModel = PrepareTranslator(viewModel);
            return View(viewModel);
        }
        [NonAction]
        protected TranslationUserViewModel PrepareTranslator(TranslationUserViewModel viewModel)
        {
            if (viewModel == null)
            {
                viewModel = new TranslationUserViewModel()
                {
                    TextToTranslate = "",
                    TranslationId = ModelConstants.DefaultTranslationId,
                    TranslatedText = null
                };
            }
            ViewBag.Title = "Leetspeak Translator";
            ViewBag.ErrorMessage = TempData["error"];
            return viewModel;
        }

        // GET: TranslatorController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TranslatorController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TranslatorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TranslatorController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TranslatorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TranslatorController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TranslatorController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
