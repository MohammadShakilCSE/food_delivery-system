using AuthService.Application.Authentication.Common;
using AuthService.Application.Common.Interfaces;
using AuthService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthService.Application.Authentication.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;

        public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
        }

        public async Task<AuthenticationResponse> RegisterAsync(string firstName, string lastName, string email, string password)
        {
            // 1. Check if user already exists
            var existingUserTask = _userRepository.GetByEmailAsync(email);
            existingUserTask.Wait();
            if (existingUserTask.Result is not null)
            {
                throw new Exception("User with this email already exists.");
            }

            // 2. Create user (In a real app, hash the password!)
            var user = new User
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                PasswordHash = password
            };

            await _userRepository.AddAsync(user);

            // 3. Create JWT token
            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResponse(user.Id, user.FirstName, user.LastName, user.Email, token);
        }

        public  async Task<AuthenticationResponse> LoginAsync(string email, string password)
        {
            // 1. Validate the user exists
            var user = await _userRepository.GetByEmailAsync(email);
          

            if (user is null)
            {
                throw new Exception("Invalid email or password.");
            }

            // 2. Validate the password
            if (user.PasswordHash != password)
            {
                throw new Exception("Invalid email or password.");
            }

            // 3. Create JWT token
            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResponse(user.Id, user.FirstName, user.LastName, user.Email, token);
        }
    }
}
