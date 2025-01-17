using HPSpells.DomainLayer.Entities.Base;
using HPSpells.DomainLayer.Services.DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPSpells.DataAccessLayer.EF.Base
{
    public class EFRepository<T>(DbContext db) : IRepository<T>
        where T : EntityBase, new()
    {

        protected readonly DbContext _dbContext = db;

        protected virtual void Set(T model, EntityState state)
        {
            switch (state)
            {
                case EntityState.Added:
                    _dbContext.Set<T>().Add(model);
                    break;
                case EntityState.Modified:
                    _dbContext.Set<T>().Update(model).Property(_ => _.Id).IsModified = false;
                    break;
                case EntityState.Deleted:
                    _dbContext.Set<T>().Remove(model);
                    break;
            }
        }

        protected virtual void SingleOperation(T model, EntityState state, bool autoDetectChangesEnabled = false)
        {
            if (model == null)
                return;

            _dbContext.ChangeTracker.AutoDetectChangesEnabled = autoDetectChangesEnabled;

            Set(model, state);

            _dbContext.SaveChanges();
        }

        protected virtual async Task SingleOperationAsync(T model, EntityState state, bool autoDetectChangesEnabled = false)
        {
            if (model == null)
                return;

            _dbContext.ChangeTracker.AutoDetectChangesEnabled = autoDetectChangesEnabled;

            Set(model, state);

            await _dbContext.SaveChangesAsync();
        }

        public async virtual Task<T> FindByIdAsync<TKeyProp>(TKeyProp key)
        {
            return await _dbContext.Set<T>().FindAsync(key);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _dbContext.Set<T>();
        }

        public virtual Task DeleteAsync(T model) => SingleOperationAsync(model, EntityState.Deleted);

        public virtual Task InsertAsync(T model, bool autoDetectChangesEnabled = false) => SingleOperationAsync(model, EntityState.Added, autoDetectChangesEnabled);

        public virtual Task UpdateAsync(T model, bool autoDetectChangesEnabled = false) => SingleOperationAsync(model, EntityState.Modified, autoDetectChangesEnabled);

        public virtual async Task<bool> ExistsAsync<TKeyProp>(TKeyProp key)
        {
            return await _dbContext.Set<T>().AnyAsync(e => e.Id.Equals(key));
        }

        public virtual async Task UpsertAsync(T model, bool autoDetectChangesEnabled = false)
        {
            if (model == null)
                return;

            bool exists = await ExistsAsync(model.Id);
            if (exists)
            {
                await UpdateAsync(model, autoDetectChangesEnabled);
            }
            else
            {
                await InsertAsync(model, autoDetectChangesEnabled);
            }
        }
    }
}
