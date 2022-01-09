using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dloc.Domain.Entities
{
    public class EntityBase
    {
        [Display(Name = "Código")]
        public int id { get; set; }

        //public EntityBase()
        //{
        //    ListaMsg = new List<string>();
        //}

        //public List<string> ListaMsg { get; private set; }
    }
}
