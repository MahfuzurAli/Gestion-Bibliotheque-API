namespace GestionBibliothequeAPI.Models
{
    public class Emprunt
    {
        public int Id { get; set; }
        public int LivreId { get; set; }
        public Livre Livre { get; set; }
        public DateTime DateEmprunt { get; set; }
        public DateTime DateRetourPrevue { get; set; }
    }
}
