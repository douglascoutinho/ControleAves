using Dloc.Domain.DTOs;
using Dloc.Domain.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Dloc.ControleAvesWeb.Models
{
    public class MainAveModel
    {
        public Ave Ave { get; set; }
        public Pai Pai { get; set; }
        public Mae Mae { get; set; }
        public Filho Filho { get; set; }
        public Mutacao Mutacao { get; set; }
        public Portador Portador { get; set; }

        public List<Ave> Aves { get; set; }
        public List<Ave> AvesAtivas { get; set; }
        public List<Ave> AvesInativas { get; set; }

        public List<Casamento> Casamentos { get; set; }
        public List<Filho> Filhos { get; set; }
        public List<Filho> Netos { get; set; }
        public List<Filho> Bisnetos { get; set; }
        public List<FilhoDTO> Trinetos { get; set; }
        public List<Mutacao> Mutacoes { get; set; }
        public List<Portador> Portadores { get; set; }

        public IEnumerable<SelectListItem> ItemMachos { set; get; }
        public IEnumerable<SelectListItem> ItemFemeas { set; get; }
        public IEnumerable<SelectListItem> ItemFilhos { set; get; }
        public IEnumerable<SelectListItem> ItemMutacoes { set; get; }
        public IEnumerable<SelectListItem> ItemPortadores { set; get; }
        public IEnumerable<SelectListItem> ItemPaiFilhos { set; get; }
        public IEnumerable<SelectListItem> ItemMaeFilhos { set; get; }

        public int QtdAtiva { get; set; }

        public string CodigoNome(Ave ave)
        {
            return ave.codigo + (ave.nome == null ? "" : "-" + ave.nome);
        }
    }
}
