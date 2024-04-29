using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Price_Evaluator.Server.Database;
using System.Net.Http;

namespace Price_Evaluator.Server.Services.ProductServices
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;
        private readonly DataContext _context;

        public List<Root> result { get; set; } = new List<Root>();
        public Root Singleresult { get; set; } = new Root();
        public string Search { get; set; }

        public ProductService(HttpClient httpClient, DataContext context)
        {
            _httpClient = httpClient;
            _context = context;
        }
        public async Task<List<Root>> GetProduct(string search)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://pricer.p.rapidapi.com/str?q={search}"),
                Headers =
                    {
                    { "X-RapidAPI-Key", "c157b460e4mshbb977f63d04f6d8p1408e1jsn61d35806f7a9" },
                         { "X-RapidAPI-Host", "pricer.p.rapidapi.com" },
                    },
            };
            using (var response = await _httpClient.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                var jsonResult = JsonConvert.DeserializeObject<List<Root>>(body);

                if (jsonResult != null)
                    result = jsonResult;
               
            }

            return result;


           
        }
        public async Task<Root> GetProductbyName(string name)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://pricer.p.rapidapi.com/str?q={name}"),
                Headers =
                    {
                       { "X-RapidAPI-Key", "c157b460e4mshbb977f63d04f6d8p1408e1jsn61d35806f7a9" },
                         { "X-RapidAPI-Host", "pricer.p.rapidapi.com" },
                    },
            };
            using (var response = await _httpClient.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                var jsonResult = JsonConvert.DeserializeObject<Root>(body);

                if (jsonResult != null)
                    Singleresult = jsonResult;

            }


            return Singleresult;



        }

        public async Task AddCart(Cart cart)
        {
           
            var result = await _context.Carts.AddAsync(cart);
            if(result != null)
            
            await _context.SaveChangesAsync();
            


        }

        public async Task<List<Cart>> GetCart()
        {
            var response = await _context.Carts.ToListAsync();
            

            return response!;
        }
    }
}
