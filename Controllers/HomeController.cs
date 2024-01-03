using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Nancy.Json;
using Newtonsoft.Json;
using QuizApp.Helpers;
using QuizApp.Services;
using QuizApp.Models.Database;
using QuizApp.Models.Inputs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QuizApp.Controllers
{
    [Authorize] // Bu controller'a sadece yetkili (authorize) kullanıcılar erişebilir.
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppSettings _appSettings;
        private OfferService _offerService;

        // Constructor metodu, bağımlılıkları enjekte eder.
        public HomeController(ILogger<HomeController> logger, IOptions<AppSettings> appSettings, OfferService offerService)
        {
            _logger = logger;
            _appSettings = appSettings.Value;
            _offerService = offerService;
        }

        // Ana sayfa için action metodu
        public IActionResult Index()
        {
            // Kullanıcının kimliği üzerinden özel bir bilgi alınır.
            var identity = (ClaimsIdentity)User.Identity;
            var kid = Convert.ToInt32(identity.FindFirst(ClaimTypes.Sid).Value);

            // Index view'ini döndürür.
            return View();
        }

        // Quiz sayfası için action metodu
        public IActionResult Quiz()
        {
            // Kullanıcının kimliği üzerinden özel bir bilgi alınır.

            // Quiz view'ini döndürür.
            return View();
        }

        // Quiz seviye sayfası için action metodu
        public IActionResult QuizSeviye()
        {
            // Kullanıcının kimliği üzerinden özel bir bilgi alınır.

            // QuizSeviye view'ini döndürür.
            return View();
        }

        // SinavSkor action metodu, kullanıcının sınav skorunu alır.
        public async Task<IActionResult> SinavSkor([FromForm] SinavSkor key)
        {
            var identity = (ClaimsIdentity)User.Identity;
            var kid = Convert.ToInt32(identity.FindFirst(ClaimTypes.Sid).Value);
            key.UyeId = kid;

            // SinavSkor'u işleyip sonucu döndüren servis metodu çağrılır.
            var insertedUser = await _offerService.SinavSkor(key);
            bool alreadysession = false, login = insertedUser;

            // Sonucu JSON formatında döndürür.
            return Ok(new { Login = login, SessionAlready = alreadysession });
        }

        // CevapKontrol action metodu, kullanıcının cevaplarını kontrol eder.
        public async Task<IActionResult> CevapKontrol([FromForm] Models.Database.dboSorular key)
        {
            // CevapKontrol'u işleyip sonucu döndüren servis metodu çağrılır.
            var insertedUser = await _offerService.CevapKontrol(key);

            // Sonucu JSON formatında döndürür.
            return Ok(new { Result = insertedUser });
        }

        // Sorular action metodu, kullanıcının sorularını işler.
        public async Task<IActionResult> Sorular([FromForm] Models.Database.dboSorular key)
        {
            // Sorular'ı işleyip sonucu döndüren servis metodu çağrılır.
            var insertedUser = await _offerService.CevapKontrol(key);

            // Sonucu JSON formatında döndürür.
            return Ok(new { Result = insertedUser });
        }

        // VeriCekK action metodu, K kategorisindeki soruları çeker.
        public IActionResult VeriCekK()
        {
            var temp = _offerService.SorucekK(new Models.Inputs.Sorular { });

            // Sonucu JSON formatında döndürür.
            return Ok(new { Result = temp });
        }

        // VeriCekO action metodu, O kategorisindeki soruları çeker.
        public IActionResult VeriCekO()
        {
            var temp = _offerService.SorucekO(new Models.Inputs.Sorular { });

            // Sonucu JSON formatında döndürür.
            return Ok(new { Result = temp });
        }

        // VeriCekZ action metodu, Z kategorisindeki soruları çeker.
        public IActionResult VeriCekZ()
        {
            var temp = _offerService.SorucekZ(new Models.Inputs.Sorular { });

            // Sonucu JSON formatında döndürür.
            return Ok(new { Result = temp });
        }
    }
}
