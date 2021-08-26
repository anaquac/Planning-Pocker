using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Planning_Poker.Dto;
using Planning_Poker.Models;
using Planning_Poker.Repositories;

namespace Planning_Poker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LettersController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public LettersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Get all letters
        /// </summary>
        /// <returns>IEnumerable<User></returns>
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Letters>))]
        public async Task<ActionResult<IEnumerable<User>>> GetLetters()
        {
            IEnumerable<Letters> letters = _unitOfWork.Letters.GetAll();
            return Ok(letters);
        }

        /// <summary>
        /// Get letters by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>User</returns>
        [HttpGet("{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Letters))]
        public async Task<ActionResult<Letters>> Letter(Guid id)
        {
            Letters letter = _unitOfWork.Letters.GetById(id);
            //we can used validator for more complex examples
            if (letter == null)
            {
                return NotFound("Letter not found");
            }

            return Ok(letter);
        }

        /// <summary>
        /// Update letter
        /// </summary>
        /// <param name="id"></param>
        /// <param name="LetterDto"></param>
        /// <returns></returns>

        [HttpPut("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UpdateLetter(Guid id, LetterDto letterDto)
        {
            if (_unitOfWork.Letters.GetById(id) == null)
                return NotFound("User not found");
            else
            {
                Letters letters = new Letters
                {
                    Id = id,
                    Value = letterDto.value
                };
                _unitOfWork.Letters.Update(letters);
                return Ok();
            }
        }

        /// <summary>
        /// Add Letter
        /// </summary>
        /// <param name="letterDto"></param>
        /// <returns></returns>
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LetterDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<ActionResult> AddLetter([FromQuery] LetterDto letterDto)
        {

            _unitOfWork.Letters.Add(new Letters
            {
                Value = letterDto.value
            });

            _unitOfWork.Complete();
            return Ok();
        }

        /// <summary>
        /// Delete Letter
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteLetter(Guid id)
        {
            Letters letters= _unitOfWork.Letters.GetById(id);
            if (letters == null)
            {
                return NotFound("User Not found");
            }

            _unitOfWork.Letters.Remove(letters);
            return Ok();
        }

    }
}
