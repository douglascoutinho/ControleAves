using Dloc.Domain.Entities;
using System.Collections.Generic;

namespace Dloc.Domain.IRepositories
{
    public interface IMutacaoRepository
    {
        void Cadastrar(Mutacao model);
        void Editar(Mutacao model);
        void Excluir(int id);
        List<Mutacao> ConsultarTodos();
        Mutacao ConsultarPorId(int id);
    }
}
