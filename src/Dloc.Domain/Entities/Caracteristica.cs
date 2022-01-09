using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DLOC.Domain.Entities
{
    public partial class Caracteristica
    {
        public int id { get; set; }
        public string ds_caracteristica { get; set; }
        public int id_ave { get; set; }

        public virtual Ave id_aveNavigation { get; set; }
    }
}
