using Fitweb.Application.Interfaces;
using Fitweb.Infrastructure.Identity.Entities;
using Fitweb.Infrastructure.Identity.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Infrastructure.Identity.Factories
{
    public class RefreshTokenFactory : IRefreshTokenFactory
    {
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IRng _rng;

        public RefreshTokenFactory(IDateTimeProvider dateTimeProvider, IRng rng)
        {
            _dateTimeProvider = dateTimeProvider;
            _rng = rng;
        }

        public RefreshToken Create(string username)
        {
            return new RefreshToken(username, _rng.Generate(), _dateTimeProvider.Now);
        }
    }
}
