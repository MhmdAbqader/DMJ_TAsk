using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DMJ_Application.Dtos;

namespace DMJ_Application.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<AuthenticationResponseDto> Register(RegisterDto registerDto);
        Task<AuthenticationResponseDto> Login(LoginDto loginDto);
    }
}
