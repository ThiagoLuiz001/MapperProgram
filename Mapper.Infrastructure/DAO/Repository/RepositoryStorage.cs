using Mapper.Communication.Exceptions;
using Mapper.Domain.Entities.Machine.ComputerParts;
using Mapper.Domain.Interfaces;
using Mapper.Exception;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;



namespace Mapper.Infrastructure.DAO.Repository
{
    public class RepositoryStorage : IRepository<Storage, int>
    {
        private readonly MapperContext _context;    
        public RepositoryStorage(MapperContext context)
        {
            _context = context;
        }

        public async Task<bool> ContainsByEntityAsync(Storage entity, CancellationToken ct = default)
        {
            return await _context.Storages.ContainsAsync(entity,ct);
        }

        public async Task<int> CountByConditionAsync(Expression<Func<Storage, bool>> predicate, CancellationToken ct = default)
        {
            return await _context.Storages.Where(predicate).CountAsync(ct);
        }

        public async Task CreateAsync(Storage entity, CancellationToken ct = default)
        {
            _context.Storages.Add(entity);
            await _context.SaveChangesAsync(ct);
        }

        public async Task CreateListAsync(List<Storage> entities, CancellationToken ct = default)
        {
            _context.Storages.AddRange(entities);
            await _context.SaveChangesAsync(ct);
        }

        public async Task DeleteAsyncById(int id, CancellationToken ct = default)
        {
            try
            {
                var obj = await GetByIdAsync(id, ct) ?? throw new NotFoundException(String.Format(ResExceptions.ID_NAO_ENCONTRADO, id, nameof(_context.Storages)));
                _context.Storages.Remove(obj!);
                await _context.SaveChangesAsync(ct);
            }
            catch(DbUpdateException ex)
            {
                throw new DbConCurrencyException(String.Format(ResExceptions.ERRO_DE_CONCORRENCIA, ResExceptions.DELETE, id, nameof(_context.Storages), ex.Message));
            }
        }

        public async Task DeleteByCondition(Expression<Func<Storage, bool>> predicate, CancellationToken ct = default)
        {
            var list = await GetListByConditionAsync(predicate, ct);
            if (!list!.Any())
                throw new NotFoundException(String.Format(ResExceptions.CONDICAO_NAO_ATENDIDA, ResExceptions.DELETE, nameof(_context.Storages)));
            try
            {
                _context.RemoveRange(list!);
                await _context.SaveChangesAsync(ct);
            }
            catch(DbUpdateException ex)
            {
                throw new DbConCurrencyException(String.Format(ResExceptions.ERRO_DE_CONCORRENCIA, ResExceptions.DELETE,"-", nameof(_context.Storages), ex.Message));
            }
        }

        public async Task<bool> ExistByConditionAsync(Expression<Func<Storage, bool>> predicate, CancellationToken ct = default)
        {
            return await _context.Storages.AnyAsync(predicate, ct);
        }

        public async Task<List<Storage>?> GetAllAsync(CancellationToken ct = default)
        {
            return await _context.Storages.Include(x=>x.Computer).ToListAsync(ct);
        }

        public async Task<Storage?> GetByConditionAsync(Expression<Func<Storage, bool>> predicate, CancellationToken ct = default)
        {
            return await _context.Storages.Include(x => x.Computer).FirstOrDefaultAsync(predicate,ct);
        }

        public async Task<Storage?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            return await _context.Storages.Include(c=>c.Computer).FirstOrDefaultAsync(x => x.Id == id, ct);
        }

        public async Task<List<Storage>?> GetListByConditionAsync(Expression<Func<Storage, bool>> predicate, CancellationToken ct = default)
        {
            return await _context.Storages.Where(predicate).Include(x => x.Computer).ToListAsync(ct);
        }

        public async Task UpdateAsync(Storage entity, CancellationToken ct = default)
        {
            if (!await ContainsByEntityAsync(entity, ct))
                throw new NotFoundException(String.Format(ResExceptions.ID_NAO_ENCONTRADO, entity.Id, nameof(_context.Storages)));
            try
            {
                _context.Storages.Update(entity);
                await _context.SaveChangesAsync(ct);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new DbConCurrencyException(String.Format(ResExceptions.ERRO_DE_CONCORRENCIA, ResExceptions.UPDATE, entity.Id, nameof(_context.Storages), ex.Message));
            }
        }

        public async Task UpdateListAsync(List<Storage> entities, CancellationToken ct = default)
        {
            var list = await _context.Storages.Where(x => entities.Select(e => e.Id).Contains(x.Id)).ToListAsync(ct);
            if (list.Count != entities.Count)
                throw new NotFoundException(String.Format(ResExceptions.ID_NAO_ENCONTRADO, "-", nameof(_context.Storages)));
            try
            {
                _context.Storages.UpdateRange(entities);
                await _context.SaveChangesAsync(ct);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new DbConCurrencyException(String.Format(ResExceptions.ERRO_DE_CONCORRENCIA, ResExceptions.UPDATE, "-", nameof(_context.Storages), ex.Message));
            }
        }


    }
}
