using HPSpells.DataAccessLayer.EF;
using HPSpells.DataAccessLayer.EF.Base;
using HPSpells.DomainLayer.Entities;
using HPSpells.DomainLayer.Services.DataAccessLayer.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPSpells.DataAccessLayer.Repositories
{
    public class SpellRepository : EFRepository<Spell>, ISpellRepository
    {
        public SpellRepository(EntityContext context) : base(context)
        {
        }

        public async Task<bool> ExistsSpellAsync(string spellName)
        {
            return await _dbContext.Set<Spell>().AnyAsync(spell => spell.Name == spellName);
        }
    }
}
