using HPSpells.DomainLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPSpells.DomainLayer.Services.BusinessLayer
{
    public interface ISpellService
    {
        Task UpsertAsync(SpellRequest spellDTO);
        IEnumerable<SpellResponse> GetAllSpells();
    }
}
