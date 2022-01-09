using Dloc.Domain.DTOs;
using Dloc.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Dloc.Domain.IRepositories
{
    public interface IAveRepository
    {
        void Cadastrar(Ave ave);
        void Atualizar(Ave ave);
        Ave ConsultarPorId(int id);
        List<Ave> ConsultarTodos();
        List<Ave> ConsultarTodos(Func<Ave, bool> where);
        




        string GetCodigo();

        Pai GetPai(int id);
        Mae GetMae(int id);
        Filho GetFilho(int id);
        List<Filho> GetFilhos(int id);
        List<Filho> GetNetos(List<Filho> filhos);
        List<Filho> GetBisnetos(List<Filho> netos);
        List<FilhoDTO> GetTrinetos(List<Filho> bisnetos);
        void ExcluirFilho(int id);

        List<Casamento> GetCasamentos(int id);
        
        

        bool Existe(int id, int parente);

        IDictionary<int, string> GetDropMutacoes();
        IDictionary<int, string> GetDropPortadores();
        IDictionary<int, string> GetDropAves(int sexo);
        IDictionary<int, string> GetDropFilhos();

        void EditarCaracteristica(Ave modelAve, Pai modelPai, Mae modelMae, List<Filho> modelFilhos, List<Mutacao> modelMutacoes, List<Portador> modelPortadores);
    }
}
