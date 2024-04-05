
using Blog.Domain.Core.Entities;
using Blog.Domain.Account.Entities;

namespace Blog.Domain.Account.Services
{
    public interface ITokenService
    {
        Task<UserToken> GenerateTokenAsync(User user);
    }
}
