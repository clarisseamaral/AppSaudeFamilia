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

        // GET: api/Perguntas
        [HttpGet]
        public Task<List<PerguntaDto>> GetAsync([FromQuery] bool listaSimplificada = false)
        {
            return GetInternalAsync(listaSimplificada);
        }

        [HttpPost("")]
        [AllowAnonymous]
        public async Task<ActionResult> PostAsync([FromBody] LoginDto login)
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

                    return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
                }
            }

            return BadRequest("Could not create token");
        }

    // GET: api/Perguntas/5
    [HttpGet("{id}")]
        public async Task<ActionResult<PerguntaDto>> GetAsync(int id)
        {
            var pergunta = await db.Pergunta.Include(p => p.OpcaoRespostaPergunta)
                                            .Include(p => p.IdTipoPerguntaNavigation)
                                            .FirstOrDefaultAsync(p => p.Id == id);
            if (pergunta == null)
                return NotFound();

            return Ok(new PerguntaDto(pergunta));
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        internal async Task<List<PerguntaDto>> GetInternalAsync(bool listaSimplificada = false, int? idQuestionario = null)
        {
            IQueryable<Pergunta> basePerguntas = db.Pergunta;
            if (!listaSimplificada)
                basePerguntas = basePerguntas.Include(p => p.OpcaoRespostaPergunta).Include(p => p.IdTipoPerguntaNavigation);

            return await (from pergunta in basePerguntas
                          //where pergunta.FlgAtivo == true
                          select pergunta).ToListAsync(p => new PerguntaDto(p));
        }
    }
}