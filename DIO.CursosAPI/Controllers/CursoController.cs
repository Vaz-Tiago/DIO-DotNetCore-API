using DIO.CursosAPI.Business.Entities;
using DIO.CursosAPI.Business.Repositories;
using DIO.CursosAPI.Models.Curso;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DIO.CursosAPI.Controllers
{
    [Route("api/v1/cursos")]
    [ApiController]
    [Authorize]
    public class CursoController : ControllerBase
    {
        private readonly ICursoRepository _cursoRepository;

        public CursoController(ICursoRepository cursoRepository)
        {
            _cursoRepository = cursoRepository;
        }

        #region POST - Cria Curso Usuário Logado

        #region Configurações de rota
        /// <summary>
        /// Permite cadastrar um curso para o usuário autenticado
        /// </summary>
        /// <param name="cursoViewModelInput"></param>
        /// <returns>Retorna status 201 e dados do curso do usuário</returns>
        [SwaggerResponse(statusCode: 201, description: "Sucesso ao Cadastrar um curso")]
        [SwaggerResponse(statusCode: 401, description: "Não autorizado")]
        [HttpPost]
        [Route("")]
        #endregion
        public async Task<IActionResult> Post(CursoViewModelInput cursoViewModelInput)
        {
            Curso curso = new Curso();

            var codigoUsuario = int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
            curso.Nome = cursoViewModelInput.Nome;
            curso.Descricao = cursoViewModelInput.Descricao;
            curso.CodigoUsuario = codigoUsuario;


            _cursoRepository.Adicionar(curso);
            _cursoRepository.Commit();

            return Created("", cursoViewModelInput);
        }
        #endregion

        #region GET - Exibe Cursos Usuário Logado

        #region Configurações Rota
        /// <summary>
        /// Permite obter todos os cursos ativos do usuário
        /// </summary>
        /// <returns>Retorna status ok e dados dos cursos do usuário</returns>
        [SwaggerResponse(statusCode: 200, description: "Sucesso ao obter cursos")]
        [SwaggerResponse(statusCode: 401, description: "Não autorizado")]
        [HttpGet]
        [Route("")]
        #endregion
        public async Task<IActionResult> Get()
        {
            var codigoUsuario = int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value);

            var cursos = _cursoRepository.ObterPorUsuario(codigoUsuario)
                .Select(s => new CursoViewModelOutput()
                {
                    Nome = s.Nome,
                    Descricao = s.Descricao,
                    Login = s.Usuario.Login
                });


            return Ok(cursos);
        }
        #endregion
    }
}
