using MediatrExample.Core.Enums;

namespace MediatrExample.Core.Services
{
    public interface IAuthService
    {
        public string GetUserId();
        public List<UserRole> GetUserRoles();
    }
}
