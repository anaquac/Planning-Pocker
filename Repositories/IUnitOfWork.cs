using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Planning_Poker.Repositories
{
    public interface IUnitOfWork
    {
        IVotesRepository Votes { get; }
        IUserRespository Users { get; }
        ILettersRepository Letters { get; }
        IUserStoryRepository UserStory { get; }
        int Complete();
    }
}
