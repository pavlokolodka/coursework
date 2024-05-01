using Microsoft.AspNetCore.Components;
using Blazored.LocalStorage;
using System.Threading.Tasks;

namespace Web.Client.Shared
{
    public class TokenManager
    {
        private readonly ILocalStorageService _localStorage;
      
        public TokenManager(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        public async Task<string>? GetAuthTokenAsync()
        {
            string token = await _localStorage.GetItemAsStringAsync("authToken");
            if (string.IsNullOrEmpty(token))
            {
                return null; 
            }
            return token;
        }

        public async Task<string>? GetUserIdAsync()
        {
            string id = await _localStorage.GetItemAsStringAsync("userId");
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }
            return id;
        }

        public async Task SetUserId(string userId)
        {
            await _localStorage.SetItemAsync("userId", userId);         
        }

        public async Task SetToken(string token)
        {
            await _localStorage.SetItemAsync("authToken", token);
        }

        public async Task RemoveToken()
        {
            await _localStorage.RemoveItemAsync("authToken");
        }

        public async Task RemoveUserId()
        {
            await _localStorage.RemoveItemAsync("userId");
        }
    }
}
