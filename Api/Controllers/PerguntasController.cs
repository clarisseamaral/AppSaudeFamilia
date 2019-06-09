﻿using System;
using System.Collections.Generic;
using System.Data;
//using System.Data.Entity;
//using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using ColetaApi.Data;
using ColetaApi.Dtos;
using ColetaApi.Helper;
//using System.Web.Http.Description;
//using Coleta.Models;
//using Coleta.Models.Api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ColetaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PerguntasController : ControllerBase
    {
        private readonly ColetaContext db;

        public PerguntasController(ColetaContext db)
        {
            this.db = db;
        }

        // GET: api/Perguntas
        [HttpGet]
        public Task<List<PerguntaDto>> GetPerguntasAsync([FromQuery] bool listaSimplificada = false)
        {
            return GetInternalAsync(listaSimplificada);
        }

        // GET: api/Perguntas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PerguntaDto>> GetPerguntaAsync(int id)
        {
            var pergunta = await db.Pergunta.Include(p => p.OpcaoRespostaPergunta)
                                            .Include(p => p.IdTipoPerguntaNavigation)
                                            .FirstOrDefaultAsync(p => p.Id == id);
            if (pergunta == null)
                return NotFound();

            return Ok(new PerguntaDto(pergunta));
        }

        internal async Task<List<PerguntaDto>> GetInternalAsync(bool listaSimplificada = false, int? idQuestionario = null)
        {
            IQueryable<Pergunta> basePerguntas = db.Pergunta;
            if (!listaSimplificada)
                basePerguntas = basePerguntas.Include(p => p.OpcaoRespostaPergunta).Include(p => p.IdTipoPerguntaNavigation);

            return await (from pergunta in basePerguntas
                          //where pergunta.FlgAtivo == true
                          orderby pergunta.Ordem
                          select pergunta).ToListAsync(p => new PerguntaDto(p));
        }
    }
}