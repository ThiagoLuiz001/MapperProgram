using Mapper.Domain.Entities.Machine;
using Mapper.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Mapper.Infrastructure.DAO.Repository
{
    public class RepositoryIP : IRepository<IP, int>
    {
        private readonly MapperContext _context;

        public RepositoryIP(MapperContext context) => _context = context;

        public async Task<bool> ContainsByEntityAsync(IP entity, CancellationToken ct = default)
        {
            return await _context.IPs.ContainsAsync(entity, ct);
        }

        public async Task<int> CountByConditionAsync(Expression<Func<IP, bool>> predicate, CancellationToken ct = default)
        {
            return await _context.IPs.CountAsync(predicate, ct);
        }

        public Task CreateAsync(IP entity, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public Task CreateListAsync(List<IP> entities, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsyncById(int id, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public Task DeleteByCondition(Expression<Func<IP, bool>> predicate, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistByConditionAsync(Expression<Func<IP, bool>> predicate, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public Task<List<IP>?> GetAllAsync(CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public Task<IP?> GetByConditionAsync(Expression<Func<IP, bool>> predicate, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public Task<IP?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public Task<List<IP>?> GetListByConditionAsync(Expression<Func<IP, bool>> predicate, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(IP entity, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public Task UpdateListAsync(List<IP> entities, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }
    }
}
