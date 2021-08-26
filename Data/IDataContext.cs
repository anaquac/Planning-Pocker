using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Planning_Poker.Models;

namespace Planning_Poker.Data
{
    public interface IDataContext
    {
        DbSet<User> Users { get; set; }
        DbSet<Letters> Letters { get; set; }

        DbSet<UserStory> UserStories { get; set; }

        DbSet<Votes> Votes { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    }
}
