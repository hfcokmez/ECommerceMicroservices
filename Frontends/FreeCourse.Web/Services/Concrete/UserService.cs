using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FreeCourse.Web.Models.ViewModels.Users;
using FreeCourse.Web.Services.Interfaces;

namespace FreeCourse.Web.Services.Concrete
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;
        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<UserViewModel> GetUser()
        {
            return await _httpClient.GetFromJsonAsync<UserViewModel>("/api/user/getuser");
        }
    }
}