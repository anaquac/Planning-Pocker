using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Planning_Poker.Models
{
    public interface IEntity
    {
        public Guid Id { get; set; }

        public DateTime CreateAt { get; set; }
    }
}
