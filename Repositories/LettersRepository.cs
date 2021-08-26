using Planning_Poker.Data;
using Planning_Poker.Models;

namespace Planning_Poker.Repositories
{
    public class LettersRepository : GenericRepository<Letters>, ILettersRepository
    {
        public LettersRepository(DataContext context) : base(context)
        {
        }
    }
}
