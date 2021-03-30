using DIO.CursosAPI.Business.Entities;
using DIO.CursosAPI.Business.Repositories;
using DIO.CursosAPI.Configurations;
using DIO.CursosAPI.Filters;
using DIO.CursosAPI.Infra.Data;
using DIO.CursosAPI.Models;
using DIO.CursosAPI.Models.Usuario;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DIO.CursosAPI.Controllers
{
    [Route("api/v1/usuario")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IAuthenticationService _authetication;

        public UsuarioController(
            IUsuarioRepository usuarioRepository, 
            IAuthenticationService authetication)
        {
            _usuarioRepository = usuarioRepository;
            _authetication = authetication;
        }

        #region Logar

        #region Configurações de rota
        /// <summary>
        /// Permite autenticar um usuário cadastrado e ativo
        /// </summary>
        /// <param name="loginViewModelInput">View Model do login</param>
        /// <returns>Retorna status OK, dados do usuário e o token</returns>
        [SwaggerResponse(statusCode: 200, description: "Sucesso ao autenticar", Type = typeof(LoginViewModelInput))]
        [SwaggerResponse(statusCode: 400, description: "Campos obrigatórios", Type = typeof(ValidaCamposViewModelOutput))]
        [SwaggerResponse(statusCode: 500, description: "Erro Interno", Type = typeof(ErroGenericoViewModel))]
        [HttpPost]
        [Route("logar")]
        [ValidacaoModelStateCustomizado]
        #endregion
        public IActionResult Logar(LoginViewModelInput loginViewModelInput)
        {
            Usuario usuario = _usuarioRepository.ObterUsuario(loginViewModelInput);

            if (usuario == null)
                return BadRequest("Houve um erro ao tentar acessar");

            var usuarioViewModelOutput = new UsuarioViewModelOutput()
            {
                Codigo = usuario.Codigo,
                Login = usuario.Login,
                Email = usuario.Senha
            };

            var token = _authetication.GerarToken(usuarioViewModelOutput);

            return Ok(new 
            {
                Token = token,
                Usuario = usuarioViewModelOutput
            });
        }

        #endregion

        #region Registrar
        [HttpPost]
        [Route("registrar")]
        [ValidacaoModelStateCustomizado]
        public IActionResult Registrar(RegistroViewModelInput registroViewModelInput)
        {
            //var migracoesPendentes = contexto.Database.GetPendingMigrations();
            //if (migracoesPendentes.Count() > 0)
            //    contexto.Database.Migrate();

            var usuario = new Usuario();
            usuario.Login = registroViewModelInput.Login;
            usuario.Senha = registroViewModelInput.Senha;
            usuario.Email = registroViewModelInput.Email;

            _usuarioRepository.Adicionar(usuario);
            _usuarioRepository.Commit();

            return Created("", registroViewModelInput);
        }
        #endregion
    }
}
