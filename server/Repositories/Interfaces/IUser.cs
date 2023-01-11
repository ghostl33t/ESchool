﻿using server.Models.Domain;
using server.Models.DTOs.UsersDTO;

namespace server.Repositories.Interfaces
{
    public interface IUser
    {
        public Task<IEnumerable<User>> GetAllAsync();
        public Task<User> GetUserAsync(long id);
        public Task<long> CreateUserAsync(User newUser);
        public Task<long> UpdateUserAsync(long Id, User user);
        public Task<bool> DeleteUserAsync(long UserId, long AdministratorId);

        // Dashboard
        public Task<UserStudentDashboard> GetUserStudentDashboard(long Id);
        public Task<float> AverageGrade(long classDepartmentId, long studentId);

    }
}
