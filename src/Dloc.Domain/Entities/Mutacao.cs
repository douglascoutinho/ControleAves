// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dloc.Domain.Entities
{
    public partial class Mutacao : EntityBase
    {
        public Mutacao() { MutacaoAve = new HashSet<MutacaoAve>(); }

        [Required(ErrorMessage = "O campo {0} é obrigatorio")]
        [Display(Name = "Mutação")]
        public string ds_mutacao { get; set; }

        public virtual ICollection<MutacaoAve> MutacaoAve { get; set; }
    }
}