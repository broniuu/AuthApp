using AuthApp.Data;
using AuthApp.Dtos;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuthApp.Controllers;

[Authorize]
public class UserController : ControllerBase
{
    private readonly IMapper _mapper;


    public UserController(IMapper mapper)
    {
        _mapper = mapper;
    }

    [HttpGet("getUsers")]
    public async Task<ActionResult<IEnumerable<MemberDto>>> Register()
    {
        var db = new DataContext();
        var users = (await db.AppUsers.ToListAsync()).Select(u => _mapper.Map<MemberDto>(u));
        return Ok(users);
    }
}