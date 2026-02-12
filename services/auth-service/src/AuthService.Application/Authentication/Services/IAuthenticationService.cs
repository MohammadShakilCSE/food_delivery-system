using AuthService.Application.Authentication.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthService.Application.Authentication.Services
{
    public interface IAuthenticationService
    {
        Task<ApiResponse<AuthenticationResponse>> RegisterAsync(string firstName, string lastName, string email, string password);
        Task<ApiResponse<AuthenticationResponse>> LoginAsync(string email, string password);
    }
}
