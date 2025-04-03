using AppointmentMvc.Data;
using AppointmentMvc.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Text;
using Microsoft.AspNetCore.Authentication.BearerToken;

namespace AppointmentMvc.Controllers
{
    public class AuthController : Controller
    {
        private readonly HttpClient _httpClient;
        public AuthController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var jsonContent = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("https://localhost:7126/api/auth/register", jsonContent);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Login");

            ModelState.AddModelError(" ", "Kayıt başarısız");
            return View(model);
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var jsonContent = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("https://localhost:7126/api/auth/login", jsonContent);
            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(" ", "Geçersiz giriş bilgileri");
                return View(model);
            }
            var responseData = await response.Content.ReadAsStringAsync();
            var token = JsonSerializer.Deserialize<TokenResponse>(responseData);
            var claims = new List<Claim>
            {
            new Claim(ClaimTypes.Name,model.Email),
            new Claim("AccessToken",token.AccessToken)
            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
        public class TokenResponse
        {
            public string? AccessToken { get; set; }
        }
    }
}
