using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Planning_Poker.Dto;
using Planning_Poker.Models;
using Planning_Poker.Repositories;

namespace Planning_Poker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserStoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UserStoryController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        /// <summary>
        /// Get all User Story
        /// </summary>
        /// <returns>IEnumerable<UserStory></returns>
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<UserStory>))]
        public async Task<ActionResult<IEnumerable<UserStory>>> GetUserStory()
        {
            IEnumerable<UserStory> userStories = _unitOfWork.UserStory.GetAll();
            return Ok(userStories);
        }

        /// <summary>
        /// Get UserStory by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>UserStory</returns>
        [HttpGet("{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserStory))]
        public async Task<ActionResult<UserStory>> GetUserStory(Guid id)
        {
            UserStory userStory = _unitOfWork.UserStory.GetById(id);
            //we can used validator for more complex examples
            if (userStory == null)
            {
                return NotFound("User not found");
            }

            return Ok(userStory);
        }

        /// <summary>
        /// Update UserStory
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userStoryDto"></param>
        /// <returns></returns>

        [HttpPut("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UpdateUserStory(Guid id, UserStoryDto userStoryDto)
        {
            if (_unitOfWork.UserStory.GetById(id) == null)
                return NotFound("User not found");
            else
            {
                UserStory userStory = new UserStory()
                {
                    Id = id,
                    Description = userStoryDto.Description
                };
                _unitOfWork.UserStory.Update(userStory);
                return Ok();
            }
        }

        /// <summary>
        /// Add UserStory
        /// </summary>
        /// <param name="userStoryDto"></param>
        /// <returns></returns>
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserStoryDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<ActionResult> AddUsers([FromQuery] UserStoryDto userStoryDto)
        {

            _unitOfWork.UserStory.Add(new UserStory
            {
                Description = userStoryDto.Description
            });

            _unitOfWork.Complete();
            return Ok();
        }

        /// <summary>
        /// Delete UserStory
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteUserStory(Guid id)
        {
            UserStory userStory = _unitOfWork.UserStory.GetById(id);
            if (userStory == null)
            {
                return NotFound("User Not found");
            }

            _unitOfWork.UserStory.Remove(userStory);
            return Ok();
        }
    }
}
