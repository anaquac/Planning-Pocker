using Planning_Poker.Data;

namespace Planning_Poker.Repositories
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly DataContext _context;

        public UnitOfWork(DataContext context)
        {
            _context = context;
            Votes = new VotesRepositories(_context);
            Users = new UserRepository(_context);
            Letters = new LettersRepository(_context);
            UserStory = new UserStoryRepository(_context);
        }
        public IVotesRepository Votes { get; private set; }
        public IUserRespository Users { get; private set; }
        public ILettersRepository Letters { get; private set; }
        public IUserStoryRepository UserStory { get; private set; }
        public int Complete() => _context.SaveChanges();

        public void Dispose() => _context.Dispose();
    }
}
