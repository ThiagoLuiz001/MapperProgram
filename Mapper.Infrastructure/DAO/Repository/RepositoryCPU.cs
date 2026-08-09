using Mapper.Communication.Exceptions;
using Mapper.Domain.Entities.Machine.ComputerParts;
using Mapper.Domain.Interfaces;
using Mapper.Exception;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Mapper.Infrastructure.DAO.Repository
{
    internal class RepositoryCPU : IRepository<CPU, int>
    {
        private readonly MapperContext _context;

        public RepositoryCPU(MapperContext context)
        {
            _context = context;
        }
        public async Task<bool> ContainsByConditionAsync(CPU entity, CancellationToken ct = default)
        {
            return await _context.CPUs.ContainsAsync(entity, ct);
        }

        public async Task<int> CountByConditionAsync(Expression<Func<CPU, bool>> predicate, CancellationToken ct = default)
        {
            return await _context.CPUs.CountAsync(predicate, ct);
        }

        public async Task CreateAsync(CPU entity, CancellationToken ct = default)
        {
            _context.CPUs.Add(entity);
            await _context.SaveChangesAsync(ct);
        }

        public async Task CreateListAsync(List<CPU> entities, CancellationToken ct = default)
        {
            _context.AddRange(entities);
            await _context.SaveChangesAsync(ct);
        }

        public async Task DeleteAsyncById(int id, CancellationToken ct = default)
        {
            var obj = await GetByIdAsync(id,ct) ?? throw new NotFoundException(String.Format(ResExceptions.ID_NAO_ENCONTRADO, id, nameof(_context.CPUs))); try
            {
                _context.Remove(obj);
                await _context.SaveChangesAsync(ct);
            }
            catch(DbUpdateException ex)
            {
                throw new DbConCurrencyException(String.Format(ResExceptions.ERRO_DE_CONCORRENCIA, ResExceptions.DELETE, id, nameof(_context.CPUs), ex.Message));
            }
        }

        public async Task DeleteByCondition(Expression<Func<CPU, bool>> predicate, CancellationToken ct = default)
        {
            var list = await GetListByConditionAsync(predicate, ct);
            if(!list!.Any())
                throw new NotFoundException(String.Format(ResExceptions.CONDICAO_NAO_ATENDIDA, ResExceptions.DELETE, nameof(_context.CPUs)));
            try
            {
                _context.RemoveRange(list!);
                await _context.SaveChangesAsync(ct);
            }
            catch(DbUpdateException ex)
            {
                throw new DbConCurrencyException(String.Format(ResExceptions.ERRO_DE_CONCORRENCIA, ResExceptions.DELETE, '-', nameof(_context.Equipaments), ex.Message));
            }
        }

        public Task<bool> ExistByConditionAsync(Expression<Func<CPU, bool>> predicate, CancellationToken ct = default)
        {
            return _context.CPUs.AnyAsync(predicate, ct);
        }

        public async Task<List<CPU>?> GetAllAsync(CancellationToken ct = default)
        {
            return await _context.CPUs.Include(c=>c.Computer).ToListAsync(ct);
        }

        public async Task<CPU?> GetByConditionAsync(Expression<Func<CPU, bool>> predicate, CancellationToken ct = default)
        {
            return await _context.CPUs.Include(c=> c.Computer).FirstOrDefaultAsync(predicate, ct);
        }

        public async Task<CPU?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            return await _context.CPUs.Include(c=> c.Computer).FirstOrDefaultAsync(x=> x.Id == id, ct);
        }

        public async Task<List<CPU>?> GetListByConditionAsync(Expression<Func<CPU, bool>> predicate, CancellationToken ct = default)
        {
            return await _context.CPUs.Include(c=> c.Computer).Where(predicate).ToListAsync(ct);
        }

        public async Task UpdateAsync(CPU entity, CancellationToken ct = default)
        {
            var obj = await GetByIdAsync(entity.Id, ct) ?? throw new NotFoundException(String.Format(ResExceptions.ID_NAO_ENCONTRADO, entity.Id, nameof(_context.CPUs)));
            try
            {
                _context.CPUs.Update(entity);
                await _context.SaveChangesAsync(ct);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new DbConCurrencyException(String.Format(ResExceptions.ERRO_DE_CONCORRENCIA, ResExceptions.UPDATE, entity.Id, nameof(_context.CPUs), ex.Message));
            }
        }

        public async Task UpdateListAsync(List<CPU> entities, CancellationToken ct = default)
        {
            var list = await GetListByConditionAsync(x => entities.Select(e => e.Id).Contains(x.Id), ct);
            if (list!.Count!=entities.Count)
                throw new NotFoundException(String.Format(ResExceptions.CONDICAO_NAO_ATENDIDA, ResExceptions.UPDATE, nameof(_context.CPUs)));
            try
            {
                _context.CPUs.UpdateRange(entities);
                await _context.SaveChangesAsync(ct);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new DbConCurrencyException(String.Format(ResExceptions.ERRO_DE_CONCORRENCIA, ResExceptions.UPDATE, '-', nameof(_context.CPUs), ex.Message));
            }
        }
    }
}
