using System.Threading.Tasks;
using FreeCourse.Web.Models.ViewModels;

namespace FreeCourse.Web.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserViewModel> GetUser();
    }
}