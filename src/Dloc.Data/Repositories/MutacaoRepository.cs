using Dloc.Data.Context;
using Dloc.Domain.Entities;
using Dloc.Domain.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Dloc.Data.Repositories
{
    public class MutacaoRepository : IMutacaoRepository
    {
        public readonly DataContext _context;

        public MutacaoRepository(DataContext context) =>
            _context = context;

        public void Cadastrar(Mutacao model)
        {
            _context.Entry(model).State = EntityState.Added;
            _context.SaveChanges();
        }

        public void Editar(Mutacao model)
        {
            _context.Entry(model).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Excluir(int id)
        {
            var mutacao = _context.MutacaoAve.Where(x => x.id == id);
            _context.Remove(mutacao.First());
            _context.SaveChanges();
        }

        public List<Mutacao> ConsultarTodos()
        {
            return _context.Mutacao.AsNoTracking().ToList();
        }
        public Mutacao ConsultarPorId(int id)
        {
            return _context.Mutacao.Find(id);
        }
    }
}
