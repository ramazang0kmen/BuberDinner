using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuberDinner.Domain.Common.Models
{
    public abstract class AgregateRoot<TId> : Entity<TId>
        where TId : notnull
    {
        protected AgregateRoot(TId id) : base(id)
        {
        }
    }
}
