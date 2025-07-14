using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class MembersController(IAppUserRepository userRepository) : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<AppUser>>> GetMembers()
        {
            return Ok(await userRepository.GetAppUsersAsync());
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<AppUser?>> GetMember(string id)
        {
            var member = await userRepository.GetAppUserByIdAsync(id);

            if (member == null)
                return NotFound();

            return member;
        }
    }
}
