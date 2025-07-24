using MinimalApi.Domain.DTOs;

namespace MinimalApi.Domain.Interfaces;

public interface ILoginService
{
    Task<bool> Login(LoginDTO loginDTO);
}
