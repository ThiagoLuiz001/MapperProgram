using Mapper.Communication.Exceptions;
using Mapper.Domain.Entities.Machine.ComputerParts;
using Mapper.Domain.Interfaces;
using Mapper.Exception;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Text;

namespace Mapper.Infrastructure.DAO.Repository
{
    public class RepositoryGPU : IRepository<GPU, int>
    {
        private readonly MapperContext _context;

        public RepositoryGPU(MapperContext context)
        {
            _context = context;
        }

        public async Task<bool> ContainsByConditionAsync(GPU entity, CancellationToken ct = default)
        {
            return await _context.GPUs.ContainsAsync(entity, ct);
        }

        public async Task<int> CountByConditionAsync(Expression<Func<GPU, bool>> predicate, CancellationToken ct = default)
        {
            return await _context.GPUs.CountAsync(predicate, ct);
        }

        public async Task CreateAsync(GPU entity, CancellationToken ct = default)
        {
            _context.GPUs.Add(entity);
            await _context.SaveChangesAsync(ct);
        }

        public async Task CreateListAsync(List<GPU> entities, CancellationToken ct = default)
        {
            _context.GPUs.AddRange(entities);
            await _context.SaveChangesAsync(ct);
        }

        public Task DeleteAsyncById(int id, CancellationToken ct = default)
        {
            var obj = GetByIdAsync(id, ct).Result ?? throw new NotFoundException(String.Format(ResExceptions.ID_NAO_ENCONTRADO, id, nameof(_context.GPUs)));
            try
            {
                _context.GPUs.Remove(obj);
                return _context.SaveChangesAsync(ct);
            }
            catch (DbUpdateException ex)
            {
                throw new DbConCurrencyException(String.Format(ResExceptions.ERRO_DE_CONCORRENCIA, ResExceptions.DELETE, id, nameof(_context.GPUs), ex.Message));
            }
        }

        public async Task DeleteByCondition(Expression<Func<GPU, bool>> predicate, CancellationToken ct = default)
        {
            var list = await GetListByConditionAsync(predicate, ct);
            if (!list!.Any())
                throw new NotFoundException(String.Format(ResExceptions.CONDICAO_NAO_ATENDIDA, ResExceptions.DELETE, nameof(_context.GPUs)));
            try
            {
                _context.GPUs.RemoveRange(list!);
                await _context.SaveChangesAsync(ct);
            }
            catch (DbUpdateException ex)
            {
                throw new DbConCurrencyException(String.Format(ResExceptions.ERRO_DE_CONCORRENCIA, ResExceptions.DELETE, nameof(_context.GPUs), ex.Message));
            }
        }

        public async Task<bool> ExistByConditionAsync(Expression<Func<GPU, bool>> predicate, CancellationToken ct = default)
        {
            return await _context.GPUs.AnyAsync(predicate, ct);
        }

        public async Task<List<GPU>?> GetAllAsync(CancellationToken ct = default)
        {
            return await _context.GPUs.Include(x=> x.Computer).ToListAsync(ct);
        }

        public async Task<GPU?> GetByConditionAsync(Expression<Func<GPU, bool>> predicate, CancellationToken ct = default)
        {
            return await _context.GPUs.Include(x => x.Computer).FirstOrDefaultAsync(predicate, ct);
        }

        public async Task<GPU?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            return await _context.GPUs.Include(x => x.Computer).FirstOrDefaultAsync(x => x.Id == id, ct);
        }

        public async Task<List<GPU>?> GetListByConditionAsync(Expression<Func<GPU, bool>> predicate, CancellationToken ct = default)
        {
            return await _context.GPUs.Include(x => x.Computer).Where(predicate).ToListAsync(ct);
        }

        public async Task UpdateAsync(GPU entity, CancellationToken ct = default)
        {
            if (!await ExistByConditionAsync(x => x.Id == entity.Id, ct))
                throw new NotFoundException(String.Format(ResExceptions.ID_NAO_ENCONTRADO, entity.Id, nameof(_context.GPUs)));
            try
            {
                _context.GPUs.Update(entity);
                await _context.SaveChangesAsync(ct);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new DbConCurrencyException(String.Format(ResExceptions.ERRO_DE_CONCORRENCIA, ResExceptions.UPDATE, entity.Id, nameof(_context.GPUs), ex.Message));
            }
        }

        public async Task UpdateListAsync(List<GPU> entities, CancellationToken ct = default)
        {
            var list = await GetListByConditionAsync(x => entities.Select(e => e.Id).Contains(x.Id), ct);
            if (list == null || list.Count != entities.Count)
                throw new NotFoundException(String.Format(ResExceptions.ID_NAO_ENCONTRADO, string.Join(", ", entities.Select(e => e.Id)), nameof(_context.GPUs)));
            try
            {
                _context.GPUs.UpdateRange(entities);
                await _context.SaveChangesAsync(ct);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new DbConCurrencyException(String.Format(ResExceptions.ERRO_DE_CONCORRENCIA, ResExceptions.UPDATE, string.Join(", ", entities.Select(e => e.Id)), nameof(_context.GPUs), ex.Message));
            }
        }
    }
}
