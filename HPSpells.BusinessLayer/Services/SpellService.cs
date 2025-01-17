using AutoMapper;
using HPSpells.BusinessLayer.Helpers;
using HPSpells.DomainLayer.DTOs;
using HPSpells.DomainLayer.Entities;
using HPSpells.DomainLayer.Services.BusinessLayer;
using HPSpells.DomainLayer.Services.DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HPSpells.BusinessLayer.Services
{
    public class SpellService : ISpellService
    {

        private readonly ISpellRepository _spellRepository;
        private readonly IMapper _mapper;
        private readonly ICacheHelper _cache;

        public SpellService(ISpellRepository spellRepository, IMapper mapper, ICacheHelper cache)
        {
            _spellRepository = spellRepository;
            _mapper = mapper;
            _cache = cache;
        }

        public IEnumerable<SpellResponse> GetAllSpells()
        {
            return _cache.GetOrCreate("GetAllSpells", item =>
            {
                return _spellRepository.GetAll().ToList()
                        .Select(spell => _mapper.Map<SpellResponse>(spell));
            }, new CacheOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(60)
            });
            
        }

        public async Task UpsertAsync(SpellRequest request)
        {
            Spell requestSpell = _mapper.Map<Spell>(request);

            Spell? spell = await _spellRepository.GetSpellByNameAsync(requestSpell.Name);
            if (spell is null)
            {
                await _spellRepository.InsertAsync(requestSpell);
            }
            else
            {
                spell.Name = requestSpell.Name;
                spell.Description = requestSpell.Description;
                await _spellRepository.UpdateAsync(spell);
            }

            _cache.Remove("GetAllSpells");
        }
    }
}
