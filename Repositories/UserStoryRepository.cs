using Planning_Poker.Data;
using Planning_Poker.Models;

namespace Planning_Poker.Repositories
{
    public class UserStoryRepository : GenericRepository<UserStory>, IUserStoryRepository
    {
        public UserStoryRepository(DataContext context) : base(context)
        {
        }
    }
}
