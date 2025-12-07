using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.CategoryDtos;
using Newtonsoft.Json;
using System.Text;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AllowAnonymous]
    [Route("Admin/Category")]
    public class CategoryController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory; // Backend API istekleri için HttpClientFactory kullanımı ve tanımlanması

        public CategoryController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            ViewBag.v1 = "Ana sayfa";
            ViewBag.v2 = "Kategoriler";
            ViewBag.v3 = "kategori listesi";
            ViewBag.v0 = "Kategori işlemleri";

            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7070/api/Categories");  // Yapacağım isteğin türünü belirleyecek ve adresini
            if (responseMessage.IsSuccessStatusCode)
            {
                var jasonData = await responseMessage.Content.ReadAsStringAsync(); // gelen veriyi string formatında okuyacak
                var values = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(jasonData); // gelen string veriyi de deserialize edip CategoryDto tipine dönüştürecek
                return View(values);
            }

            return View();
        }
        [HttpGet]
        [Route("CreateCategory")]
        public async Task<IActionResult> CreateCategory()
        {
            ViewBag.v1 = "Ana sayfa";
            ViewBag.v2 = "Kategoriler";
            ViewBag.v3 = "Yeni kategori girişi";
            ViewBag.v0 = "Kategori işlemleri";
            return View();
        }
        [HttpPost]
        [Route("CreateCategory")]
        public async Task<IActionResult> CreateCategory(CreateCategoryDto createCategoryDto)
        {
            var client = _httpClientFactory.CreateClient();
            var JasonData = JsonConvert.SerializeObject(createCategoryDto);
            StringContent stringContent = new StringContent(JasonData, Encoding.UTF8, "application/json");  // gönderilecek veriyi belirleyecek
            var responseMassage = await client.PostAsync("https://localhost:7070/api/Categories", stringContent);
            if (responseMassage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index","Category",new {area="Admin"});
            }
            return View();
        }
        [Route("DeleteCategory/{id}")]
        public async Task<IActionResult> DeleteCategory(string id) 
        {
            var client = _httpClientFactory.CreateClient();
            var responseMassage = await client.DeleteAsync("https://localhost:7070/api/Categories?id="+id);
            if (responseMassage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Category", new { area = "Admin" });
            }
            return View();
        }
        [Route("UpdateCategory/{id}")]
        [HttpGet]
        public async Task<IActionResult> UpdateCategory(string id)
        {
            ViewBag.v1 = "Ana sayfa";
            ViewBag.v2 = "Kategoriler";
            ViewBag.v3 = "kategori güncellem sayfası";
            ViewBag.v0 = "Kategori işlemleri";

            var client = _httpClientFactory.CreateClient();
            var responseMassage = await client.GetAsync("https://localhost:7070/api/Categories/" + id);
            if (responseMassage.IsSuccessStatusCode)
            {
                var jsonData = await responseMassage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<UpdateCategoryDto>(jsonData);
                return View(values);  
            }
            return View();
        }
        [Route("UpdateCategory/{id}")]
        [HttpPost]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryDto updateCategoryDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateCategoryDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMassage = await client.PutAsync("https://localhost:7070/api/Categories",stringContent);
            if (responseMassage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Category", new { area = "Admin" });
            }
            return View();
        }

    }
}
// kategori ile ilgili tüm işlemler bu controller da yer almaktatır. Api ile iletişim kurup veriler asenkron 
//olarak crud işlemlerine tabi tutulmaktadır.
//backend de api ile iletişim httpclientfactory ile sağlanmaktadır bunu öğrenmiş oldum.
//Route ile alan ve controller bazında yönlendirme yapmayı öğrendim.
//Deserialize ve serialize arasındaki farkı ve kullanım alanlarını öğrendim 
//{Deserialize: Json formatındaki veriyi C# nesnesine dönüştürme ve genellikle listeleme ve id'ye göre getrirme işlemlerinde kullanılır.
//{ Serialize: C# nesnesini Json formatına dönüştürme ve genellikle veri ekleme ve güncelleme işlemlerinde kullanılır.
//allow anonymous ile yetkilendirme işlemlerini kapatmayı öğrendim.