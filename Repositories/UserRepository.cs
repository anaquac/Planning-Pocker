using Planning_Poker.Data;
using Planning_Poker.Models;

namespace Planning_Poker.Repositories
{
    public class UserRepository: GenericRepository<User>, IUserRespository
    {
        public UserRepository(DataContext context) : base(context)
        {
        }
    }
}
