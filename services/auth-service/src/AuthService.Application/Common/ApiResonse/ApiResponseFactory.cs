using AuthService.Application.Authentication.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthService.Application.Common.ApiResonse
{
    public static class ApiResponseFactory
    {
        public static ApiResponse<T> Success<T>(
            T data,
            string message = "Request successful")
        {
            return new ApiResponse<T>
            {
                Success = true,
                Message = message,
                Data = data
            };
        }

        public static ApiResponse<T> Failed<T>(
            string message)
        {
            return new ApiResponse<T>
            {
                Success = false,
                Message = message,
                Data = default
            };
        }
    }
}
