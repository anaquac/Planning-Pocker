using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Planning_Poker.Models;
using Planning_Poker.Repositories;

namespace Planning_Poker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UsersController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all UserDto
        /// </summary>
        /// <returns>IEnumerable<User></returns>
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<UserDto>))]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            
            IEnumerable<User> users =  _unitOfWork.Users.GetAll();

            return Ok(_mapper.Map<List<UserDto>>(users));
        }

        /// <summary>
        /// Get User by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>UserDto</returns>
        [HttpGet("{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDto))]
        public async Task<ActionResult<UserDto>> GetUser(Guid id)
        {
            User user = _unitOfWork.Users.GetById(id);
            //we can used validator for more complex examples
            if (user == null)
            {
                return NotFound("User not found");
            }

            return Ok(_mapper.Map<UserDto>(user));
        }

        /// <summary>
        /// Update User
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userDto"></param>
        /// <returns></returns>

        [HttpPut("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UpdateUser(Guid id, UserDto userDto)
        {
            if (_unitOfWork.Users.GetById(id) == null)
                return NotFound("User not found");
            else
            {
                User user = new User()
                {
                    Id = id,
                    Name = userDto.Name
                };
                _unitOfWork.Users.Update(user);
                return Ok();
            }
        }

        /// <summary>
        /// Add user
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<ActionResult> AddUsers([FromQuery] UserDto userDto)
        {
          
            _unitOfWork.Users.Add(new User
            {
                Name = userDto.Name
            });

            _unitOfWork.Complete();
            return Ok();
        }

        /// <summary>
        /// Delete User
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteUser(Guid id)
        {
            User user = _unitOfWork.Users.GetById(id);
            if (user == null)
            {
                return NotFound("User Not found");
            }

            _unitOfWork.Users.Remove(user);
            return Ok();
        }
    }
}
