using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Dloc.Domain.Entities
{
    public partial class Portador : EntityBase
    {
        public Portador()
        {
            PortadorAve = new HashSet<PortadorAve>();
        }

        [Required(ErrorMessage = "O campo {0} é obrigatorio")]
        [Display(Name = "Mutação Portadora")]
        public string ds_portador { get; set; }

        public virtual ICollection<PortadorAve> PortadorAve { get; set; }
    }
}
