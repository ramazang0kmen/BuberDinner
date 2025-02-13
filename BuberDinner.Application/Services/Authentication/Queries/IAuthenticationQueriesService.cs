﻿using BuberDinner.Application.Services.Authentication.Common;
using ErrorOr;

namespace BuberDinner.Application.Services.Authentication.Queries
{
    public interface IAuthenticationQueriesService
    {
        ErrorOr<AuthenticationResult> Login(string email, string password);
    }
}
