﻿namespace Authentication.Core.Contracts;

public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByUsernameAsync(string username);
    Task<Guid> RegisterAsync(User user);
    Task<bool> ValidatePasswordAsync(string password);
}