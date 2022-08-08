using System.Threading.Tasks;
using FreeCourse.Shared.Dtos;
using FreeCourse.Web.Models.Inputs;
using IdentityModel.Client;

namespace FreeCourse.Web.Services.Interfaces
{
    public interface IIdentityService
    {
        Task<Response<bool>> SignIn(SignInInput signInInput);
        Task<TokenResponse> GetAccessTokenByRefreshToken();
        Task RevokeRefreshToken();
    }
}