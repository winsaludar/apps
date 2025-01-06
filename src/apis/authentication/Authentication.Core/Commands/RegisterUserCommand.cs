﻿namespace Authentication.Core.Commands;

public class RegisterUserCommandHandler(IUserRepository userRepository) : IRequestHandler<RegisterUserCommand, User>
{
    public async Task<User> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        // Make sure email and username are unique
        User? existingUser;        
        existingUser = await userRepository.GetByEmailAsync(request.Email);
        if (existingUser != null)
            throw new BadRequestException("Email already exist");
        existingUser = await userRepository.GetByUsernameAsync(request.Username);
        if (existingUser != null)
            throw new BadRequestException("Username already exist");

        // Make sure password conforms with our requirement
        bool isPasswordValid = await userRepository.ValidatePasswordAsync(request.Password);
        if (!isPasswordValid)
            throw new BadRequestException("Password do not meet our minimum requirements");

        User newUser = User.Create(request.Username, request.Email, request.Password);      
        Guid newId = await userRepository.RegisterAsync(newUser);
        newUser.SetId(newId);

        return newUser;
    }
}

public record RegisterUserCommand(string Username, string Email, string Password, string RetypePassword) : IRequest<User>;