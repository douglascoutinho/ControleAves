using Dloc.ControleAvesWeb.Models;
using Dloc.Domain.IRepositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DLOC.ControleAvesWeb.Util.Extentions.ViewComponents
{
    [ViewComponent(Name = "DetalheAve")]
    public class DetalheAveViewComponent : ViewComponent
    {
        private readonly IAveRepository _aveRepository;

        public DetalheAveViewComponent(IAveRepository aveRepository) => _aveRepository = aveRepository;

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var model = new MainAveModel
            {
                Ave = _aveRepository.ConsultarPorId(id),
                Pai = _aveRepository.GetPai(id),
                Mae = _aveRepository.GetMae(id),
                Casamentos = _aveRepository.GetCasamentos(id)
            };

            model.Filhos = _aveRepository.GetFilhos(id);
            model.Netos = _aveRepository.GetNetos(model.Filhos);
            model.Bisnetos = _aveRepository.GetBisnetos(model.Netos);
            model.Trinetos = _aveRepository.GetTrinetos(model.Bisnetos);

            return View(model);
        }
    }
}
