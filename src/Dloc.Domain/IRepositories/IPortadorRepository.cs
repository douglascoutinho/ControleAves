using Dloc.Domain.DTOs;
using Dloc.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Dloc.Domain.IRepositories
{
    public interface IPortadorRepository
    {
        void Cadastrar(Portador model);
        void Editar(Portador model);
        void Excluir(int id);
        List<Portador> ConsultarTodos();
        Portador ConsultarPorId(int id);
    }
}
