using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using DatingApp.API.Helpers;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public UserRepository(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<MemberDto> GetMemberDtoAsync(int id)
        {
            var member = await _context.Users
                .Where(x => x.Id == id)
                .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();

            return member;
        }

        public async Task<MemberDto> GetMemberDtoAsync(string username)
        {
            var member = await _context.Users
                .Where(x => x.UserName == username)
                .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();

            return member;
        }

        public async Task<PagedList<MemberDto>> GetMemberDtosAsync(UserParams userParams)
        {
            var members = _context.Users.AsQueryable();

            members = members.Where(u => u.UserName != userParams.CurrentUsername && u.Gender == userParams.Gender);

            var minDod = DateTime.Today.AddYears(-userParams.MaxAge - 1);

            var maxDod = DateTime.Today.AddYears(-userParams.MinAge);

            members = members.Where(u => u.DateOfBirth >= minDod && u.DateOfBirth <= maxDod);

            members = userParams.OrderBy switch
            {
                "created" => members.OrderByDescending(u => u.Created),
                _ => members.OrderByDescending(u => u.LastActive)
            };

            return await PagedList<MemberDto>.CreateAsync(
                members.ProjectTo<MemberDto>(_mapper.ConfigurationProvider).AsTracking(), 
                userParams.PageNumber, userParams.PageSize);
        }

        public async Task<AppUser> GetUserByUsernameAsync(string username)
        {
            var user = await _context.Users
                .Include(p => p.Photos)
                .SingleOrDefaultAsync(x => x.UserName == username);
            
            return user;
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Update(AppUser user)
        {
            _context.Entry(user).State = EntityState.Modified;
        }
    }
}