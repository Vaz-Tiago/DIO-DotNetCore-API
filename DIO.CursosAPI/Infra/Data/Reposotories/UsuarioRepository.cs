using DIO.CursosAPI.Business.Entities;
using DIO.CursosAPI.Business.Repositories;
using DIO.CursosAPI.Models.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DIO.CursosAPI.Infra.Data.Reposotories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly CursoDbContext _contexto;

        public UsuarioRepository(CursoDbContext contexto)
        {
            this._contexto = contexto;
        }

        public void Adicionar(Usuario usuario)
        {
            _contexto.Usuario.Add(usuario);
        }

        public void Commit()
        {
            _contexto.SaveChanges();
        }

        public Usuario ObterUsuario(LoginViewModelInput loginViewModelInput)
        {
            return _contexto.Usuario.FirstOrDefault(u => u.Login == loginViewModelInput.Login);
        }
    }
}
