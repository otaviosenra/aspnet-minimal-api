using MinimalApi.Domain.DTOs;
using MinimalApi.Domain.Entities;

namespace MinimalApi.Domain.Interfaces;

public interface IUserService
{
    bool Login(LoginDTO loginDTO);
}
