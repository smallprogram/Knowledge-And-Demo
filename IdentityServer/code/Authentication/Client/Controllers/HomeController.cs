using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient _httpClient;

        public HomeController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }


        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Secret()
        {
            var token = await HttpContext.GetTokenAsync("access_token");

            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            var serverResponse = await _httpClient.GetAsync("https://localhost:6021/secret/index");
            var serverResponseContent = await serverResponse.Content.ReadAsStringAsync();

            var apiResponse = await _httpClient.GetAsync("https://localhost:6041/secret/index");
            var apiResponseContent = await apiResponse.Content.ReadAsStringAsync();


            return View(model:new string[] { serverResponseContent, apiResponseContent });
        }

        public IActionResult ResetCookie()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("Index");
        }
    }
}
