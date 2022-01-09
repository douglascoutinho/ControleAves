using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Dloc.Domain.Entities
{
    public partial class Ave : EntityBase
    {
        public Ave()
        {
            Noivos = new HashSet<Casamento>();
            Noivas = new HashSet<Casamento>();
            GetFilhos = new HashSet<Filho>();
            Filhos = new HashSet<Filho>();
            ObterFilhos = new HashSet<Filho>();
            GetMaes = new HashSet<Mae>();
            Maes = new HashSet<Mae>();
            MutacaoAve = new HashSet<MutacaoAve>();
            GetPais = new HashSet<Pai>();
            Pais = new HashSet<Pai>();
            PortadorAve = new HashSet<PortadorAve>();
        }

        
        [Display(Name = "Código")]
        public string codigo { get; set; }

        [Display(Name = "Nome")]
        public string nome { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatorio")]
        [Display(Name = "Sexo")]
        public int sexo { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatorio")]
        [Display(Name = "Cor")]
        public string cor { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Data de Nascimento")]
        public DateTime? dt_nascimento { get; set; } //= DateTime.Now;

        public bool ativo { get; set; }

        public virtual ICollection<Casamento> Noivos { get; set; }
        public virtual ICollection<Casamento> Noivas { get; set; }
        public virtual ICollection<Filho> GetFilhos { get; set; }
        public virtual ICollection<Filho> Filhos { get; set; }
        public virtual ICollection<Filho> ObterFilhos { get; set; }
        public virtual ICollection<Mae> GetMaes { get; set; }
        public virtual ICollection<Mae> Maes { get; set; }
        public virtual ICollection<MutacaoAve> MutacaoAve { get; set; }
        public virtual ICollection<Pai> GetPais { get; set; }
        public virtual ICollection<Pai> Pais { get; set; }
        public virtual ICollection<PortadorAve> PortadorAve { get; set; }


        public string Sequencial(int codigo)
        {
            if (codigo.ToString().Length == 1)
            {
                return "C0" + codigo.ToString();
            }

            return "C" + codigo.ToString();
        }

        public Ave Ativar(Ave ave)
        {
            ave.ativo = true;
            return ave;
        }
    }
}
