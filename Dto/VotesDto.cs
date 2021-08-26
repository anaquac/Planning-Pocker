using System;

namespace Planning_Poker.Dto
{
    public class VotesDto
    {
        public Guid Id { get; set; }
        public UserDto user { get; set; }
        public LetterDto letter { get; set; }
        public UserStoryDto storyDto { get; set; }
    }
}
