using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CommentDtos;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.WebUI.Controllers
{
    public class ProductListController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public ProductListController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult Index(string id)
        {
            ViewBag.i = id;
            return View();
        }

        public IActionResult ProductDetail(string id)
        {
            //öne çıkan ürünlerin seçilen kartlarından alınan product id değeri diğer view sayfalarına gönderilir.
            ViewBag.x = id;
            return View();
        }
        [HttpGet]
        public PartialViewResult AddComment()
        {
            return PartialView();
        }
        [HttpPost]
        public async Task<IActionResult> AddComment(CreateCommentDto createCommentDto) //biri partial view ise ve diğer method normal Action view ise her ikiside aynı sayfaya gider.
        {
            createCommentDto.ImageUrl = "test";
            createCommentDto.Raiting = 1;
            createCommentDto.CreatedDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            createCommentDto.Status = false;
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createCommentDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7066/api/Comments", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Default");
            }
            return View("Index","Default");
        }
    }
}
