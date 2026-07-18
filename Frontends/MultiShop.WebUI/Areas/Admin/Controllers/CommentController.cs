using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CommentDtos;
using Newtonsoft.Json;
using System.Text;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AllowAnonymous]
    [Route("Admin/Comment")]
    public class CommentController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory; 

        public CommentController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            ViewBag.v1 = "Ana sayfa";
            ViewBag.v2 = "Yorumlar";
            ViewBag.v3 = "Yorum listesi";
            ViewBag.v0 = "Yorum işlemleri";

            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7066/api/Comments");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jasonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultCommentDto>>(jasonData);
                return View(values);
            }

            return View();
        }

        [Route("DeleteComment/{id}")]
        public async Task<IActionResult> DeleteComment(string id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMassage = await client.DeleteAsync("https://localhost:7066/api/Categories?id=" + id);
            if (responseMassage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Comment", new { area = "Admin" });
            }
            return View();
        }
        [Route("UpdateComment/{id}")]
        [HttpGet]
        public async Task<IActionResult> UpdateComment(string id)
        {
            ViewBag.v1 = "Ana sayfa";
            ViewBag.v2 = "Yorumlar";
            ViewBag.v3 = "Yorum listesi";
            ViewBag.v0 = "Yorum işlemleri";

            var client = _httpClientFactory.CreateClient();
            var responseMassage = await client.GetAsync("https://localhost:7066/api/Comments/" + id);
            if (responseMassage.IsSuccessStatusCode)
            {
                var jsonData = await responseMassage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<UpdateCommentDto>(jsonData);
                return View(values);
            }
            return View();
        }
        [Route("UpdateComment/{id}")]
        [HttpPost]
        public async Task<IActionResult> UpdateComment(UpdateCommentDto updateCommentDto)
        {
            updateCommentDto.Status = true;
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateCommentDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMassage = await client.PutAsync("https://localhost:7066/api/Comments", stringContent);
            if (responseMassage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Comment", new { area = "Admin" });
            }
            return View();
        }
    }
}