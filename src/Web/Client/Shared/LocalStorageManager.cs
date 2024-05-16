using Microsoft.AspNetCore.Components;
using Blazored.LocalStorage;
using System.Threading.Tasks;

namespace Web.Client.Shared
{
    public class LocalStorageManager
    {
        private readonly ILocalStorageService _localStorage;
      
        public LocalStorageManager(ILocalStorageService localStorage)
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

        public async Task<bool> GetIsAdmin()
        {
            string flag = await _localStorage.GetItemAsStringAsync("isAdmin");
            if (bool.TryParse(flag, out bool isAdmin))
            {
                return isAdmin;
            }
            return false; 
        }

        public async Task SetUserId(string userId)
        {
            await _localStorage.SetItemAsync("userId", userId);         
        }

        public async Task SetIsAdmin(bool isAdmin)
        {
            await _localStorage.SetItemAsync("isAdmin", isAdmin);
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

        public async Task ClearAll()
        {
            await _localStorage.RemoveItemAsync("userId");
            await _localStorage.RemoveItemAsync("authToken");
            await _localStorage.RemoveItemAsync("isAdmin");
        }
    }
}
