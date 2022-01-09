using Dloc.ControleAvesWeb.Models;
using Dloc.Data.Context;
using Dloc.Domain.DTOs;
using Dloc.Domain.Entities;
using Dloc.Domain.Enumeration;
using Dloc.Domain.IRepositories;
using DLOC.ControleAvesWeb.Util.Menssagem;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dloc.ControleAvesWeb.Controllers
{
    public class CaracteristicaController : Controller
    {
        private readonly DataContext _context;
        private readonly IAveRepository _aveRepository;
        private readonly IMutacaoRepository _mutacaoRepository;
        private readonly IPortadorRepository _portadorRepository;

        public CaracteristicaController(DataContext context, IAveRepository aveRepository, IMutacaoRepository mutacaoRepository, IPortadorRepository portadorRepository)
        {
            _context = context;
            _aveRepository = aveRepository;
            _mutacaoRepository = mutacaoRepository;
            _portadorRepository = portadorRepository;
        }

        public IActionResult Index() => View(new MainAveModel
        {
            AvesAtivas = _aveRepository.ConsultarTodos(x => x.ativo),
            AvesInativas = _aveRepository.ConsultarTodos(x => x.ativo == false)
        });

        public ActionResult Edit(int id)
        {
            var filho = _aveRepository.GetFilho(id);

            var model = new MainAveModel
            {
                Ave = _aveRepository.ConsultarPorId(id),
                Pai = _aveRepository.GetPai(id),
                Mae = _aveRepository.GetMae(id),

                ItemMachos = filho == null ? ObterAves((int)SexoEnun.Macho) : ObterAves((int)SexoEnun.Macho, filho.id_pai),
                ItemFemeas = filho == null ? ObterAves((int)SexoEnun.Femea) : ObterAves((int)SexoEnun.Femea, filho.id_mae),
                ItemFilhos = ObterFilhos((int)SexoEnun.Ambos),
                ItemMutacoes = ObterMutacoes(),
                ItemPortadores = ObterPortadores()
            };

            model.ItemPaiFilhos = model.Ave.sexo == (int)SexoEnun.Macho ? ObterAves((int)SexoEnun.Macho, id) : ObterAves((int)SexoEnun.Macho);
            model.ItemMaeFilhos = model.Ave.sexo == (int)SexoEnun.Femea ? ObterAves((int)SexoEnun.Femea, id) : ObterAves((int)SexoEnun.Femea);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MainAveModel model)
        {
            try
            {
                _aveRepository.EditarCaracteristica(model.Ave, model.Pai, model.Mae, model.Filhos, model.Mutacoes, model.Portadores);
                this.ShowMessage(Mensagens.msgAlteracaoSucesso, ToastrDialogType.Sucess);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Details(int id) =>
                    View(new MainAveModel { Ave = new Ave { id = id } });

        public IActionResult GetFilhos(int id)
        {
            try
            {
                var filhos = _context.Filho
                                     .Include(x => x.id_paiNavigation)
                                     .Include(x => x.id_maeNavigation)
                                     .Include(x => x.id_filhoNavigation)
                                     .Where(x => x.id_pai == id || x.id_mae == id);


                var _filhos = new List<FilhoDTO>();

                foreach (var item in filhos)
                {
                    _filhos.Add(new FilhoDTO
                    {
                        id_filho = item.id_filho,
                        id_pai = item.id_pai,
                        id_mae = item.id_mae,
                        nm_filho = new MainAveModel().CodigoNome(item.id_filhoNavigation),
                        nm_pai = new MainAveModel().CodigoNome(item.id_paiNavigation),
                        nm_mae = new MainAveModel().CodigoNome(item.id_maeNavigation),
                    });
                }

                return Json(new { data = _filhos });
            }
            catch
            {
                return StatusCode(500);
            }
        }

        public IActionResult GetPortadores(int id)
        {
            try
            {
                var portadores = _context.PortadorAve
                                     .Include(x => x.id_portadorNavigation)
                                     .Where(x => x.id_ave == id).ToList();

                var _portadores = new List<PortadorAveDTO>();

                foreach (var item in portadores)
                {
                    _portadores.Add(new PortadorAveDTO
                    {
                        id_portadorAve = item.id,
                        id_portador = item.id_portador,
                        ds_portador = item.id_portadorNavigation.ds_portador
                    });
                }

                return Json(new { data = _portadores });
            }
            catch
            {
                return StatusCode(500);
            }
        }

        public IActionResult GetMutacoes(int id)
        {
            try
            {
                var mutacoes = _context.MutacaoAve
                                     .Include(x => x.id_mutacaoNavigation)
                                     .Where(x => x.id_ave == id).ToList();

                var _mutacoes = new List<MutacaoAveDTO>();

                foreach (var item in mutacoes)
                {
                    _mutacoes.Add(new MutacaoAveDTO
                    {
                        id_mutacaoAve = item.id,
                        id_mutacao = item.id_mutacao,
                        ds_mutacao = item.id_mutacaoNavigation.ds_mutacao
                    });
                }

                return Json(new { data = _mutacoes });
            }
            catch
            {
                return StatusCode(500);
            }
        }

        public IActionResult DeleteFilho(int id)
        {
            try
            {
                _aveRepository.ExcluirFilho(id);

                return Json(new { data = string.Empty });
            }
            catch(Exception e)
            {
                return StatusCode(500);
            }
        }

        public IActionResult DeleteMutacao(int id)
        {
            try
            {
                _mutacaoRepository.Excluir(id);
                return Json(new { data = string.Empty });
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

        public IActionResult DeletePortador(int id)
        {
            try
            {
                _portadorRepository.Excluir(id);
                return Json(new { data = string.Empty });
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

        public IActionResult Reativar(int id)
        {
            try
            {
                var ave = _aveRepository.ConsultarPorId(id);
                ave.ativo = true;
                _aveRepository.Atualizar(ave);

                this.ShowMessage(Mensagens.msgReativarSucesso, ToastrDialogType.Sucess);
                return RedirectToAction(nameof(Index));

            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

        private SelectList ObterAves(int sexo, int selected)
        {
            return new SelectList(_aveRepository.GetDropAves(sexo), "Key", "Value", selected);
        }

        private SelectList ObterAves(int sexo)
        {
            return new SelectList(_aveRepository.GetDropAves(sexo), "Key", "Value");
        }

        private SelectList ObterFilhos(int sexo)
        {
            return new SelectList(_aveRepository.GetDropFilhos(), "Key", "Value");
        }

        private SelectList ObterMutacoes()
        {
            return new SelectList(_aveRepository.GetDropMutacoes(), "Key", "Value");
        }

        private SelectList ObterPortadores()
        {
            return new SelectList(_aveRepository.GetDropPortadores(), "Key", "Value");
        }
    }
}