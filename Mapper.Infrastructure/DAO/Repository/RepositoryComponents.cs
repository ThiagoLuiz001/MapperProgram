using Mapper.Communication.Exceptions;
using Mapper.Domain.Entities.Machine.ComputerParts;
using Mapper.Domain.Interfaces;
using Mapper.Exception;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;


namespace Mapper.Infrastructure.DAO.Repository
{
    public class RepositoryComponents : IRepository<Components, int>
    {
        private readonly MapperContext _context;

        public RepositoryComponents(MapperContext context)
        {
            _context = context;
        }
        public async Task<bool> ContainsByEntityAsync(Components entity, CancellationToken ct = default)
        {
            return await _context.ComputerComponents.ContainsAsync(entity, ct);
        }

        public async Task<int> CountByConditionAsync(Expression<Func<Components, bool>> predicate, CancellationToken ct = default)
        {
            return await _context.ComputerComponents.CountAsync(predicate, ct);
        }

        public async Task CreateAsync(Components entity, CancellationToken ct = default)
        {
            _context.ComputerComponents.Add(entity);
            await _context.SaveChangesAsync(ct);
        }

        public async Task CreateListAsync(List<Components> entities, CancellationToken ct = default)
        {
            _context.ComputerComponents.AddRange(entities);
            await _context.SaveChangesAsync(ct);
        }

        public async Task DeleteAsyncById(int id, CancellationToken ct = default)
        {
            var obj = await GetByIdAsync(id, ct) ??
                throw new NotFoundException(String.Format(ResExceptions.ID_NAO_ENCONTRADO, id, nameof(_context.ComputerComponents)));
            try
            {
                _context.ComputerComponents.Remove(obj);
                await _context.SaveChangesAsync(ct);
            }
            catch (DbUpdateException ex)
            {
                throw new DbConCurrencyException(String.Format(
                    ResExceptions.ERRO_DE_CONCORRENCIA, 
                    ResExceptions.DELETE, 
                    id, 
                    nameof(_context.ComputerComponents),
                    ex.Message));
            }
        }

        public async Task DeleteByCondition(Expression<Func<Components, bool>> predicate, CancellationToken ct = default)
        {
            var list = await GetListByConditionAsync(predicate, ct);
            if(list is null || list.Count == 0)
                throw new NotFoundException(String.Format(ResExceptions.CONDICAO_NAO_ATENDIDA, ResExceptions.DELETE, nameof(_context.ComputerComponents)));
            try
            {
                _context.ComputerComponents.RemoveRange(list);
                await _context.SaveChangesAsync(ct);
            }
            catch (DbUpdateException ex)
            {
                throw new DbConCurrencyException(String.Format(
                    ResExceptions.ERRO_DE_CONCORRENCIA, 
                    ResExceptions.DELETE, 
                    nameof(_context.ComputerComponents),
                    ex.Message));
            }
        }

        public async Task<bool> ExistByConditionAsync(Expression<Func<Components, bool>> predicate, CancellationToken ct = default)
        {
            return await _context.ComputerComponents.AnyAsync(predicate, ct);
        }

        public async Task<List<Components>?> GetAllAsync(CancellationToken ct = default)
        {
            return await _context.ComputerComponents.ToListAsync(ct);
        }

        public Task<Components?> GetByConditionAsync(Expression<Func<Components, bool>> predicate, CancellationToken ct = default)
        {
            return _context.ComputerComponents.Include(c=>c.Computer).FirstOrDefaultAsync(predicate, ct);
        }

        public Task<Components?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            return _context.ComputerComponents.Include(c=>c.Computer).FirstOrDefaultAsync(x => x.Id == id, ct);
        }

        public async Task<List<Components>?> GetListByConditionAsync(Expression<Func<Components, bool>> predicate, CancellationToken ct = default)
        {
            return await _context.ComputerComponents.Include(c=>c.Computer).Where(predicate).ToListAsync(ct);
        }

        public async Task UpdateAsync(Components entity, CancellationToken ct = default)
        {
            if (!await ContainsByEntityAsync(entity, ct))
                throw new NotFoundException(String.Format(ResExceptions.ID_NAO_ENCONTRADO, entity.Id, nameof(_context.ComputerComponents)));
            try
            {
                _context.ComputerComponents.Update(entity);
                await _context.SaveChangesAsync(ct);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new DbConCurrencyException(String.Format(
                    ResExceptions.ERRO_DE_CONCORRENCIA,
                    ResExceptions.UPDATE,
                    entity.Id,
                    nameof(_context.ComputerComponents),
                    ex.Message));
            }
        }

        public async Task UpdateListAsync(List<Components> entities, CancellationToken ct = default)
        {
            var list = await GetListByConditionAsync(x => entities.Select(e => e.Id).Contains(x.Id), ct);
            if (list is null || list.Count != entities.Count)
                throw new NotFoundException(String.Format(
                    ResExceptions.CONDICAO_NAO_ATENDIDA,
                    ResExceptions.UPDATE,
                    nameof(_context.ComputerComponents)));
            try
            {
                _context.ComputerComponents.UpdateRange(entities);
                await _context.SaveChangesAsync(ct);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new DbConCurrencyException(String.Format(
                    ResExceptions.ERRO_DE_CONCORRENCIA,
                    ResExceptions.UPDATE,
                    nameof(_context.ComputerComponents),
                    ex.Message));
            }
        }
    }
}
