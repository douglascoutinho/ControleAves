namespace Dloc.Domain.DTOs
{
    public class FilhoDTO
    {
        public int id_filho { get; set; }
        public int id_pai { get; set; }
        public int id_mae { get; set; }
        public string nm_pai { get; set; }
        public string nm_mae { get; set; }
        public string nm_filho { get; set; }
        public string codigo { get; set; }
    }
}
