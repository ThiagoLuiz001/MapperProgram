using Mapper.Communication.Exceptions;
using Mapper.Domain.Entities.Machine;
using Mapper.Domain.Interfaces;
using Mapper.Exception;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;


namespace Mapper.Infrastructure.DAO.Repository
{
    public class RepositoryMemory : IRepository<RAM, int>
    {

        private readonly MapperContext _context;

        public RepositoryMemory(MapperContext context)
        {
            _context = context;
        }
        public async Task<bool> ContainsByEntityAsync(RAM entity, CancellationToken ct = default)
        {
            return await _context.Memorys.ContainsAsync(entity, ct);
        }

        public async Task<int> CountByConditionAsync(Expression<Func<RAM, bool>> predicate, CancellationToken ct = default)
        {
            return await _context.Memorys.CountAsync(predicate, ct);
        }

        public async Task CreateAsync(RAM entity, CancellationToken ct = default)
        {
            _context.Memorys.Add(entity);
            await _context.SaveChangesAsync(ct);
        }

        public async Task CreateListAsync(List<RAM> entities, CancellationToken ct = default)
        {
            _context.Memorys.AddRange(entities);
            await _context.SaveChangesAsync(ct);
        }

        public async Task DeleteAsyncById(int id, CancellationToken ct = default)
        {
            var obj = await GetByIdAsync(id, ct) ??
                throw new NotFoundException(String.Format(ResExceptions.ID_NAO_ENCONTRADO, id, nameof(_context.Memorys)));
            try
            {
                _context.Memorys.Remove(obj);
                await _context.SaveChangesAsync(ct);
            }
            catch (DbUpdateException ex)
            {
                throw new DbConCurrencyException(String.Format(
                    ResExceptions.ERRO_DE_CONCORRENCIA,
                    ResExceptions.DELETE,
                    id,
                    nameof(_context.Memorys),
                    ex.Message));
            }
        }

        public Task DeleteByCondition(Expression<Func<RAM, bool>> predicate, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ExistByConditionAsync(Expression<Func<RAM, bool>> predicate, CancellationToken ct = default)
        {
            return await _context.Memorys.AnyAsync(predicate, ct);
        }

        public async Task<List<RAM>?> GetAllAsync(CancellationToken ct = default)
        {
            return await _context.Memorys.ToListAsync(ct);
        }

        public async Task<RAM?> GetByConditionAsync(Expression<Func<RAM, bool>> predicate, CancellationToken ct = default)
        {
            return await _context.Memorys.FirstOrDefaultAsync(predicate, ct);
        }

        public async Task<RAM?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            return await _context.Memorys.FirstOrDefaultAsync(m => m.Id == id, ct);
        }

        public async Task<List<RAM>?> GetListByConditionAsync(Expression<Func<RAM, bool>> predicate, CancellationToken ct = default)
        {
            return await _context.Memorys.Where(predicate).ToListAsync(ct);
        }

        public async Task UpdateAsync(RAM entity, CancellationToken ct = default)
        {
            if(!await ContainsByEntityAsync(entity, ct))
                throw new NotFoundException(String.Format(ResExceptions.ID_NAO_ENCONTRADO, entity.Id, nameof(_context.Memorys)));
            try
            {
                _context.Memorys.Update(entity);
                await _context.SaveChangesAsync(ct);
            }
            catch(DbUpdateConcurrencyException ex)
            {
                throw new DbConCurrencyException(String.Format(ResExceptions.ERRO_DE_CONCORRENCIA, ResExceptions.UPDATE, entity.Id, nameof(_context.Memorys), ex.Message));
            }
        }

        public async Task UpdateListAsync(List<RAM> entities, CancellationToken ct = default)
        {
            var list = await GetListByConditionAsync(x => entities.Select(e => e.Id).Contains(x.Id), ct);
            if(list != null || list.Count != entities.Count)
                throw new NotFoundException(String.Format(ResExceptions.ID_NAO_ENCONTRADO, "-", nameof(_context.Storages)));
            try
            {
                _context.UpdateRange(entities);
                await _context.SaveChangesAsync(ct);
            }
            catch(DbUpdateConcurrencyException ex)
            {
                throw new DbConCurrencyException(String.Format(ResExceptions.ERRO_DE_CONCORRENCIA, ResExceptions.UPDATE, '-', nameof(_context.Memorys), ex.Message));
            }
        }
    }
}
