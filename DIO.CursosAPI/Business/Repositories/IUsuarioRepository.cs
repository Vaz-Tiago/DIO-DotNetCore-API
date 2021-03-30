using DIO.CursosAPI.Business.Entities;
using DIO.CursosAPI.Models.Usuario;

namespace DIO.CursosAPI.Business.Repositories
{
    public interface IUsuarioRepository
    {
        void Adicionar(Usuario usuario);
        void Commit();
        Usuario ObterUsuario(LoginViewModelInput loginViewModelInput);
    }
}
