using Dloc.ControleAvesWeb.Models;
using Dloc.Data.Context;
using Dloc.Domain.Entities;
using Dloc.Domain.IRepositories;
using DLOC.ControleAvesWeb.Util.Menssagem;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using X.PagedList;

namespace Dloc.ControleAvesWeb.Controllers
{
    public class AveController : Controller
    {
        private readonly IAveRepository _aveRepository;
        private readonly DataContext _context;

        public AveController(IAveRepository aveRepository, DataContext context)
        {
            _aveRepository = aveRepository;
            _context = context;
        }

        public IActionResult Index(int page = 1)
        {
            ViewBag.Aves = GetPagedNames(page);
            return View();
        }

        private IPagedList<Ave> GetPagedNames(int? page)
        {
            // return a 404 if user browses to before the first page
            if (page.HasValue && page < 1)
                return null;

            // retrieve list from database/whereverand
            var listUnpaged = _aveRepository.ConsultarTodos(x => x.ativo);

            // page the list
            const int pageSize = 10;
            var listPaged = listUnpaged.ToPagedList(page ?? 1, pageSize);

            // return a 404 if user browses to pages beyond last page. special case first page if no items exist
            if (listPaged.PageNumber != 1 && page.HasValue && page > listPaged.PageCount)
                return null;

            return listPaged;
        }

        public ActionResult Details(int id) =>
            View(new MainAveModel { Ave = new Ave { id = id } });

        public ActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Ave model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.codigo = _aveRepository.GetCodigo();
                    model.Ativar(model);
                    _aveRepository.Cadastrar(model);
                    this.ShowMessage(Mensagens.msgCadastroSucesso, ToastrDialogType.Sucess);
                    return RedirectToAction(nameof(Index));
                }

                return View(model);

            }
            catch (Exception e)
            {
                return View();
            }
        }

        public ActionResult Edit(int id) => View(_aveRepository.ConsultarPorId(id));

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Ave model)
        {
            try
            {
                model.Ativar(model);
                _aveRepository.Atualizar(model);
                model.Ativar(model);

                this.ShowMessage(Mensagens.msgAlteracaoSucesso, ToastrDialogType.Sucess);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public IActionResult Desativar(int id)
        {
            try
            {
                var ave = _aveRepository.ConsultarPorId(id);
                ave.ativo = false;
                _aveRepository.Atualizar(ave);

                return Json(new { data = string.Empty });
            }
            catch
            {
                return Json(new { data = "1" });
            }
        }
    }
}
