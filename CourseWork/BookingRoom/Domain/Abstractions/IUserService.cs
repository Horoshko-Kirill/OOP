using System.Linq.Expressions;
using Domain.Models;

namespace Application.Services
{
    public interface IUserService
    {
        Task CreateUser(User user);
        Task DeleteUser(User user);
        Task<bool> ExistsUsers(Expression<Func<User, bool>> predicate);
        Task<IEnumerable<User>> FindUsers(Expression<Func<User, bool>> predicate);
        Task<IEnumerable<User>> GetAllUser();
        Task<User> GetByEmail(string email);
        Task<User> GetByIdUser(int id);
        Task<bool> IsEmailTaken(string email);
        Task UpdateUser(User user);
    }
}