using Mapper.Communication.Exceptions;
using Mapper.Domain.Entities.Machine;
using Mapper.Domain.Interfaces;
using Mapper.Exception;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Mapper.Infrastructure.DAO.Repository
{
    internal class RepositoryComputer : IRepository<Computer, int>
    {
        private readonly MapperContext _context;
        public RepositoryComputer(MapperContext context) => _context = context;
        public async Task<bool> ContainsByConditionAsync(Computer entity, CancellationToken ct = default)
        {
            return await _context.Computers.ContainsAsync(entity, ct);
        }

        public async Task<int> CountByConditionAsync(Expression<Func<Computer, bool>> predicate, CancellationToken ct = default)
        {
            return await _context.Computers.CountAsync(predicate, ct);
        }

        public async Task CreateAsync(Computer entity, CancellationToken ct = default)
        {
            _context.Computers.Add(entity);
            await _context.SaveChangesAsync(ct);
        }

        public async Task CreateListAsync(List<Computer> entities, CancellationToken ct = default)
        {
            _context.Computers.AddRange(entities);
            await _context.SaveChangesAsync(ct);
        }

        public async Task DeleteAsyncById(int id, CancellationToken ct = default)
        {
            var obj = await GetByIdAsync(id, ct) ?? 
                throw new NotFoundException(String.Format(ResExceptions.ID_NAO_ENCONTRADO, id, nameof(_context.Computers)));
            try
            {
                _context.Computers.Remove(obj);
                await _context.SaveChangesAsync(ct);
            }
            catch (DbUpdateException ex)
            {
                throw new DbConCurrencyException(String.Format(ResExceptions.ERRO_DE_CONCORRENCIA, ResExceptions.DELETE, id, nameof(_context.Computers), ex.Message));
            }
        }

        public async Task DeleteByCondition(Expression<Func<Computer, bool>> predicate, CancellationToken ct = default)
        {
            var list = await GetListByConditionAsync(predicate, ct) ?? 
                throw new NotFoundException(String.Format(ResExceptions.CONDICAO_NAO_ATENDIDA, ResExceptions.DELETE, nameof(_context.Storages)));
            try
            {
                _context.Computers.RemoveRange(list);
                await _context.SaveChangesAsync(ct);
            }
            catch (DbUpdateException ex)
            {
                throw new DbConCurrencyException(String.Format(ResExceptions.ERRO_DE_CONCORRENCIA, ResExceptions.DELETE, "-", nameof(_context.Storages), ex.Message));
            }
        }

        public async Task<bool> ExistByConditionAsync(Expression<Func<Computer, bool>> predicate, CancellationToken ct = default)
        {
            return await _context.Computers.AnyAsync(predicate, ct);
        }

        public async Task<List<Computer>?> GetAllAsync(CancellationToken ct = default)
        {
            return await _context.Computers.Include(c => c.Equipament)
                .Include(c => c.Ip)
                .Include(c => c.Ip)
                .Include(c => c.Motherboards)
                .Include(c => c.CPUs)
                .Include(c => c.Memory)
                .Include(c => c.Components)
                .Include(c => c.GPUs)
                .Include(c => c.Storages)
                .ToListAsync(ct);
        }

        public async Task<Computer?> GetByConditionAsync(Expression<Func<Computer, bool>> predicate, CancellationToken ct = default)
        {
             return await _context.Computers.Include(c => c.Equipament)
                .Include(c => c.Ip)
                .Include(c => c.Motherboards)
                .Include(c => c.CPUs)
                .Include(c => c.Memory)
                .Include(c => c.Components)
                .Include(c => c.GPUs)
                .Include(c => c.Storages)
                .FirstOrDefaultAsync(predicate, ct);
        }

        public async Task<Computer?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            return await _context.Computers.Include(c => c.Equipament)
                .Include(c => c.Ip)
                .Include(c => c.Motherboards)
                .Include(c => c.CPUs)
                .Include(c => c.Memory)
                .Include(c => c.Components)
                .Include(c => c.GPUs)
                .Include(c => c.Storages)
                .FirstOrDefaultAsync(c => c.Id == id, ct);
        }

        public async Task<List<Computer>?> GetListByConditionAsync(Expression<Func<Computer, bool>> predicate, CancellationToken ct = default)
        {
            return await _context.Computers.Include(c => c.Equipament)
                .Include(c => c.Ip)
                .Include(c => c.Motherboards)
                .Include(c => c.CPUs)
                .Include(c => c.Memory)
                .Include(c => c.Components)
                .Include(c => c.GPUs)
                .Include(c => c.Storages)
                .Where(predicate).ToListAsync(ct);
        }

        public async Task UpdateAsync(Computer entity, CancellationToken ct = default)
        {
            if (!await ExistByConditionAsync(c => c.Id == entity.Id, ct))
            {
                throw new NotFoundException(String.Format(ResExceptions.ID_NAO_ENCONTRADO, entity.Id, nameof(_context.Computers)));
            }
            try
            {
                _context.Computers.Update(entity);
                await _context.SaveChangesAsync(ct);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new DbConCurrencyException(String.Format(ResExceptions.ERRO_DE_CONCORRENCIA, ResExceptions.UPDATE, entity.Id, nameof(_context.Computers), ex.Message));
            }
        }

        public async Task UpdateListAsync(List<Computer> entities, CancellationToken ct = default)
        {
            var list = await GetListByConditionAsync(c => entities.Select(e => e.Id).Contains(c.Id), ct);
            if (list == null || list.Count != entities.Count)
                throw new NotFoundException(String.Format(ResExceptions.CONDICAO_NAO_ATENDIDA, ResExceptions.UPDATE, nameof(_context.Computers)));
            try
            {
                _context.Computers.UpdateRange(entities);
                await _context.SaveChangesAsync(ct);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new DbConCurrencyException(String.Format(ResExceptions.ERRO_DE_CONCORRENCIA, ResExceptions.UPDATE, "-", nameof(_context.Computers), ex.Message));
            }
        }
    }
}
