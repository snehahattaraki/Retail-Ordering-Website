using System;
using System.Threading.Tasks;
using AutoMapper;
using RetailOrdering.Application.DTOs.Auth;
using RetailOrdering.Application.Exceptions;
using RetailOrdering.Application.Interfaces.Services;
using RetailOrdering.Application.Interfaces.Repositories;

namespace RetailOrdering.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IGenericRepository<Domain.Entities.User> _userRepo;
        private readonly IMapper _mapper;
        private readonly RetailOrdering.Infrastructure.Identity.IJwtTokenGenerator _jwt;

        public AuthService(IGenericRepository<Domain.Entities.User> userRepo, IMapper mapper, RetailOrdering.Infrastructure.Identity.IJwtTokenGenerator jwt)
        {
            _userRepo = userRepo;
            _mapper = mapper;
            _jwt = jwt;
        }

        public async Task<AuthResponseDto> LoginAsync(LoginRequestDto dto)
        {
            var users = await _userRepo.FindAsync(u => u.Email == dto.Email);
            var user = System.Linq.Enumerable.FirstOrDefault(users);
            if (user == null) throw new NotFoundException("User not found");
            if (!RetailOrdering.Infrastructure.Identity.PasswordHasher.Verify(dto.Password, user.PasswordHash))
                throw new UnauthorizedAccessException("Invalid credentials");

            var token = _jwt.CreateToken(user);
            return new AuthResponseDto(token.Token, token.RefreshToken, token.ExpiresAt);
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterRequestDto dto)
        {
            var exists = await _userRepo.FindAsync(u => u.Email == dto.Email);
            if (System.Linq.Enumerable.Any(exists)) throw new Exception("User already exists");
            var user = new Domain.Entities.User
            {
                Id = Guid.NewGuid(),
                Email = dto.Email,
                FullName = dto.FullName,
                PasswordHash = RetailOrdering.Infrastructure.Identity.PasswordHasher.Hash(dto.Password)
            };
            await _userRepo.AddAsync(user);
            var token = _jwt.CreateToken(user);
            return new AuthResponseDto(token.Token, token.RefreshToken, token.ExpiresAt);
        }

        public Task<AuthResponseDto> RefreshTokenAsync(string refreshToken)
        {
            throw new NotImplementedException();
        }
    }
}
