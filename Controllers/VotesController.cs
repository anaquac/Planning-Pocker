using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Planning_Poker.Dto;
using Planning_Poker.Hubs;
using Planning_Poker.Models;
using Planning_Poker.RabbitMQ;
using Planning_Poker.Repositories;
using RabbitMQ.Client;

namespace Planning_Poker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VotesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHubContext<NotifyUsers> _hubContext;
        private readonly IMapper _mapper;

        public VotesController(IUnitOfWork unitOfWork, IHubContext<NotifyUsers> hubContext, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _hubContext = hubContext;
            _mapper = mapper;
        }

        /// <summary>
        /// Add Vote
        /// </summary>
        /// <param name=""></param>
        /// <returns>VotesDto</returns>
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(VotesDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<ActionResult> AddVotes([FromQuery] VotesDto votesDto)
        {
            var letter = _unitOfWork.Letters.GetById(votesDto.letter.Id);
            var user = _unitOfWork.Users.GetById(votesDto.user.Id);
            var userStory = _unitOfWork.UserStory.GetById(votesDto.storyDto.Id);
            if (letter == null || user == null || userStory == null)
                return BadRequest();
            _unitOfWork.Votes.Add(new Votes
            {
                LettersId = letter.Id,
                UseStoryId = userStory.Id,
                UserId = user.Id
            });

            _unitOfWork.Complete();

            var factory = new ConnectionFactory
            {
                Uri = new Uri("amqp://guest:guest@localhost:5672")
            };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            QueueProducer.Publish(channel,votesDto);

            return Ok();
        }


        /// <summary>
        /// Get all Votes
        /// </summary>
        /// <returns>IEnumerable<VotesDtoHubs></returns>
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<VotesDtoHubs>))]
        public async Task<ActionResult<IEnumerable<VotesDtoHubs>>> GetAllVotes()
        {
            IEnumerable<Votes> votes = _unitOfWork.Votes.GetAll();
            var votesDtoHub = new List<VotesDtoHubs>();
            foreach (var vote in votes)
            {
                var aux = new VotesDtoHubs
                {
                    userStory = _unitOfWork.UserStory.GetById(vote.UseStoryId).Description,
                    letter = _unitOfWork.Letters.GetById(vote.LettersId).Value,
                    user = _unitOfWork.Users.GetById(vote.UserId).Name
                };
                votesDtoHub.Add(aux);
            }
            return Ok(votesDtoHub);
        }

        /// <summary>
        /// Get Votes by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Votes</returns>
        [HttpGet("{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Votes))]
        public async Task<ActionResult<Votes>> GetVotesId(Guid id)
        {
            Votes votes = _unitOfWork.Votes.GetById(id);
            //we can used validator for more complex examples
            if (votes == null)
            {
                return NotFound("User not found");
            }

            return Ok(votes);
        }

    }
}
