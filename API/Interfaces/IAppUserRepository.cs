using API.Entities;

namespace API.Interfaces
{
    public interface IAppUserRepository
    {
        void Update(AppUser user);
        Task<bool> SaveAllAsync();
        Task<IReadOnlyList<AppUser>> GetAppUsersAsync();
        Task<AppUser> GetAppUserByIdAsync(string id);
    }
}