using Blazored.LocalStorage;
using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using Price_Evaluator.Shared;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Xml.Linq;
using static System.Net.WebRequestMethods;

namespace Price_Evaluator.Client.Services
{
    public class ProductClientServices : IProductClientServices
    {
        private readonly HttpClient _httpClient;
        private readonly NavigationManager _navigation;
        private readonly ILocalStorageService _localStorage;
        private readonly IToastService _toastService;

        public event Action ProductChanged;
        public List<Root> result { get; set; } = new List<Root>();
        public Root Singleresult { get; set; } = new Root();

        public string Search { get; set; } = string.Empty;
       

        public ProductClientServices(HttpClient httpClient, NavigationManager navigation, ILocalStorageService localStorage, IToastService toastService)
        {
            _toastService = toastService;
        

        _httpClient = httpClient;
            _navigation = navigation;
            _localStorage = localStorage;
        }
        

        public async Task<List<Root>> GetProduct()
        {
            
            var response = await _httpClient.GetFromJsonAsync<List<Root>>($"api/product/getproducts/{Search}");

            if (response is not null && response != null)

                result = response;
           
            ProductChanged.Invoke();

            
            return result;
           

        }

        //this is for cart
        
        public async Task<Root> GetProductbyName(string name)
        {
            var result = await _httpClient.GetFromJsonAsync<Root>($"api/product/getone/{name}");
            

            return result;
        }

        public async Task AddCart(Cart cart)
        {

            var Addcart = await _localStorage.GetItemAsync<List<Cart>>("cart");
            if (Addcart == null)
            {
                Addcart = new List<Cart>();
            }

                Addcart.Add(cart);
                _toastService.ShowSuccess($"{cart.Name} Added Successfully");
           
           
            await _localStorage.SetItemAsync<List<Cart>>("cart", Addcart);
            
          

            

        }


             
            
    }
}

