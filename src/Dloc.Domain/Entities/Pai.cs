// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Dloc.Domain.Entities
{
    public partial class Pai : EntityBase
    {
        public int id_ave { get; set; }
        public int id_pai { get; set; }

        public virtual Ave id_aveNavigation { get; set; }
        public virtual Ave id_paiNavigation { get; set; }
    }
}
