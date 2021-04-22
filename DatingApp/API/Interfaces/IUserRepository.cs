using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using DatingApp.API.Helpers;

namespace API.Interfaces
{
    public interface IUserRepository
    {
        void Update(AppUser user);
        Task<bool> SaveAllAsync();
        Task<PagedList<MemberDto>> GetMemberDtosAsync(UserParams userParams);
        Task<MemberDto> GetMemberDtoAsync(int id);
        Task<MemberDto> GetMemberDtoAsync(string username);
        Task<AppUser> GetUserByIdAsync(int id);
        Task<AppUser> GetUserByUsernameAsync(string username);
    }
}