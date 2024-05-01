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

        public async Task<string> GetAuthTokenAsync()
        {
            string token = await _localStorage.GetItemAsStringAsync("authToken");
            if (string.IsNullOrEmpty(token))
            {
                return null; 
            }
            return token;
        }
    }
}
