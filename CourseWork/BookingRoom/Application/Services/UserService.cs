using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Repository.UserRepository;
using DataAccess.UnitOfWork;
using Domain.Models;

namespace Application.Services
{
    public class UserService : IUserService
    {

        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task CreateUser(User user)
        {
            await _userRepository.AddAsync(user);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteUser(User user)
        {
            await _userRepository.DeleteAsync(user);
            await _unitOfWork.CommitAsync();
        }

        public async Task<bool> ExistsUsers(Expression<Func<User, bool>> predicate)
        {
            return await _userRepository.ExistsAsync(predicate);
        }

        public async Task<IEnumerable<User>> FindUsers(Expression<Func<User, bool>> predicate)
        {
            return await _userRepository.FindAsync(predicate);
        }

        public async Task<IEnumerable<User>> GetAllUser()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task<User> GetByIdUser(int id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public async Task UpdateUser(User user)
        {
            await _userRepository.UpdateAsync(user);
            await _unitOfWork.CommitAsync();
        }

        public async Task<User> GetByEmail(string email)
        {
            return await _userRepository.GetByEmailAsync(email);
        }

        public async Task<bool> IsEmailTaken(string email)
        {
            return await _userRepository.IsEmailTakenAsync(email);
        }

    }
}
