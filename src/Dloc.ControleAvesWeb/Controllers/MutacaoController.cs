using Dloc.Data.Context;
using Dloc.Domain.Entities;
using Dloc.Domain.IRepositories;
using DLOC.ControleAvesWeb.Util.Menssagem;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Dloc.ControleAvesWeb.Controllers
{
    public class MutacaoController : Controller
    {
        private readonly IMutacaoRepository _mutacaoRepository;
        public MutacaoController(DataContext context, IMutacaoRepository mutacaoRepository)
        {
        
            _mutacaoRepository = mutacaoRepository;
        }

        public ActionResult Index() => View( _mutacaoRepository.ConsultarTodos());

        public ActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Mutacao model)
        {
            try
            {
                _mutacaoRepository.Cadastrar(model);
                this.ShowMessage(Mensagens.msgCadastroSucesso, ToastrDialogType.Sucess);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int id) => View(_mutacaoRepository.ConsultarPorId(id));

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Mutacao model)
        {
            try
            {
                _mutacaoRepository.Editar(model);
                this.ShowMessage(Mensagens.msgCadastroSucesso, ToastrDialogType.Sucess);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
