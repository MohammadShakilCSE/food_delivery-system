using AuthService.Application.Common.Interfaces;
using AuthService.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthService.Infrastructure.UnitOfWorks
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly AuthDbContext _context;
        public IUserRepository Users { get; }

        public UnitOfWork(AuthDbContext context, IUserRepository users)
        {
            _context = context;
            Users = users;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
