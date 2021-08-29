using Fitweb.Infrastructure.Identity;
using Fitweb.Infrastructure.Identity.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Infrastructure.Persistence.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly FitwebDbContext _context;

        public RefreshTokenRepository(FitwebDbContext context)
        {
            _context = context;
        }


        public async Task<RefreshToken> GetAsync(string refreshToken)
            => await _context.RefreshTokens.SingleOrDefaultAsync(x => x.Token == refreshToken);

        public async Task AddAsync(RefreshToken refreshToken)
        {
            await _context.RefreshTokens.AddAsync(refreshToken);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(RefreshToken refreshToken)
        {
            _context.RefreshTokens.Update(refreshToken);

            await _context.SaveChangesAsync();
        }
    }
}
