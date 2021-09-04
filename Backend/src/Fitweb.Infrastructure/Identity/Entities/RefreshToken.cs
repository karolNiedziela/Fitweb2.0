using Fitweb.Infrastructure.Identity.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Infrastructure.Identity.Entities
{
    public class RefreshToken
    {
        public Guid Id { get; set; }

        public string Username { get; set; }

        public string Token { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? RevokedAt { get; set; }

        public bool IsRevoked => RevokedAt.HasValue;

        public RefreshToken()
        {

        }

        public RefreshToken(string username, string token, DateTime createdAt, DateTime? revoketAt = null)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                throw new InvalidRefreshTokenException();
            }

            Id = Guid.NewGuid();
            Username = username;
            Token = token;
            CreatedAt = createdAt;
            RevokedAt = revoketAt;
        }

        public void Use(DateTime usedAt)
        {
            if (IsRevoked)
            {
                throw new RevokedRefreshTokenException();
            }

            RevokedAt = usedAt;
        }
    }
}
