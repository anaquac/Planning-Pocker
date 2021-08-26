using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Planning_Poker.Data;
using Planning_Poker.Models;

namespace Planning_Poker.Repositories
{
    public class VotesRepositories: GenericRepository<Votes>, IVotesRepository
    {
        public VotesRepositories(DataContext context) : base(context)
        {
        }

        public IEnumerable<Votes> GetAllVotesRegistered()
        {
            return _context.Votes.OrderByDescending(x => x.Letters.Value).ToList();
        }
    }
}
