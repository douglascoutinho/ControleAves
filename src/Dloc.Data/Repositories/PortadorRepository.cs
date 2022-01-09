using Dloc.Data.Context;
using Dloc.Domain.Entities;
using Dloc.Domain.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Dloc.Data.Repositories
{
    public class PortadorRepository : IPortadorRepository
    {
        public readonly DataContext _context;

        public PortadorRepository(DataContext context) =>
            _context = context;

        public void Cadastrar(Portador model)
        {
            _context.Entry(model).State = EntityState.Added;
            _context.SaveChanges();
        }

        public void Editar(Portador model)
        {
            _context.Entry(model).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Excluir(int id)
        {
            var portador = _context.PortadorAve.Where(x => x.id == id);
            _context.Remove(portador.First());
            _context.SaveChanges();
        }

        public List<Portador> ConsultarTodos()
        {
            return _context.Portador.AsNoTracking().ToList();
        }

        public Portador ConsultarPorId(int id)
        {
            return _context.Portador.Find(id);
        }
    }
}
