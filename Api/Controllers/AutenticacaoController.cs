using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
//using System.Data.Entity;
//using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using ColetaApi.Data;
using ColetaApi.Dtos;
using ColetaApi.Helper;
using Microsoft.AspNetCore.Authorization;
//using System.Web.Http.Description;
//using Coleta.Models;
//using Coleta.Models.Api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ColetaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticacaoController : ControllerBase
    {
        private readonly ColetaContext db;
        private readonly IConfiguration configuration;

        public AutenticacaoController(ColetaContext db, IConfiguration configuration)
        {
            this.db = db;
            this.configuration = configuration;
        }

        [HttpPost("")]
        [AllowAnonymous]
        public async Task<ActionResult<TokenDto>> PostAutenticacaoAsync([FromBody] LoginDto login)
        {
            var usuario = await db.Usuario.FirstOrDefaultAsync(u => u.Login == login.Usuario);

            if (usuario != null)
            {
                var checkPwd = login.Senha == usuario.Senha;
                if (checkPwd)
                {
                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, usuario.Id.ToString()),
                        //new Claim(JwtRegisteredClaimNames.Jti, usuario.Id.ToString()),
                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["AuthenticationToken:Key"]));
                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken(configuration["AuthenticationToken:Issuer"],
                    configuration["AuthenticationToken:Audience"],
                    claims,
                    expires: DateTime.Now.AddMonths(2),
                    signingCredentials: creds);

                    return Ok(new TokenDto { Token = new JwtSecurityTokenHandler().WriteToken(token) });
                }
            }

            return BadRequest("Could not create token");
        }
    }
}