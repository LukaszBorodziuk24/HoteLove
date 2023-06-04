using HoteLove.Models;

namespace HoteLove.Services.Interfaces
{
    public interface IUserContext
    {
        public ApplicationUser GetCurrentUser();
        public string GetUserId();
    }
}
