using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using QuizApp.Helpers;
using QuizApp.Models.Database;
using QuizApp.Models.Login;
using QuizApp.Services;
using QuizApp.Models.Inputs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;

namespace QuizApp.Controllers
{
    public class LoginController : Controller
    {
        private OfferService _offerService;

        // Constructor metodu, bağımlılığı enjekte eder.
        public LoginController(OfferService offerService)
        {
            _offerService = offerService;
        }

        // Index action metodu, giriş sayfasını görüntüler.
        public IActionResult Index()
        {
            return View();
        }

        // CheckLogin action metodu, kullanıcının giriş bilgilerini kontrol eder.
        [AllowAnonymous]
        public async Task<IActionResult> CheckLogin([FromForm] LoginPage key)
        {
            // Login servis metodu ile kullanıcının giriş bilgileri kontrol edilir.
            var temp = _offerService.Login(new Models.Database.Users { Email = key.username, Password = key.password });
            bool alreadysession = false, login = false;

            if (temp != null)
            {
                login = true;

                // Kullanıcı giriş yaparsa Login metodu çağrılır ve oturum açılır.
                Login(temp);
            }

            // Sonucu JSON formatında döndürür.
            return Ok(new { Login = login, SessionAlready = alreadysession });
        }

        // Login metodu, kullanıcıyı oturum açar.
        async void Login(dboKullanicilar temp)
        {
            if (temp != null)
            {
                string token = "";

                // Kullanıcı için gerekli olan temel bilgilerle birlikte bir ClaimsIdentity oluşturulur.
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, temp.Ad + " " + temp.Soyad),
                    new Claim(ClaimTypes.NameIdentifier, temp.Email),
                    new Claim(ClaimTypes.Sid, temp.Id.ToString()),
                };

                var useridentity = new ClaimsIdentity(claims, "Login");
                ClaimsPrincipal principal = new ClaimsPrincipal(useridentity);

                // HttpContext aracılığıyla kullanıcıyı oturum açar.
                await HttpContext.SignInAsync(principal);
            }
        }

        // Logout action metodu, kullanıcıyı oturumdan çıkarır.
        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var tvmkodu = Convert.ToInt32(identity.FindFirst(ClaimTypes.Sid).Value);

            // HttpContext aracılığıyla kullanıcıyı oturumdan çıkarır.
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // Sonucu JSON formatında döndürür.
            return Ok(true);
        }

        // Error action metodu, oturum hatası durumunda yönlendirme yapar.
        [AllowAnonymous]
        public async Task<IActionResult> Error()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var tvmkodu = Convert.ToInt32(identity.FindFirst(ClaimTypes.Sid).Value);

            // HttpContext aracılığıyla kullanıcıyı oturumdan çıkarır ve Index action'ına yönlendirir.
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Login");
        }

        // SignUp action metodu, kullanıcının kaydını yapar.
        [HttpPost]
        public async Task<IActionResult> SignUp([FromForm] SignUp key)
        {
            // SignUp servis metodu ile kullanıcının kaydı yapılır.
            var insertedUser = await _offerService.SignUp(key);

            bool alreadysession = false, login = insertedUser;

            // Sonucu JSON formatında döndürür.
            return Ok(new { Login = login, SessionAlready = alreadysession });
        }
    }
}
