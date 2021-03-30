using DIO.CursosAPI.Models.Usuario;

namespace DIO.CursosAPI.Configurations
{
    public interface IAuthenticationService
    {
        string GerarToken(UsuarioViewModelOutput usuarioViewModelOutput);
    }
}
