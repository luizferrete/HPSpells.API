using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPSpells.DomainLayer.DTOs
{
    public record SpellRequest(string Spell, string Use);
}
