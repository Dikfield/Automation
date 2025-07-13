using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class AppUserRepository(AppDbContext context) : IAppUserRepository
    {
        public async Task<AppUser?> GetAppUserByIdAsync(string id)
        {
            return await context.Users.FindAsync(id);
        }

        public async Task<IReadOnlyList<AppUser>> GetAppUsersAsync()
        {
            return await context.Users.ToListAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await context.SaveChangesAsync() > 0;
        }

        public void Update(AppUser user)
        {
            context.Entry(user).State = EntityState.Modified;
        }
    }
}
