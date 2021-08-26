using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Planning_Poker.Models;

namespace Planning_Poker.Repositories
{
    public interface IVotesRepository: IGenericRepository<Votes>
    {
        IEnumerable<Votes> GetAllVotesRegistered();
    }
}
