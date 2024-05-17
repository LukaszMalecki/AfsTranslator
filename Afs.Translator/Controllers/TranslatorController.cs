using Afs.Translator.DTOs;
using Afs.Translator.FunTranslations;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Afs.Translator.Models;

namespace Afs.Translator.Controllers
{
    public class TranslatorController : Controller
    {
        private ITranslationClient _client;
        public TranslatorController(ITranslationClient client) 
        {
            _client = client;
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
                return BadRequest(ModelState.ToDictionary(x => x.Key, x => x.Value.Errors));
            }
            if(!VerifyTranslation(translationRequest, out string verifyResult))
            {
                return BadRequest(verifyResult);
            }
            try
            {
                var result = await GetTranslatedAsync(translationRequest.TextToTranslate, verifyResult);
                return Ok(result.TranslatedText);
            }
            catch (Exception ex) 
            {
                return BadRequest($"{ex.GetType().Name}: {ex.Message}");
            }
        }
        protected bool VerifyTranslation(TranslationRequestCreateDto translationRequest, out string translation)
        {
            bool isValid = VerifyTranslation(translationRequest.TranslationName, translationRequest.TranslationId, out translation);
            return isValid;
        }
        protected bool VerifyTranslation(string? translationName ,int? translationId, out string translation)
        {
            //In the future, validation using DbContext
            if(translationName != null)
            {
                translation = translationName;
                return true;
            }
            if(translationId != null)
            {
                //In the future, validation using DbContext
                switch (translationId)
                {
                    case ModelConstants.DefaultTranslationId:
                        translation = ModelConstants.DefaultTranslation;
                        return true;
                    default:
                        translation = "No available translation with such Id";
                        return false;
                }
            }
            translation = ModelConstants.DefaultTranslation;
            return true;
        }
        /*
        public IActionResult TranslateApi([FromQuery] TranslationRequestCreateDto translationRequest)
        {
            translationRequest.TextToTranslate = "a";
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return Ok(translationRequest);
            //return new JsonResult("aaa");
        }*/
        // GET: TranslatorController
        public ActionResult Index()
        {
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
