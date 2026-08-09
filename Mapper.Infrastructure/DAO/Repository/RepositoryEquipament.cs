using Mapper.Communication.Exceptions;
using Mapper.Domain.Entities;
using Mapper.Domain.Interfaces;
using Mapper.Exception;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;


namespace Mapper.Infrastructure.DAO.Repository
{
    public class RepositoryEquipament : IRepository<Equipament, Guid>
    {
        private readonly MapperContext _context;
        
        public RepositoryEquipament(MapperContext context)
        {
            _context = context;
        }

        public async Task<bool> ContainsByConditionAsync(Equipament entity, CancellationToken ct = default)
        {
            return await _context.Equipaments.ContainsAsync(entity, ct);
        }

        public async Task<int> CountByConditionAsync(Expression<Func<Equipament, bool>> predicate, CancellationToken ct = default)
        {
            return await _context.Equipaments.CountAsync(predicate, ct);
        }

        public async Task CreateAsync(Equipament entity, CancellationToken ct = default)
        {
            _context.Equipaments.Add(entity);
            await _context.SaveChangesAsync(ct);
        }

        public async Task CreateListAsync(List<Equipament> entities, CancellationToken ct = default)
        {
            _context.Equipaments.AddRange(entities);
            await _context.SaveChangesAsync(ct);
        }

        public async Task DeleteAsyncById(Guid id, CancellationToken ct = default)
        {
            var obj = await GetByIdAsync(id, ct) ?? throw new NotFoundException(String.Format(ResExceptions.ID_NAO_ENCONTRADO, id, nameof(_context.Equipaments)));
            try
            {
                _context.Equipaments.Remove(obj);
                await _context.SaveChangesAsync(ct);
            }
            catch (DbUpdateException ex)
            {
                throw new DbConCurrencyException(String.Format(ResExceptions.ERRO_DE_CONCORRENCIA, ResExceptions.DELETE, id, nameof(_context.Equipaments), ex.Message));
            }
        }

        public async Task DeleteByCondition(Expression<Func<Equipament, bool>> predicate, CancellationToken ct = default)
        {
            var list = await _context.Equipaments.Where(predicate).ToListAsync();
            if (!list.Any())
                throw new NotFoundException(String.Format(ResExceptions.CONDICAO_NAO_ATENDIDA, ResExceptions.DELETE, nameof(_context.Storages)));
            try
            {
                _context.Equipaments.RemoveRange(list);
                await _context.SaveChangesAsync(ct);
            }
            catch (DbUpdateException ex)
            {
                throw new DbConCurrencyException(String.Format(ResExceptions.ERRO_DE_CONCORRENCIA, ResExceptions.DELETE, nameof(_context.Equipaments), ex.Message));
            }

        }

        public async Task<bool> ExistByConditionAsync(Expression<Func<Equipament, bool>> predicate, CancellationToken ct = default)
        {
            return await _context.Equipaments.AnyAsync(predicate, ct);
        }

        public async Task<List<Equipament>?> GetAllAsync(CancellationToken ct = default)
        {
            return await _context.Equipaments.ToListAsync(ct);
        }

        public async Task<Equipament?> GetByConditionAsync(Expression<Func<Equipament, bool>> predicate, CancellationToken ct = default)
        {
            return await _context.Equipaments.FirstOrDefaultAsync(predicate, ct);
        }

        public async Task<Equipament?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            return await _context.Equipaments.FindAsync(id, ct);
        }

        public async Task<List<Equipament>?> GetListByConditionAsync(Expression<Func<Equipament, bool>> predicate, CancellationToken ct = default)
        {
            return await _context.Equipaments.Where(predicate).ToListAsync(ct);
        }

        public async Task UpdateAsync(Equipament entity, CancellationToken ct = default)
        {
            if (!await ContainsByConditionAsync(entity, ct))
                throw new NotFoundException(String.Format(ResExceptions.ID_NAO_ENCONTRADO, entity.Id, nameof(_context.Equipaments)));
            try
            {
                _context.Equipaments.Update(entity);
                await _context.SaveChangesAsync(ct);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new ApplicationException(String.Format(ResExceptions.ERRO_DE_CONCORRENCIA, ResExceptions.UPDATE, entity.Id, nameof(_context.Equipaments), ex.Message));
            }
        }

        public async Task UpdateListAsync(List<Equipament> entities, CancellationToken ct = default)
        {
            var list = await _context.Equipaments.Where(x => entities.Select(e => e.Id).Contains(x.Id)).ToListAsync(ct);
            if (list.Count != entities.Count)
                throw new NotFoundException(String.Format(ResExceptions.ID_NAO_ENCONTRADO, String.Join(", ", entities.Select(e => e.Id)), nameof(_context.Equipaments)));
            try
            {
                _context.Equipaments.UpdateRange(entities);
                await _context.SaveChangesAsync(ct);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new ApplicationException(String.Format(ResExceptions.ERRO_DE_CONCORRENCIA, ResExceptions.UPDATE, String.Join(", ", entities.Select(e => e.Id)), nameof(_context.Equipaments), ex.Message));
            }
        }
    }
}
