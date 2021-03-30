using DIO.CursosAPI.Business.Entities;
using System.Collections.Generic;

namespace DIO.CursosAPI.Business.Repositories
{
    public interface ICursoRepository
    {
        void Adicionar(Curso curso);
        void Commit();
        IList<Curso> ObterPorUsuario(int codigoUsuario);
    }
}
