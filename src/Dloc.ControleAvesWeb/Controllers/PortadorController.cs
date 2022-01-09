using Dloc.Domain.Entities;
using Dloc.Domain.IRepositories;
using DLOC.ControleAvesWeb.Util.Menssagem;
using Microsoft.AspNetCore.Mvc;

namespace Dloc.ControleAvesWeb.Controllers
{
    public class PortadorController : Controller
    {
        private readonly IPortadorRepository _portadorRepository;

        public PortadorController(IPortadorRepository portadorRepository) =>
            _portadorRepository = portadorRepository;


        public ActionResult Index() => View(_portadorRepository.ConsultarTodos());

        public ActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Portador model)
        {
            try
            {
                _portadorRepository.Cadastrar(model);
                this.ShowMessage(Mensagens.msgCadastroSucesso, ToastrDialogType.Sucess);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int id) => View(_portadorRepository.ConsultarPorId(id));

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Portador model)
        {
            try
            {
                _portadorRepository.Editar(model);
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
