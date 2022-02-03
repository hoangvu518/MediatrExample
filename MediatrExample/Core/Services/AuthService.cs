using MediatrExample.Core.Enums;

namespace MediatrExample.Core.Services
{
    public class AuthService : IAuthService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AuthService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public string GetUserId()
        {
            return "1234";
        }

        public List<UserRole> GetUserRoles()
        {
            return new List<UserRole>() { UserRole.Admin, UserRole.SuperUser };
        }
    }
}
