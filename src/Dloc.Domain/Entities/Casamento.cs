// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Dloc.Domain.Entities
{
    public partial class Casamento : EntityBase
    {
        public int id_macho { get; set; }
        public int id_femea { get; set; }

        public virtual Ave id_femeaNavigation { get; set; }
        public virtual Ave id_machoNavigation { get; set; }
    }
}