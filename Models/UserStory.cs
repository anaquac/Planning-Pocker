using System.ComponentModel.DataAnnotations;

namespace Planning_Poker.Models
{
    public class UserStory : BaseEntity
    {

        [Required (ErrorMessage = "Description is required")]
        public string Description { get; set; }
    }
}
