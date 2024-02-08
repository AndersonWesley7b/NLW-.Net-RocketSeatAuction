using RocketSeatAuction.API.Entities;

namespace RocketSeatAuction.API.Contracts;

public interface IUserRepository
{
    bool ExistsUserWithEmail(string Email);

    User GetUserByEmail(string Email);
}
