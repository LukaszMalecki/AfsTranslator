using Afs.Translator.DTOs;
using Afs.Translator.FunTranslations;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Afs.Translator.Models;
using Afs.Translator.Data;
using Microsoft.EntityFrameworkCore;

namespace Afs.Translator.Controllers
{
    public class TranslatorController : Controller
    {
        private ITranslationClient _client;
        private readonly TranslatorDbContext _context;
        public TranslatorController(ITranslationClient client, TranslatorDbContext context) 
        {
            _client = client;
            _context = context;
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
            var x = ModelState;
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState.ToDictionary(x => x.Key, x => x.Value!.Errors));
            }
            Tuple<bool, string> VerificationResult = await VerifyTranslationAsync(translationRequest);
            if (!VerificationResult.Item1)
            {
                return BadRequest(VerificationResult.Item2);
            }
            try
            {
                var result = await GetTranslatedAsync(translationRequest.TextToTranslate, VerificationResult.Item2);
                return Ok(result.TranslatedText);
            }
            catch (Exception ex) 
            {
                return BadRequest($"{ex.GetType().Name}: {ex.Message}");
            }
        }
        protected async Task<Tuple<bool, string>> VerifyTranslationAsync(TranslationRequestCreateDto translationRequest)
        {
            return await VerifyTranslationAsync(translationRequest.TranslationName, translationRequest.TranslationId);
        }
        protected async Task<Tuple<bool, string>> VerifyTranslationAsync(string? translationName ,int? translationId)
        {
            if(translationName != null)
            {
                translationName = translationName.Trim();
                if(await _context.Translations.Where(x => x.TranslationName.Equals(translationName)).FirstOrDefaultAsync() != null)
                {
                    return new Tuple<bool, string>(true, translationName);
                }
                return new Tuple<bool, string>(false, "No available translation with such name");
            }
            if(translationId != null)
            {
                var translation = await _context.Translations.FindAsync(translationId);
                if (translation != null)
                {
                    return new Tuple<bool, string>(true, translation.TranslationName);
                }
                return new Tuple<bool, string>(false, "No available translation with such id");
            }
            //Both nulls return default translation without need to call the database
            return new Tuple<bool, string>(true, ModelConstants.DefaultTranslation);
        }

        // GET: TranslatorController
        public async Task<ActionResult> Index()
        {
            var translation = await _context.Translations.FindAsync(1);
            ViewBag.Test = translation.TranslationName;
            return View();
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
    }
}
