﻿using BuberDinner.Application.Common.Errors;
using BuberDinner.Application.Common.Interfaces.Authentication;
using BuberDinner.Application.Common.Persistence;
using BuberDinner.Domain.Common.Errors;
using BuberDinner.Domain.Entities;
using ErrorOr;
using FluentResults;

namespace BuberDinner.Application.Services.Authentication
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

        public ErrorOr<AuthenticationResult> Register(string firstName, string lastName, string email, string password)
        {
            //1. Validate the user doesn't exist
            if(_userRepository.GetUserByEmail(email) is not null)
                return Errors.User.DuplicateEmail;

            //2. Create user (generate unique Id) & Persist to Db
            var user = new User
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Password = password
            };

            _userRepository.Add(user);

            //3. Create Jwt Token
            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(
                user, 
                token);
        }

        public ErrorOr<AuthenticationResult> Login(string email, string password)
        {
            //1. Validate the user exists
            var user = _userRepository.GetUserByEmail(email);
            if (user is null)
                return Errors.Authentication.InvalidCredentials;

            //2. Validate the password is correct
            if(user.Password != password)
                return new[] { Errors.Authentication.InvalidCredentials };

            //3. Create Jwt Token
            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(
                user, 
                token);
        }
    }
}
