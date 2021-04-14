using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface IUserRepository
    {
        void Update(AppUser user);
        Task<bool> SaveAllAsync();
        Task<IEnumerable<MemberDto>> GetMemberDtosAsync();
        Task<MemberDto> GetMemberDtoAsync(int id);
        Task<MemberDto> GetMemberDtoAsync(string username);
        Task<AppUser> GetUserByUsernameAsync(string username);
    }
}