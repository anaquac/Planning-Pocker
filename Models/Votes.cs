using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Planning_Poker.Models
{
    public class Votes : BaseEntity
    {
        
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        public User User { get; set; }

        [ForeignKey(nameof(Letters))]
        public Guid LettersId { get; set; }
        public Letters Letters { get; set; }

        [ForeignKey(nameof(UserStory))]
        public Guid UseStoryId { get; set; }
        public UserStory UserStory { get; set; }
    }
}
