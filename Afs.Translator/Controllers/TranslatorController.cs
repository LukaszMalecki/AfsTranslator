using Afs.Translator.DTOs;
using Afs.Translator.FunTranslations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Afs.Translator.Controllers
{
    public class TranslatorController : Controller
    {
        private ITranslationClient _client;
        public TranslatorController(ITranslationClient client) 
        {
            _client = client;
        }

        public async Task<TranslationResponseBriefDto> GetTranslatedAsync(string textToTranslate, string translation)
        {
            var result = await _client.TranslateAsync(textToTranslate, translation);
            return new TranslationResponseBriefDto() { TranslatedText = result.Contents.Translated };
        }

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
