using System.Collections.Generic;
using System.Threading.Tasks;
using API.Controllers;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Interfaces;
using DatingApp.API.Extensions;
using DatingApp.API.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{
    [Authorize]
    public class LikesController : BaseApiController
    {
        private readonly IUserRepository _userRepository;
        private readonly ILikeRepository _likeRepository;
        public LikesController(IUserRepository userRepository, ILikeRepository likeRepository)
        {
            _likeRepository = likeRepository;
            _userRepository = userRepository;
        }

        [HttpPost("{username}")]
        public async Task<ActionResult> AddLike(string username)
        {
            var sourceUserId = User.GetUserId();

            var likedUser = await _userRepository.GetUserByUsernameAsync(username);

            var sourceUser = await _likeRepository.GetUserWithLikes(sourceUserId);

            if (likedUser == null) return NotFound();

            if (sourceUser.UserName == username) return BadRequest("You can not like yourself");

            var userLike = await _likeRepository.GetUserLike(sourceUserId, likedUser.Id);

            if (userLike != null) return BadRequest("You already like this user");

            userLike = new UserLike
            {
                SourceUserId = sourceUserId,
                LikeUserId = likedUser.Id
            };

            sourceUser.LikeUsers.Add(userLike);

            if (await _userRepository.SaveAllAsync()) return Ok();

            return BadRequest("Failed to like user");
        }

        [HttpGet]
        public async Task<ActionResult<PagedList<LikeDto>>> GetUserLists([FromQuery]LikesParams likesParams)
        {
            likesParams.UserId = User.GetUserId();

            var users = await _likeRepository.GetUserLikes(likesParams);

            Response.AddpaginationHeader(users.CurrentPage, users.PageSize, users.TotalCount, users.TotalPages);
            
            return Ok(users);
        }
    }
}