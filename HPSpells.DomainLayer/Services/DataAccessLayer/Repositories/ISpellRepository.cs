using HPSpells.DomainLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPSpells.DomainLayer.Services.DataAccessLayer.Repositories
{
    public interface ISpellRepository : IRepository<Spell>
    {
        Task<bool> ExistsSpellAsync(string spellName);
    }
}
