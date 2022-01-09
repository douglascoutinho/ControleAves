using Dloc.Data.Context;
using Dloc.Domain.DTOs;
using Dloc.Domain.Entities;
using Dloc.Domain.Enumeration;
using Dloc.Domain.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dloc.Data.Repositories
{
    public class AveRepository : IAveRepository
    {
        private readonly DataContext _context;

        public AveRepository(DataContext context) =>
            _context = context;
        

        public void Cadastrar(Ave ave)
        {
            _context.Entry(ave).State = EntityState.Added;
            _context.SaveChanges();
        }

        public void Atualizar(Ave ave)
        {
            _context.Entry(ave).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public List<Ave> ConsultarTodos()
        {
            return _context.Ave.AsNoTracking().ToList();
        }

        public List<Ave> ConsultarTodos(Func<Ave, bool> where)
        {
            return _context.Ave.Where(where).ToList();
        }

        public Ave ConsultarPorId(int id)
        {
            return _context.Ave.Find(id);
        }

        public string GetCodigo()
        {
            var contador = _context.Ave.Count() + 1;
            return new Ave().Sequencial(contador);
        }

        

        public Pai GetPai(int id)
        {
            return _context.Pai.Include(x => x.id_paiNavigation).Where(x => x.id_ave == id).FirstOrDefault();
        }

        public Mae GetMae(int id)
        {
            return _context.Mae.Include(x => x.id_maeNavigation).Where(x => x.id_ave == id).FirstOrDefault();
        }

        public Filho GetFilho(int id)
        {
            return _context.Filho.Where(x => x.id_filho == id).FirstOrDefault();
        }

        public List<Filho> GetFilhos(int id)
        {
            return _context.Filho.Include(x => x.id_filhoNavigation).Where(x => x.id_pai == id || x.id_mae == id).ToList();
        }

        public List<Filho> GetNetos(List<Filho> filhos)
        {
            var _netos = new List<Filho>();

            foreach (var item in filhos)
            {
                var lista = _context.Filho.Include(x => x.id_filhoNavigation).Where(x => x.id_pai == item.id_filho || x.id_mae == item.id_filho).ToList();

                foreach (var neto in lista)
                {
                    _netos.Add(neto);
                }
            }

            return _netos;
        }

        public List<Filho> GetBisnetos(List<Filho> netos)
        {
            var _bisnetos = new List<Filho>();

            foreach (var neto in netos)
            {
                var lista = _context.Filho.Include(x => x.id_filhoNavigation).Where(x => x.id_pai == neto.id_filho).ToList();

                foreach (var bisneto in lista)
                {
                    _bisnetos.Add(bisneto);
                }
            }

            return _bisnetos;
        }

        public List<FilhoDTO> GetTrinetos(List<Filho> bisnetos)
        {
            var _trinetos = new List<FilhoDTO>();

            foreach (var bisneto in bisnetos)
            {
                var lista = _context.Filho.Include(x => x.id_filhoNavigation).Where(x => x.id_pai == bisneto.id_filho).ToList();

                foreach (var trineto in lista)
                {
                    _trinetos.Add(new FilhoDTO
                    {
                        id_filho = trineto.id_filhoNavigation.id,
                        id_pai = trineto.id_pai,
                        id_mae = trineto.id_mae,
                        nm_filho = trineto.id_filhoNavigation.nome
                    });
                }
            }

            return _trinetos;
        }

        public List<Casamento> GetCasamentos(int id)
        {
            return _context.Casamento.Include(x => x.id_machoNavigation).Include(x => x.id_femeaNavigation).Where(x => x.id_machoNavigation.id == id || x.id_femeaNavigation.id == id).ToList();
        }

        public void ExcluirFilho(int id)
        {
            var pai = GetPai(id);    //_context.Pai.Where(x => x.id_ave == id).SingleOrDefault();
            var mae = GetMae(id);    // _context.Mae.Where(x => x.id_ave == id).SingleOrDefault();

            if (pai != null) _context.Remove(pai);
            if (mae != null) _context.Remove(mae);

            var filho = _context.Filho.Where(x => x.id_filho == id);
            _context.Remove(filho.First());

            _context.SaveChanges();
        }

        public bool Existe(int id, int parente)
        {
            switch (parente)
            {
                case (int)ParenteEnun.Pai:
                    return _context.Pai.Where(x => x.id_ave == id).Any();

                case (int)ParenteEnun.Mae:
                    return _context.Mae.Where(x => x.id_ave == id).Any();

                case (int)ParenteEnun.Filho:
                    return _context.Filho.Where(x => x.id_filho == id).Any();

                default:
                    return false;
            }
        }

        public IDictionary<int, string> GetDropMutacoes()
        {
            return _context.Mutacao.ToDictionary(x => x.id, x => x.ds_mutacao);
        }

        public IDictionary<int, string> GetDropPortadores()
        {
            return _context.Portador.ToDictionary(x => x.id, x => x.ds_portador);
        }

        public IDictionary<int, string> GetDropAves(int sexo)
        {
            if (sexo.Equals((int)SexoEnun.Ambos))
            {
                return _context.Ave.Where(x => x.ativo).ToDictionary(x => x.id, x => String.IsNullOrEmpty(x.nome) ? x.codigo : x.codigo + "-" + x.nome);
            }
            else
            {
                return _context.Ave.Where(x => x.sexo == sexo && x.ativo).ToDictionary(x => x.id, x => String.IsNullOrEmpty(x.nome) ? x.codigo : x.codigo + "-" + x.nome);
            }
        }

        public IDictionary<int, string> GetDropFilhos()
        {
            var aves = new List<Ave>();

            foreach (var ave in _context.Ave.AsNoTracking().Where(x => x.ativo).ToList())
            {
                if (!_context.Filho.AsNoTracking().Where(x => x.id_filho == ave.id).Any())
                {
                    aves.Add(ave);
                }
            }

            return aves.ToDictionary(x => x.id, x => String.IsNullOrEmpty(x.nome) ? x.codigo : x.codigo + "-" + x.nome);
        }

        public void EditarCaracteristica(Ave modelAve, Pai modelPai, Mae modelMae, List<Filho> modelFilhos, List<Mutacao> modelMutacoes, List<Portador> modelPortadores)
        {
            var pai = GetPai(modelAve.id);
            var mae = GetMae(modelAve.id);

            if (Existe(modelAve.id, (int)ParenteEnun.Pai))
                _context.Remove(pai);

            if (Existe(modelAve.id, (int)ParenteEnun.Mae))
                _context.Remove(mae);

            if (modelPai.id_pai != 0 && modelMae.id_mae != 0)
            {
                if (!_context.Filho.Where(x => x.id_pai == modelPai.id_pai && x.id_mae == modelMae.id_mae && x.id_filho == modelAve.id).Any())
                {
                    _context.Add(new Filho
                    {
                        id_filho = modelAve.id,
                        id_pai = modelPai.id_pai,
                        id_mae = modelMae.id_mae
                    });

                    if (!_context.Casamento.Where(x => x.id_femea == modelMae.id_mae && x.id_macho == modelPai.id_pai).Any())
                    {
                        _context.Casamento.Add(new Casamento
                        {
                            id_femea = modelMae.id_mae,
                            id_macho = modelPai.id_pai
                        });
                    }
                }
            }

            if (modelPai.id_pai != 0) // validação quando os pai forem removidos
                _context.Add(new Pai { id_ave = modelAve.id, id_pai = modelPai.id_pai });

            if (modelMae.id_mae != 0)
                _context.Add(new Mae { id_ave = modelAve.id, id_mae = modelMae.id_mae });

            if (modelFilhos != null)
            {
                foreach (var item in modelFilhos)
                {
                    var existeCasamento = _context.Casamento.Where(x => x.id_femea == item.id_mae && x.id_macho == item.id_pai);

                    if (!existeCasamento.Any())
                    {
                        _context.Casamento.Add(new Casamento { id_macho = item.id_pai, id_femea = item.id_mae });
                        _context.SaveChanges();
                    }

                    var filho = _context.Filho.Where(x => x.id_filho == item.id_filho);

                    if (!filho.Any())
                    {
                        _context.Add(new Filho
                        {
                            id_filho = item.id_filho,
                            id_mae = item.id_mae,
                            id_pai = item.id_pai
                        });



                        var _pai = _context.Pai.Where(x => x.id_ave == item.id_filho).SingleOrDefault();
                        var _mae = _context.Mae.Where(x => x.id_ave == item.id_filho).SingleOrDefault();

                        if (_pai != null) _context.Remove(_pai);
                        if (_mae != null) _context.Remove(_mae);

                        _context.Add(new Pai
                        {
                            id_ave = item.id_filho,
                            id_pai = item.id_pai
                        });

                        _context.Add(new Mae
                        {
                            id_ave = item.id_filho,
                            id_mae = item.id_mae
                        });
                    }
                }
            }

            if (modelMutacoes != null)
            {
                foreach (var mutacao in modelMutacoes)
                {
                    var existeMutacao = _context.MutacaoAve.Where(x => x.id_ave == modelAve.id && x.id_mutacao == mutacao.id).Any();

                    if (!existeMutacao)
                    {
                        _context.MutacaoAve.Add(new MutacaoAve { id_ave = modelAve.id, id_mutacao = mutacao.id });
                    }
                }
            }

            if (modelPortadores != null)
            {
                foreach (var portador in modelPortadores)
                {
                    var existePortador = _context.PortadorAve.Where(x => x.id_ave == modelAve.id && x.id_portador == portador.id).Any();

                    if (!existePortador)
                    {
                        _context.PortadorAve.Add(new PortadorAve { id_ave = modelAve.id, id_portador = portador.id });
                    }
                }
            }

            _context.SaveChanges();
        }
    }
}
