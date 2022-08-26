using System.Threading.Tasks;
using FreeCourse.Web.Models.ViewModels;
using FreeCourse.Web.Services.Interfaces;

namespace FreeCourse.Web.Services.Concrete
{
    public class UserService : IUserService
    {
        public Task<UserViewModel> GetUser()
        {
            throw new System.NotImplementedException();
        }
    }
}