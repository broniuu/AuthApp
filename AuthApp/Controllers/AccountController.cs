using System.Net;
using System.Net.Mail;
using AuthApp.Data;
using AuthApp.Dtos;
using AuthApp.Interfaces;
using AuthApp.Model;
using AuthApp.Services;
using AutoMapper;
using Isopoh.Cryptography.Argon2;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuthApp.Controllers;

public class AccountController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ITokenService _tokenService;


    public AccountController(IMapper mapper, ITokenService tokenService)
    {
        _mapper = mapper;
        _tokenService = tokenService;
    }

    [HttpPost("register")]
    public async Task<ActionResult<UserDto>> Register([FromBody] RegisterDto registerDto)
    {
        await using var db = new DataContext();
        var sameUserIsInDatabase =
            await db.AppUsers.AnyAsync(x => x.Email == registerDto.Email || x.Login == registerDto.Login);
        if (sameUserIsInDatabase)
            return BadRequest("Login or email is taken");
        var hashedPassword = Argon2.Hash(registerDto.Password);
        var userToAdd = new AppUser
        {
            Password = hashedPassword,
            Email = registerDto.Email,
            Login = registerDto.Login
        };
        var result = db.AppUsers.Add(userToAdd);
        if (result.State != EntityState.Added)
            return BadRequest("Error While adding user to db");
        await db.SaveChangesAsync();
        var userDto = _mapper.Map<UserDto>(userToAdd);
        userDto.Token = _tokenService.CreateToken(userToAdd);
        return userDto;
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login([FromBody] LoginDto loginDto)
    {
        await using var db = new DataContext();
        var user = await db.AppUsers.FirstOrDefaultAsync(u => u.Login == loginDto.Login);

        if (user == null) return Unauthorized("Invalid username");

        var passwordCheckResult = Argon2.Verify(user.Password, loginDto.Password);

        if (!passwordCheckResult) return Unauthorized();

        return new UserDto
        {
            Login = user.Login,
            Token = _tokenService.CreateToken(user),
            Email = user.Email,
        };
    }
}