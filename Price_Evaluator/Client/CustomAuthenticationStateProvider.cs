using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Price_Evaluator.Shared;
using System.Net.Http.Json;
using System.Security.Claims;

namespace Price_Evaluator.Client
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorage;
        private readonly HttpClient _http;

        public CustomAuthenticationStateProvider(ILocalStorageService localStorage, HttpClient http)
        {
            _localStorage = localStorage;
            _http = http;
        }


        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {

            var username = await _localStorage.GetItemAsStringAsync("email");

            var state = new AuthenticationState(new ClaimsPrincipal());
            if (!string.IsNullOrEmpty(username))
            {
                username = username.Trim('"');
                var user = await _http.GetFromJsonAsync<UserModel>($"api/Account/email/{username}");

                if (user != null)
                {

                    var identity = new ClaimsIdentity(new[]
                    {
                        new Claim(ClaimTypes.Email, username),
                        new Claim(ClaimTypes.Role, user.Role!)

                 }, "test authentication type");

                    state = new AuthenticationState(new ClaimsPrincipal(identity));
                    
                }
               

                NotifyAuthenticationStateChanged(Task.FromResult(state));
            }

            return state;
        }

       
    }
}
